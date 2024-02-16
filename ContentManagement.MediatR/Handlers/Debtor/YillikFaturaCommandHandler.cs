using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands.Debtor;
using ContentManagement.Repository;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class YillikFaturaCommandHandler : IRequestHandler<YillikFaturaCommand, ServiceResponse<bool>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IAppSettingRepository _appSettingRepository;

        public YillikFaturaCommandHandler(IDebtorRepository debtorRepository, IFamilyRepository familyRepository, IUnitOfWork<PTContext> unitOfWork, IAppSettingRepository appSettingRepository)
        {
            _debtorRepository = debtorRepository;
            _familyRepository = familyRepository;
            _uow = unitOfWork;
            _appSettingRepository = appSettingRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(YillikFaturaCommand request, CancellationToken cancellationToken)
        {
            if (request.Durum == "a")
            {
                var families = await _familyRepository.All.Where(x => x.IsActive == true && x.IsDeleted == false).AsNoTracking().ToListAsync();
                var count = families.Count();
                int sayac = 0;
                foreach (var family in families)
                {
                    var debtor = new Debtor
                    {
                        Id = Guid.NewGuid(),
                        FamilyId = family.Id,
                        Amount = 350,
                        CreationDate = new DateTime(2024,1,1),
                        IsPayment = false,
                        DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                        DueDate = new DateTime(2024,3,31),
                        DebtorTypeId = 4,
                    };
                    var yil = debtor.CreationDate.Year - 1;
                    var borc = family.Debtors.Where(x => x.CreationDate.Year == yil && x.IsPayment == false && x.DebtorTypeId == debtor.DebtorTypeId).FirstOrDefault();
                    if (borc != null) { debtor.Amount += 150; debtor.Amount += borc.Amount; }
                    _debtorRepository.Add(debtor);
                    _uow.Save();
                    sayac++;
                    SendPDFWithMail(family.MemberId, debtor.DueDate.Value.Year);
                }
                if (sayac == count)
                {
                    return ServiceResponse<bool>.ReturnSuccess();
                }
                else return ServiceResponse<bool>.Return409("Bir hata meydana geldi!");
            }
            else return ServiceResponse<bool>.Return409("Çalıştırılamadı!");
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
            var asm = _appSettingRepository.All.Where(x => x.Key == "arkasayfamesaji").FirstOrDefault();
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
                        var regularFont = BaseFont.CreateFont(wwwrootPath + "/Documents" + "/Montserrat-VariableFont_wght.ttf", "Cp1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        //fields.SetField("@@Besök Eller Post Adress@@", result.Address.District + result.Address.Street + result.Address.PostCode);
                        //fields.SetField("@@Tel@@", result.Address.PhoneNumber);
                        //fields.SetField("@@E-Posta@@", result.Address.Email.ToString());
                        //fields.SetField("@@Bankgiro NR@@", "11-1184");
                        fields.SetField("InfoBox", appsetting.Value.ToString());
                        string arka = asm.Value.ToString();
                        string msj = satirAtlat(arka);
                        string app = appsetting.Value.ToString();
                        string mtn = satirAtlat(app);
                        fields.SetField("BoxInfo", mtn);
                        fields.SetField("arkasayfa", msj);
                        fields.SetField("Faktura Datum", DateTime.Now.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("Ocr", pdf.MemberId.ToString());
                        fields.SetField("Forfalloddatum", pdf.Debtors.Where(x => x.IsPayment == false && x.DueDate.Value.Year == year).FirstOrDefault().DueDate.Value.Date.ToString("dd-MM-yyyy"));
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
                            fields.SetField("Betalningavsendare", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName + "\n" + pdf.Address.District ?? "" + "\n" + pdf.Address.PostCode ?? "" + "\n" + pdf.Address.Street ?? "");
                            fields.SetField("Inf", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName + "\n" + pdf.Address.District ?? "" + "\n" + pdf.Address.PostCode ?? "" + "\n" + pdf.Address.Street ?? "");
                        }
                        else
                        {
                            fields.SetField("Betalningavsendare", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                            fields.SetField("Inf", pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + pdf.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName);
                        }
                        fields.SetField("Kronor", pdf.Debtors.Where(x => x.IsPayment == false).FirstOrDefault().Amount.ToString());
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
