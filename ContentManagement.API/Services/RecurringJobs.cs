using AutoMapper;
using Hafiz.Core.Utilities.Mail;
using Hangfire;
using HtmlAgilityPack;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.Repository;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Hafiz.UI.BackgroudServices
{
    public class RecurringJobs
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IReplacementIdRepository _replacementIdRepository;
        private readonly IDebtorRepository _debtorRepository;
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IUserRoleRepository _UserRoleRepository;
        private readonly UserManager<User> _userManager;

        List<string> ccList = new List<string> { "boranturk@vakifglobal.com" };

        #region ctor
        public RecurringJobs(IFamilyRepository familyRepository, IUserRepository userRepository, IAddressRepository addressRepository, IUnitOfWork<PTContext> uow, IMapper mapper, IFamilyMemberRepository familyMemberRepository, IReplacementIdRepository replacementIdRepository, IDebtorRepository debtorRepository, IAppSettingRepository appSettingRepository, IUserRoleRepository userRoleRepository, UserManager<User> userManager)
        {
            _familyRepository = familyRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _uow = uow;
            _mapper = mapper;
            _familyMemberRepository = familyMemberRepository;
            _replacementIdRepository = replacementIdRepository;
            _debtorRepository = debtorRepository;
            _appSettingRepository = appSettingRepository;
            _UserRoleRepository = userRoleRepository;
            _userManager = userManager;
        }

        // 01.01.2024te ailelere yeni yıl için fatura yazacak mail yoluyla faturalarını bildirecek
        #endregion
        public void FaturaGonder()
        {
            var families = _familyRepository.All.Where(x => x.IsActive == true && x.IsDeleted == false).AsNoTracking().ToList();
            foreach (var family in families)
            {
                var debtor = new Debtor
                {
                    Id = Guid.NewGuid(),
                    FamilyId = family.Id,
                    Amount = 350,
                    CreationDate = DateTime.Now,
                    IsPayment = false,
                    DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                    DueDate = DateTime.Now.AddYears(1),
                    DebtorTypeId = 4,
                };
                _debtorRepository.Add(debtor);
                _uow.Save();
                SendPDFWithMail(family.MemberId, debtor.DueDate.Value.Year);
            }
        }

        //Her gün İtalya saatine göre öğlen 12:00'de çalışır
        public async Task<string> YirmiUcYasKadin()
        {
            CreateExcel createExcel = new CreateExcel();
            var list = new List<Dictionary<string, string>>();
            try
            {
                var kids = _userRepository.FindBy(x => x.MemberTypeId == 3 && x.IsActive == true && x.IsDeleted == false && x.BirthDay != null && x.IsDead != true && x.GenderId == 1).Include(X => X.Family).Include(X => X.FamilyMembers).ThenInclude(x => x.Family).AsNoTracking().ToList();

                foreach (var kid in kids)
                {
                    int refno = 0;
                    int id = 0;
                    var reps = _replacementIdRepository.All.Where(X => X.IsActive == true).AsNoTracking().FirstOrDefault();
                    var flast = _familyRepository.All.OrderBy(x => x.MemberId).AsNoTracking().LastOrDefault(x => x.IsDeleted == false);
                    if (reps == null)
                    {
                        if (flast == null)
                        {
                            id = 10000;
                            refno = 90000;
                        }
                        else
                        {
                            id = flast.MemberId + 1;
                            refno = (int)(flast.ReferenceNumber + 1);
                        }
                    }
                    else
                    {
                        id = reps.SubId;
                        if (flast.ReferenceNumber == null)
                        {
                            refno = 90000;
                        }
                        else refno = (int)flast.ReferenceNumber + 1;
                        reps.IsActive = false;
                        _replacementIdRepository.Update(reps);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();
                    }
                    var age = DateTime.Now.Year - kid.BirthDay.Value.Year;
                    if (age >= 23)
                    {
                        var li = new Dictionary<string, string>();
                        li.Add("NO", kid.IdentificationNumber);
                        li.Add("AD-SOYAD", kid.FirstName + " " + kid.LastName);
                        var memberId = kid.FamilyMembers.FirstOrDefault(X => X.MemberUserId == kid.Id).Family.Id;
                        var adres = _addressRepository.All.Where(X => X.FamilyId == memberId).AsNoTracking().FirstOrDefault();
                        Address address = new Address();
                        if (adres != null)
                        {
                            if (adres.PhoneNumber == null)
                            {
                                adres.PhoneNumber = "-";
                            }
                            if (adres.Email == null)
                            {
                                adres.Email = "-";
                            }
                            address = new Address
                            {
                                District = adres.District,
                                Email = adres.Email,
                                PhoneNumber = adres.PhoneNumber,
                                Street = adres.Street,
                                PostCode = adres.PostCode
                            };
                        }
                        if (kid.FamilyMembers != null)
                        {
                            var fmember = kid.FamilyMembers.FirstOrDefault(x => x.MemberUserId == kid.Id);
                            if (fmember != null)
                            {
                                fmember.MemberTypeId = null;
                                _familyMemberRepository.Remove(fmember);
                                _uow.Context.SaveChanges();
                                _uow.Context.Dispose();
                            }
                        }
                        var family = new Family
                        {
                            Id = Guid.NewGuid(),
                            Name = kid.LastName,
                            IsActive = false,
                            IsDeleted = false,
                            MemberId = id,
                            ReferenceNumber = refno,
                            UserId = kid.Id,
                            CreationDate = DateTime.Now,
                        };
                        _familyRepository.Add(family);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();
                        li.Add("Family Name", family.Name);
                        li.Add("Family MemberId", family.MemberId.ToString());
                        li.Add("Reference Number", family.ReferenceNumber.ToString());
                        if (address != null)
                        {
                            address.FamilyId = family.Id;
                            _addressRepository.Add(address);
                            _uow.Context.SaveChanges();
                            _uow.Context.Dispose();
                        }
                        li.Add("Address", address.Street + "/" + address.District + " " + address.PostCode.ToString());
                        li.Add("Personal Information", "PhoneNumber: " + address.PhoneNumber + " " + "Email: " + address.Email);
                        list.Add(li);
                        var role = new UserRole
                        {
                            RoleId = Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB"),
                            UserId = kid.Id,
                        };
                        _UserRoleRepository.Add(role);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();

                        var fm = new FamilyMember
                        {
                            FamilyId = family.Id,
                            MemberUserId = kid.Id,
                            MemberTypeId = 1,
                            Id = Guid.NewGuid(),
                        };
                        _familyMemberRepository.Add(fm);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();

                        var debtor = new Debtor
                        {
                            Id = Guid.NewGuid(),
                            FamilyId = family.Id,
                            Amount = 350,
                            CreationDate = DateTime.Now,
                            IsPayment = false,
                            DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                            DueDate = DateTime.Now.AddMonths(3),
                            DebtorTypeId = 4,
                        };
                        _debtorRepository.Add(debtor);
                        kid.MemberTypeId = 1;
                        kid.UserName = family.MemberId.ToString();
                        kid.NormalizedUserName = family.MemberId.ToString();
                        //string numara = kid.IdentificationNumber; İtalya'da şifreler kimlik numaralarına göre olmayacak, GUID 10 haneli bir şifre olacak
                        //string sifre = numara.Substring(0, numara.Length >= 6 ? 6 : numara.Length);
                        string sifre = Guid.NewGuid().ToString().Replace("-","").Substring(0, 10);
                        var code = await _userManager.GeneratePasswordResetTokenAsync(kid);
                        var pass = await _userManager.ResetPasswordAsync(kid, code, sifre);
                        _userRepository.Update(kid);
                        _uow.Context.SaveChanges();
                        _uow.Context.ChangeTracker.Clear();
                        _uow.Context.Dispose();
                        SendPDFCommand(family.Id);
                    }
                }
                if (list.Count != 0)
                {
                    var fileA = createExcel.CreatorExcelFile(list);
                    var message = DateTime.Now.Date.ToString() + " tarihinde, 23 yaşına basan kız çocukları için hesap açılma işlemleri tamamlanmıştır. İşlemleri tamamlanan çocuklar ekte verilen belge içerisinde mevcuttur.";
                    var subject = "18 Yaş - Bilgilendirme";
                    MailHelper.SendMailWithCC(message, subject, "noreply@ditib.it", ccList, new Attachment(new MemoryStream(fileA), DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "-23YasKizCocukBilgilendirme" + ".xlsx"));
                }
                else
                {
                    var message = DateTime.Now.Date + " tarihinde 23 yaşına basan kız çocuk bulunamamıştır.";
                    var subject = "23 Yaş Kız Çocuk - Bilgilendirme";
                    MailHelper.SendMail(message, subject, "noreply@ditib.it");
                }
            }
            catch (Exception ex)
            {
                MailHelper.SendMail($"Sorgu sırasında bir hata oluştu. <br/>Hata zamanı: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")} <br/>Hata mesajı: {ex.Message}", "İtalya - 23 Yaş Kız Çocuk Sorgusu", "boranturk@vakifglobal.com");
            }
            return "Bitti";
        }

        public async Task<string> YirmiBirYasErkek()
        {
            CreateExcel createExcel = new CreateExcel();
            var list = new List<Dictionary<string, string>>();
            try
            {
                var kids = _userRepository.FindBy(x => x.MemberTypeId == 3 && x.IsActive == true && x.IsDeleted == false && x.BirthDay != null && x.IsDead != true && x.GenderId == 2).Include(X => X.Family).Include(X => X.FamilyMembers).ThenInclude(x => x.Family).AsNoTracking().ToList();

                foreach (var kid in kids)
                {
                    int refno = 0;
                    int id = 0;
                    var reps = _replacementIdRepository.All.Where(X => X.IsActive == true).AsNoTracking().FirstOrDefault();
                    var flast = _familyRepository.All.OrderBy(x => x.MemberId).AsNoTracking().LastOrDefault(x => x.IsDeleted == false);
                    if (reps == null)
                    {
                        if (flast == null)
                        {
                            id = 10000;
                            refno = 90000;
                        }
                        else
                        {
                            id = flast.MemberId + 1;
                            refno = (int)(flast.ReferenceNumber + 1);
                        }
                    }
                    else
                    {
                        id = reps.SubId;
                        if (flast.ReferenceNumber == null)
                        {
                            refno = 90000;
                        }
                        else refno = (int)flast.ReferenceNumber + 1;
                        reps.IsActive = false;
                        _replacementIdRepository.Update(reps);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();
                    }
                    var age = DateTime.Now.Year - kid.BirthDay.Value.Year;
                    if (age >= 21)
                    {
                        var li = new Dictionary<string, string>();
                        li.Add("NO", kid.IdentificationNumber);
                        li.Add("AD-SOYAD", kid.FirstName + " " + kid.LastName);
                        var memberId = kid.FamilyMembers.FirstOrDefault(X => X.MemberUserId == kid.Id).Family.Id;
                        var adres = _addressRepository.All.Where(X => X.FamilyId == memberId).AsNoTracking().FirstOrDefault();
                        Address address = new Address();
                        if (adres != null)
                        {
                            if (adres.PhoneNumber == null)
                            {
                                adres.PhoneNumber = "-";
                            }
                            if (adres.Email == null)
                            {
                                adres.Email = "-";
                            }
                            address = new Address
                            {
                                District = adres.District,
                                Email = adres.Email,
                                PhoneNumber = adres.PhoneNumber,
                                Street = adres.Street,
                                PostCode = adres.PostCode
                            };
                        }
                        if (kid.FamilyMembers != null)
                        {
                            var fmember = kid.FamilyMembers.FirstOrDefault(x => x.MemberUserId == kid.Id);
                            if (fmember != null)
                            {
                                fmember.MemberTypeId = null;
                                _familyMemberRepository.Remove(fmember);
                                _uow.Context.SaveChanges();
                                _uow.Context.Dispose();
                            }
                        }
                        var family = new Family
                        {
                            Id = Guid.NewGuid(),
                            Name = kid.LastName,
                            IsActive = false,
                            IsDeleted = false,
                            MemberId = id,
                            ReferenceNumber = refno,
                            UserId = kid.Id,
                            CreationDate = DateTime.Now,
                        };
                        _familyRepository.Add(family);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();
                        li.Add("Family Name", family.Name);
                        li.Add("Family MemberId", family.MemberId.ToString());
                        li.Add("Reference Number", family.ReferenceNumber.ToString());
                        if (address != null)
                        {
                            address.FamilyId = family.Id;
                            _addressRepository.Add(address);
                            _uow.Context.SaveChanges();
                            _uow.Context.Dispose();
                        }
                        li.Add("Address", address.Street + "/" + address.District + " " + address.PostCode.ToString());
                        li.Add("Personal Information", "PhoneNumber: " + address.PhoneNumber + " " + "Email: " + address.Email);
                        list.Add(li);
                        var role = new UserRole
                        {
                            RoleId = Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB"),
                            UserId = kid.Id,
                        };
                        _UserRoleRepository.Add(role);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();

                        var fm = new FamilyMember
                        {
                            FamilyId = family.Id,
                            MemberUserId = kid.Id,
                            MemberTypeId = 1,
                            Id = Guid.NewGuid(),
                        };
                        _familyMemberRepository.Add(fm);
                        _uow.Context.SaveChanges();
                        _uow.Context.Dispose();

                        var debtor = new Debtor
                        {
                            Id = Guid.NewGuid(),
                            FamilyId = family.Id,
                            Amount = 350,
                            CreationDate = DateTime.Now,
                            IsPayment = false,
                            DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                            DueDate = DateTime.Now.AddMonths(3),
                            DebtorTypeId = 4,
                        };
                        _debtorRepository.Add(debtor);
                        kid.MemberTypeId = 1;
                        kid.UserName = family.MemberId.ToString();
                        kid.NormalizedUserName = family.MemberId.ToString();
                        //string numara = kid.IdentificationNumber; İtalya'da şifreler kimlik numaralarına göre olmayacak, GUID 10 haneli bir şifre olacak
                        //string sifre = numara.Substring(0, numara.Length >= 6 ? 6 : numara.Length);
                        string sifre = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
                        var code = await _userManager.GeneratePasswordResetTokenAsync(kid);
                        var pass = await _userManager.ResetPasswordAsync(kid, code, sifre);
                        _userRepository.Update(kid);
                        _uow.Context.SaveChanges();
                        _uow.Context.ChangeTracker.Clear();
                        _uow.Context.Dispose();
                        SendPDFCommand(family.Id);
                    }
                }
                if (list.Count != 0)
                {
                    var fileA = createExcel.CreatorExcelFile(list);
                    var message = DateTime.Now.Date.ToString() + " tarihinde, 21 yaşına basan çocuklar için hesap açılma işlemleri tamamlanmıştır. İşlemleri tamamlanan çocuklar ekte verilen belge içerisinde mevcuttur.";
                    var subject = "21 Erkek Çocuk - Bilgilendirme";
                    MailHelper.SendMailWithCC(message, subject, "noreply@ditib.it", ccList, new Attachment(new MemoryStream(fileA), DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "-21ErkekCocukBilgilendirme" + ".xlsx"));
                }
                else
                {
                    var message = DateTime.Now.Date + " tarihinde 21 yaşına basan çocuk bulunamamıştır.";
                    var subject = "21 Yaş Erkek Çocuk - Bilgilendirme";
                    MailHelper.SendMail(message, subject, "noreply@ditib.it");
                }
            }
            catch (Exception ex)
            {
                MailHelper.SendMail($"Sorgu sırasında bir hata oluştu. <br/>Hata zamanı: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")} <br/>Hata mesajı: {ex.Message}", "İtalya - 21 Yaş Erkek Çocuk Sorgusu", "boranturk@vakifglobal.com");
            }
            return "Bitti";
        }

        private void SendPDFWithMail(int memberId, int year)
        {
            var pdf = _familyRepository.All
            .Include(x => x.FamilyMembers)
            .ThenInclude(x => x.MemberUser)
            .Include(x => x.Debtors)
            .Include(x => x.Address)
            .Include(x => x.FamilyNotes)
            .Where(x => x.MemberId == memberId).FirstOrDefault();
            var appsetting = _appSettingRepository.All.Where(x => x.Key == "isvecfaturamessage").FirstOrDefault();
            byte[] pdfdata;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string loadPath = wwwrootPath + "/Documents/IDVFatura.pdf";
            MailResponse response;
            //var addicted = JsonConvert.DeserializeObject<AddictedEnum[]>(result.IsAddicted);
            using (var reader = new PdfReader(loadPath))
            {
                using (var stream = new MemoryStream())
                {
                    using (var stamper = new PdfStamper(reader, stream))
                    {
                        AcroFields fields = stamper.AcroFields;
                        fields.GenerateAppearances = true;
                        var regularFont = BaseFont.CreateFont(wwwrootPath + "/Documents" + "/calibri-font.ttf", "windows-1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        //fields.SetField("@@Besök Eller Post Adress@@", result.Address.District + result.Address.Street + result.Address.PostCode);
                        //fields.SetField("@@Tel@@", result.Address.PhoneNumber);
                        //fields.SetField("@@E-Posta@@", result.Address.Email.ToString());
                        //fields.SetField("@@Bankgiro NR@@", "11-1184");
                        string app = appsetting.Value.ToString();
                        string mtn = satirAtlat(app);
                        var innerhtml = ConvertToPlainText(mtn);
                        fields.SetField("BoxInfo", innerhtml);
                        fields.SetField("Faktura Datum", DateTime.Now.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("Ocr", pdf.MemberId.ToString());
                        if (pdf.Debtors.Count!=0)
                        {
                            fields.SetField("Forfalloddatum", pdf.Debtors.Where(x => x.IsPayment == false && x.DueDate.Value.Year == year).FirstOrDefault().DueDate.Value.Date.ToString("dd-MM-yyyy"));
                        }
                        fields.SetField("UyeNo", pdf.MemberId.ToString());
                        fields.SetField("Kendisi", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                        if (pdf.FamilyMembers.Any(x => x.MemberTypeId == 2))
                        {
                            fields.SetField("Es", pdf.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.LastName);
                        }
                        else { fields.SetField("Es", "-"); }
                        if (pdf.FamilyMembers.Where(x => x.MemberTypeId == 3).Count() != 0)
                        {
                            foreach (var field in pdf.FamilyMembers.Where(x => x.MemberTypeId == 3))
                            {
                                fields.SetField("Cocuklari", field.MemberUser.FirstName + " " + field.MemberUser.LastName);
                            }
                        }
                        else { fields.SetField("Cocuklari", "-"); }
                        if (pdf.Address != null)
                        {
                            fields.SetField("Betalningavsendare", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + pdf.Address.Street + "\n" + pdf.Address.PostCode + " " + pdf.Address.District);
                            fields.SetField("Inf", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + pdf.Address.Street + "\n" + pdf.Address.PostCode + " " + pdf.Address.District);
                        }
                        else
                        {
                            fields.SetField("Betalningavsendare", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                            fields.SetField("Inf", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                        }
                        var amount = pdf.Debtors.Where(x => x.IsPayment == false).FirstOrDefault().Amount.ToString("0.##");
                        fields.SetField("Kronor", amount);
                        fields.SetField("Meddalende", pdf.MemberId.ToString());
                        stamper.Writer.CloseStream = false;
                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;
                        pdfdata = stream.ToArray();
                        //var subject = "İsveç Cenaze Fonu - Fatura Bilgilendirme";
                        //var message = "İsveç Cenaze Fonuna kayıt işleminiz başarıyla gerçekleşmiştir! \n Giriş faturanız ektedir.";
                        //MailHelper.SendMail2(message, subject, pdf.Address.Email, new System.Net.Mail.Attachment(new MemoryStream(pdfdata),
                        //    pdf.MemberId + "_" + pdf.Name + "_Fatura", "application/pdf"));
                    }
                }
            }
        }

        private void SendPDFCommand(Guid familyId)
        {
            var pdf = _familyRepository.All
            .Include(x => x.FamilyMembers)
            .ThenInclude(x => x.MemberUser)
            .Include(x => x.Debtors)
            .Include(x => x.Address)
            .Include(x => x.FamilyNotes)
            .Where(x => x.Id == familyId).FirstOrDefault();
            var appsetting = _appSettingRepository.All.Where(x => x.Key == "isvecfaturamessage").FirstOrDefault();
            byte[] pdfdata;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string loadPath = wwwrootPath + "/Documents/IDVFatura.pdf";
            MailResponse response;
            using (var reader = new PdfReader(loadPath))
            {
                using (var stream = new MemoryStream())
                {
                    using (var stamper = new PdfStamper(reader, stream))
                    {
                        AcroFields fields = stamper.AcroFields;
                        fields.GenerateAppearances = true;
                        var regularFont = BaseFont.CreateFont(wwwrootPath + "/Documents" + "/arial.ttf", "windows-1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        string app = appsetting.Value.ToString();
                        string mtn = satirAtlat(app);
                        var innerhtml = ConvertToPlainText(mtn);
                        fields.SetField("BoxInfo", innerhtml);
                        fields.SetField("Faktura Datum", DateTime.Now.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("Ocr", pdf.ReferenceNumber.ToString());
                        fields.SetField("ocrinf", pdf.ReferenceNumber.ToString());
                        fields.SetField("Forfalloddatum", pdf.Debtors.Where(x => x.IsPayment == false).FirstOrDefault().DueDate.Value.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("UyeNo", pdf.ReferenceNumber.ToString());
                        fields.SetField("Kendisi", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                        if (pdf.FamilyMembers.Any(x => x.MemberTypeId == 2))
                        {
                            fields.SetField("Es", pdf.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.LastName);
                        }
                        else { fields.SetField("Es", "-"); }
                        if (pdf.FamilyMembers.Where(x => x.MemberTypeId == 3).Count() != 0)
                        {
                            int sayac = 0;
                            foreach (var field in pdf.FamilyMembers.Where(x => x.MemberTypeId == 3))
                            {
                                sayac++;
                                if (sayac == 1)
                                {
                                    fields.SetField("Cocuklari" + sayac, field.MemberUser.FirstName + " " + field.MemberUser.LastName);
                                }
                                else
                                {
                                    string alan = "Cocuklari" + sayac;
                                    fields.SetField(alan, field.MemberUser.FirstName + " " + field.MemberUser.LastName);
                                }
                            }
                        }
                        else { fields.SetField("Cocuklari", "-"); }
                        fields.SetField("Betalningavsendare", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + pdf.Address.Street + "\n" + pdf.Address.PostCode + " " + pdf.Address.District);
                        var amount = pdf.Debtors.Where(x => x.IsPayment == false).FirstOrDefault().Amount.ToString("0.##");
                        fields.SetField("Kronor", amount);
                        fields.SetField("Meddalende", pdf.ReferenceNumber.ToString());
                        fields.SetField("Inf", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + pdf.Address.Street + "\n" + pdf.Address.PostCode + " " + pdf.Address.District);
                        stamper.Writer.CloseStream = false;
                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;
                        pdfdata = stream.ToArray();
                        var subject = "İtalya Cenaze Fonu - Aile Bilgilendirme Bilgilendirme";
                        var message = "İtalya Cenaze Fonuna kayıt işleminiz başarıyla gerçekleşmiştir! \n Giriş faturanız ektedir.";
                        string name = pdf.MemberId + "_" + pdf.Name + "_Fatura";
                        MailHelper.SendMail2(message, subject, pdf.Address.Email, new System.Net.Mail.Attachment(new MemoryStream(pdfdata),
                            name, "application/pdf"));
                        FileHelper.UploadPDF(pdfdata, "PDFs/YeniAilePDF", name);
                    }
                }
            }
        }

        #region InnerHTML için Kodlar
        // Sorun olursa araştırdığım site; https://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text
        private static string ConvertToPlainText(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }
        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }
        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // get text
                    html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            outText.Write("\r\n");
                            break;
                        case "br":
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }
        #endregion
        public static string satirAtlat(string metin)
        {
            string[] satirlar = metin.Split("\\n"); // appsettingsin değerini \nlerden kesecek
            string yenimetin = "";

            foreach (string satir in satirlar)
            {
                yenimetin += satir + "\n\n"; // \den sonra gelen satırı alacak sonra tekrar bir daha satır atlatacak
            }

            return yenimetin;
        }
    }
}



