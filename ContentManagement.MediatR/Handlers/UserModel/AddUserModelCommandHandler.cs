using AutoMapper;
using Hafiz.Core.Utilities.Mail;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneSignalApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Asn1.Ocsp;
using HtmlAgilityPack;

namespace ContentManagement.MediatR.Handlers
{
    public class AddUserModelCommandHandler : IRequestHandler<AddUserModelCommand, ServiceResponse<UserModelDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserModelRepository _userModelRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IDebtorRepository _debtorRepository;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISpouseRepository _spouseRepository;
        private readonly IReplacementIdRepository _replacementIdRepository;
        private readonly IAgeRepository _ageRepository;
        private readonly IUserRoleRepository _roleRepository;
        private readonly UserManager<Data.User> _userManager;
        private readonly UserInfoToken _infoToken;
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly ICityRepository _cityRepository;
        public AddUserModelCommandHandler(IMapper mapper, IUserModelRepository userModelRepository, IUnitOfWork<PTContext> uow, IFamilyMemberRepository familyMemberRepository, IAddressRepository addressRepository, IDebtorRepository debtorRepository, IUserDetailRepository userDetailRepository, IFamilyRepository familyRepository, IUserRepository userRepository, ISpouseRepository spouseRepository, IReplacementIdRepository replacementIdRepository, IAgeRepository ageRepository, IUserRoleRepository roleRepository, UserManager<Data.User> userManager, UserInfoToken infoToken, IAppSettingRepository appSettingRepository, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _userModelRepository = userModelRepository;
            _uow = uow;
            _familyMemberRepository = familyMemberRepository;
            _addressRepository = addressRepository;
            _debtorRepository = debtorRepository;
            _userDetailRepository = userDetailRepository;
            _familyRepository = familyRepository;
            _userRepository = userRepository;
            _spouseRepository = spouseRepository;
            _replacementIdRepository = replacementIdRepository;
            _ageRepository = ageRepository;
            _roleRepository = roleRepository;
            _userManager = userManager;
            _infoToken = infoToken;
            _appSettingRepository = appSettingRepository;
            _cityRepository = cityRepository;
        }
        public async Task<ServiceResponse<UserModelDTO>> Handle(AddUserModelCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<UserModel>(request);
            var userBdAY = request.BirthDay;
            var familyMembers = request.FamilyMembers?.Select(fm => fm.BirthDay);
            int userage = 0;
            if (familyMembers != null)
            {
                var maxAge = familyMembers?.Concat(new[] { userBdAY }).Min();
                userage = DateTime.Now.Year - maxAge.Value.Year;
            }
            else userage = DateTime.Now.Year - userBdAY.Value.Year;

            var deb = _ageRepository.All.Where(x => userage >= x.MinAge && userage <= x.MaxAge).FirstOrDefault();
            if (deb == null)
            {
                return ServiceResponse<UserModelDTO>.Return409("Yaşınıza uygun bir borç miktarı bulunamadı!");
            }
            var city = _cityRepository.All.Where(X => X.Id == request.CityId).FirstOrDefault();
            if (city == null)
            {
                return ServiceResponse<UserModelDTO>.Return409("Seçtiğiniz şehir bulunmamaktadır!");
            }
            List<string> familyMailList = new List<string>();
            var usercheck = _userRepository.FindBy(x => x.IdentificationNumber == result.IdentificationNumber).FirstOrDefault();
            if (usercheck != null)
            {
                return ServiceResponse<UserModelDTO>.Return409("Bu kimlik numarasıyla eşleşen bir kayıt bulunmaktadır!");
            }
            int refno = 0;
            int id = 0;
            var reps = _replacementIdRepository.All.Where(X => X.IsActive == true).FirstOrDefault();
            var flast = _familyRepository.All.OrderBy(x => x.MemberId).LastOrDefault(x => x.IsDeleted == false);
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
                if (flast == null)
                {
                    refno = 90000;
                }
                else refno = (int)(flast.ReferenceNumber + 1);

                reps.IsActive = false;
                _replacementIdRepository.Update(reps);
                _uow.Context.SaveChanges();
                _uow.Context.Dispose();
            }
            string userfirstName = ReplaceTurkishCharacters(result.FirstName);
            string userLastName = ReplaceTurkishCharacters(result.LastName);
            var user = new Data.User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsActive = false,
                IsDeleted = false,
                CreatedDate = DateTime.Now.ToLocalTime(),
                ModifiedDate = DateTime.Now.ToLocalTime(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                GenderId = result.GenderId,
                Nationality = result.Nationality,
                IdentificationNumber = result.IdentificationNumber,
                BirthPlace = result.BirthPlace,
                BirthDay = result.BirthDay,
                IsDead = false,
                Email = request.Address.Email,
                MemberTypeId = 1,
                UserName = id.ToString(),
                CityId = city.Id,
            };
            var reisAge = DateTime.Now.Year - user.BirthDay.Value.Year;
            if (reisAge < 18)
            {
                return ServiceResponse<UserModelDTO>.Return409("18 yaşından küçükler başvuramaz!");
            }
            user.PasswordHash = Guid.NewGuid().ToString();
            user.SecurityStamp = Guid.NewGuid().ToString();
            _userRepository.Add(user);
            var role = new UserRole
            {
                RoleId = Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB"),
                UserId = user.Id,
            };
            _roleRepository.Add(role);
            var userDetail = new UserDetail
            {
                UserId = user.Id,
                CitizenShipId = 1,
                IdentificationNumber = user.IdentificationNumber
            };
            _userDetailRepository.Add(userDetail);

            var f = new Family
            {

                Id = Guid.NewGuid(),
                Name = result.LastName,
                UserId = user.Id,
                IsDeleted = false,
                IsActive = false,
                MemberId = id,
                ReferenceNumber = refno,
                CreationDate = DateTime.Now,
                CityId = city.Id,
            };
            _familyRepository.Add(f);

            var ufm = new FamilyMember
            {
                FamilyId = f.Id,
                MemberTypeId = 1,
                Id = Guid.NewGuid(),
                MemberUserId = user.Id,
            };
            _familyMemberRepository.Add(ufm);
            var address = new Address
            {
                FamilyId = f.Id,
                Street = request.Address.Street,
                District = request.Address.District,
                PostCode = request.Address.PostCode,
                Email = request.Address.Email,
                PhoneNumber = request.Address.PhoneNumber,
                CityId = city.Id
            };
            _addressRepository.Add(address);

            var debtor = new Debtor
            {
                Id = Guid.NewGuid(),
                DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                Amount = deb.Amount,
                IsPayment = false,
                CreationDate = DateTime.Now.ToLocalTime(),
                FamilyId = f.Id,
                DueDate = DateTime.Now.AddMonths(3),
                DebtorTypeId = 3
            };
            _debtorRepository.Add(debtor);
            foreach (var familyMember in result.FamilyMembers)
            {
                if (familyMember.MemberTypeId == 2)
                {
                    var esYas = DateTime.Now.Year - familyMember.BirthDay.Value.Year;
                    if (esYas < 18)
                    {
                        return ServiceResponse<UserModelDTO>.Return409("Eş 18 yaşından küçük olamaz!");
                    }
                    var wcheck = _userRepository.FindBy(x => x.IdentificationNumber == familyMember.IdentificationNumber).FirstOrDefault();
                    if (wcheck != null)
                    {
                        return ServiceResponse<UserModelDTO>.Return409("Bu kimlik numarasına ait bir eş kaydı bulunmaktadır!");
                    }
                }
                if (familyMember.MemberTypeId == 3)
                {
                    var bday = DateTime.Now.Year - familyMember.BirthDay.Value.Year;
                    if (familyMember.GenderId == 1)
                    {
                        if (bday >= 23)
                        {
                            return ServiceResponse<UserModelDTO>.Return409("23 yaşından büyük kız çocukları için ayrı hesap açılmalıdır!");
                        }
                    }
                    else
                    {
                        if (bday >= 21)
                        {
                            return ServiceResponse<UserModelDTO>.Return409("21 yaşından büyük erkek çocukları için ayrı hesap açılmalıdır!");
                        }
                    }                  
                    var childcheck = _userRepository.FindBy(x => x.IdentificationNumber == familyMember.IdentificationNumber).FirstOrDefault();
                    if (childcheck != null)
                    {
                        return ServiceResponse<UserModelDTO>.Return409("Bu kimlik numarasına ait bir çocuk kaydı bulunmaktadır!");
                    }
                }
                string fmUserFirstName = ReplaceTurkishCharacters(familyMember.FirstName);
                string fmUserLastName = ReplaceTurkishCharacters(familyMember.LastName);
                var fmUser = new Data.User
                {
                    Id = familyMember.Id,
                    FirstName = familyMember.FirstName,
                    LastName = familyMember.LastName,
                    IsActive = false,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now.ToLocalTime(),
                    ModifiedDate = DateTime.Now.ToLocalTime(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    IdentificationNumber = familyMember.IdentificationNumber,
                    BirthPlace = familyMember.BirthPlace,
                    BirthDay = familyMember.BirthDay,
                    GenderId = familyMember.GenderId,
                    Nationality = familyMember.Nationality,
                    IsDualNationality = familyMember.IsDualNationality,
                    IsDead = false,
                    Email = request.Address.Email,
                    MemberTypeId = familyMember.MemberTypeId,
                    PasswordHash = Guid.NewGuid().ToString(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CityId = city.Id
                };
                _userRepository.Add(fmUser);
                var fm = new FamilyMember
                {
                    Id = Guid.NewGuid(),
                    FamilyId = f.Id,
                    MemberUserId = familyMember.Id,
                    MemberTypeId = familyMember.MemberTypeId
                };
                _familyMemberRepository.Add(fm);
                var ud = new UserDetail
                {
                    UserId = fmUser.Id,
                    CitizenShipId = 1,
                    IdentificationNumber = familyMember.IdentificationNumber
                };
                _userDetailRepository.Add(ud);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserModelDTO>.Return500();
            }
            else
            {
                var app = _appSettingRepository.All.Where(X => X.Key == "MailSend").FirstOrDefault();
                if (app.Value == "1")
                {
                   var path = SendPDFCommand(f.Id, userage);
                   result.PdfPath = path;
                }
                return ServiceResponse<UserModelDTO>.ReturnResultWith200(_mapper.Map<UserModelDTO>(result));
            }
        }

        private string SendPDFCommand(Guid familyId, int age)
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
                        var subject = "İtalya Cenaze Fonu - Fatura Bilgilendirme";
                        if (age >= 65)
                        {
                            var msg = "İtalya Cenaze Fonuna kayıt işleminiz başarıyla gerçekleşmiştir! \n Fatura detayınız için vakıfla iletişime geçiniz!";
                            MailHelper.SendMail(msg, subject, pdf.Address.Email);
                        }
                        var message = "İtalya Cenaze Fonuna kayıt işleminiz başarıyla gerçekleşmiştir! \n Giriş faturanız ektedir.";
                        string name = pdf.MemberId + "_" + pdf.Name + "_Fatura";
                        MailHelper.SendMail2(message, subject, pdf.Address.Email, new System.Net.Mail.Attachment(new MemoryStream(pdfdata),
                            name, "application/pdf"));
                        var spath = FileHelper.UploadPDF(pdfdata, "PDFs", name);
                        return "PDFs/"+ spath;
                    }
                }
            }
        }

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
        public static string ReplaceTurkishCharacters(string turkishWord)
        {
            string source = "ığüşöçĞÜŞİÖÇ";
            string destination = "igusocGUSIOC";

            string result = turkishWord;

            for (int i = 0; i < source.Length; i++)
            {
                result = result.Replace(source[i], destination[i]);
            }

            return result.ToLower();
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
    }
}
