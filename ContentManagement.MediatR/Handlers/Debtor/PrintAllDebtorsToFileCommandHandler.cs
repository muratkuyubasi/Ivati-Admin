using DocumentFormat.OpenXml.Spreadsheet;
using Hafiz.Core.Utilities.Mail;
using HtmlAgilityPack;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class PrintAllDebtorsToFileCommandHandler : IRequestHandler<PrintAllDebtorsToFileCommand, ServiceResponse<string>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IAppSettingRepository _appSettingRepository;

        public PrintAllDebtorsToFileCommandHandler(IDebtorRepository debtorRepository, IAppSettingRepository appSettingRepository, IFamilyRepository familyRepository)
        {
            _debtorRepository = debtorRepository;
            _appSettingRepository = appSettingRepository;
            _familyRepository = familyRepository;
        }
        public async Task<ServiceResponse<string>> Handle(PrintAllDebtorsToFileCommand request, CancellationToken cancellationToken)
        {
            if (request.Approve)
            {
                var debtors = await _debtorRepository.FindBy(x => x.CreationDate.Year == request.Year && x.IsPayment == false && x.Family.IsDeleted == false).Include(x => x.Family).OrderBy(x=>x.Family.MemberId).ToListAsync();
                List<byte[]> pdfDataList = new List<byte[]>();
                int sayac = 0;
                foreach (var debtor in debtors)
                {
                    var pdfData = SendPDFCommand(debtor.Family.Id, debtor.DebtorNumber);
                    byte[] compressedPdfData = CompressPdf(pdfData);
                    pdfDataList.Add(compressedPdfData);
                    sayac++;
                }

                string outputPath = Path.Combine("wwwroot", "Debtors", request.Year + "_" + "Debtors.pdf");
                string winrarPath = Path.Combine("wwwroot", "Documents", "WinRAR.exe");
                string rarFilePath = Path.Combine("wwwroot", "Debtors", request.Year + "_" + "Debtors.rar");
                if (System.IO.File.Exists(rarFilePath))
                {
                    System.IO.File.Delete(rarFilePath);
                }
                if (pdfDataList.Count != 0)
                {
                    MergeDebtorsPDFs(pdfDataList, outputPath, winrarPath, rarFilePath);
                }
                else return ServiceResponse<string>.ReturnFailed(404, "Bu yıla ait ödenmemiş bastırılacak fatura bulunmamaktadır!");
                #region C içerisine klasör oluşturma ve pdfi oraya atma
                //string folderPath = "C:\\Debtors";
                //if (pdfDataList.Count != 0)
                //{
                //    if (!Directory.Exists(folderPath))
                //    {
                //        string mergedFilePath = Path.Combine(folderPath, request.Year.ToString() + "_" + "Debtors.pdf");
                //        Directory.CreateDirectory(folderPath);
                //        MergeDebtorsPDFs(pdfDataList, mergedFilePath);
                //    }
                //    else
                //    {
                //        Directory.CreateDirectory(folderPath);
                //        string mergedFilePath = Path.Combine(folderPath, request.Year.ToString() + "_" + "Debtors.pdf");
                //        MergeDebtorsPDFs(pdfDataList, mergedFilePath);
                //    }
                //}
                //else return ServiceResponse<bool>.Return409("Bu yıla ait bastırılacak bir fatura bulunamadı!");
                #endregion

                if (sayac == debtors.Count)
                {
                    string returnPath = "Debtors/" + request.Year + "_Debtors.rar";
                    return ServiceResponse<string>.ReturnResultWith200(returnPath);
                }
                else return ServiceResponse<string>.ReturnFailed(102, "Yükleme devam ediyor!");
            }
            else return ServiceResponse<string>.ReturnFailed(406, "Faturaları oluşturmak için onay vermeniz gerekmektedir!");
        }
        private byte[] SendPDFCommand(Guid familyId, string debtornumber)
        {
            var pdf = _familyRepository.All
            .Include(x => x.FamilyMembers)
            .ThenInclude(x => x.MemberUser)
            .Include(x => x.Debtors)
            .Include(x => x.Address)
            .Include(x => x.FamilyNotes)
            .Where(x => x.Id == familyId).FirstOrDefault();
            var appsetting = _appSettingRepository.All.Where(x => x.Key == "isvecfaturamessage").FirstOrDefault();
            //var arkasayfa = _appSettingRepository.All.Where(x => x.Key == "arkasayfametni").FirstOrDefault();
            byte[] pdfdata;
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string loadPath = wwwrootPath + "/Documents/IDVFaturaTekli.pdf";
            MailResponse response;
            //var addicted = JsonConvert.DeserializeObject<AddictedEnum[]>(result.IsAddicted);
            using (var reader = new PdfReader(loadPath))
            {
                using (var stream = new MemoryStream())
                {
                    using (var stamper = new PdfStamper(reader, stream))
                    {
                        #region Form Alanlarını Doldurma
                        AcroFields fields = stamper.AcroFields;
                        fields.GenerateAppearances = true;
                        var regularFont = BaseFont.CreateFont(wwwrootPath + "/Documents" + "/calibri-font.ttf", "windows-1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        //if (arkasayfa != null)
                        //{
                        //    string asm = arkasayfa.Value.ToString();
                        //    string asmetni = satirAtlat(asm);
                        //    fields.SetField("arkasayfa", asmetni);
                        //}
                        string app = appsetting.Value.ToString();
                        string mtn = satirAtlat(app);
                        var innerhtml = ConvertToPlainText(mtn);
                        fields.SetField("BoxInfo", innerhtml);
                        fields.SetField("Faktura Datum", pdf.Debtors.Where(x => x.DebtorNumber == debtornumber).FirstOrDefault().CreationDate.ToString("dd-MM-yyyy"));
                        fields.SetField("Ocr", pdf.MemberId.ToString());
                        fields.SetField("ocrinf", pdf.MemberId.ToString());
                        fields.SetField("Forfalloddatum", pdf.Debtors.Where(x => x.DebtorNumber == debtornumber).FirstOrDefault().DueDate.Value.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("UyeNo", pdf.MemberId.ToString());
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
                                    fields.SetField("Cocuklari" + sayac, field.MemberUser.FirstName + " " + field.MemberUser.LastName);
                                }
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

                        var amount = pdf.Debtors.Where(x => x.DebtorNumber == debtornumber).FirstOrDefault().Amount.ToString("0.##");
                        fields.SetField("Kronor", amount);
                        fields.SetField("Meddalende", pdf.MemberId.ToString());
                        #endregion
                        stamper.Writer.CloseStream = false;
                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;
                        byte[] pdfData;
                        stream.CopyTo(stream);
                        pdfdata = stream.ToArray();
                        return pdfdata;
                        #region Klasör Oluşturma ve Her fatura pdfini klasör içerisine yazma
                        //pdfdata = stream.ToArray();
                        //string folderPath = "C:\\Debtors";
                        //if (!Directory.Exists(folderPath))
                        //{
                        //    //Directory.CreateDirectory(folderPath);
                        //    //string fileName = Path.Combine(folderPath, pdf.MemberId + "_" + debtornumber + "_" + pdf.Name + ".pdf"); 
                        //    //using (FileStream fs = new FileStream(fileName, FileMode.Append))
                        //    //{
                        //    //    fs.Write(pdfdata, 0, pdfdata.Length);
                        //    //}
                        //}
                        //else
                        //{
                        //    //Directory.CreateDirectory(folderPath);
                        //    //string fileName = Path.Combine(folderPath, pdf.MemberId + "_" + debtornumber + "_" + pdf.Name + ".pdf");
                        //    //using (FileStream fs = new FileStream(fileName, FileMode.Append))
                        //    //{
                        //    //    fs.Write(pdfdata, 0, pdfdata.Length);
                        //    //}
                        //}
                        #endregion
                    }
                }
            }
        }

        private byte[] CompressPdf(byte[] pdfData)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(pdfData);
                PdfStamper stamper = new PdfStamper(reader, outputStream, PdfWriter.VERSION_1_5);
                stamper.SetFullCompression();
                stamper.Close();
                reader.Close();
                return outputStream.ToArray();
            }
        }

        private void MergeDebtorsPDFs(List<byte[]> pdfDataList, string outputPath, string winrarPath, string rarFilePath)
        {
            using (var outputPdfStream = new FileStream(outputPath, FileMode.Create))
            {
                using (var document = new Document())
                {
                    using (var pdfCopy = new PdfCopy(document, outputPdfStream))
                    {
                        document.Open();

                        foreach (var pdfData in pdfDataList)
                        {
                            byte[] compressedPdf = CompressPdf(pdfData);
                            using (var pdfStream = new MemoryStream(compressedPdf))
                            {
                                var reader = new PdfReader(pdfStream);
                                for (var i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, i));
                                }
                            }
                        }
                    }
                }
            }
            CreateRarFile(winrarPath, rarFilePath, outputPath);
        }
        static void CreateRarFile(string winrarPath, string rarFilePath, string fileToAdd)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = winrarPath;
            processInfo.Arguments = $"a \"{rarFilePath}\" -ep1 \"{fileToAdd}\"";

            processInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();

            process.WaitForExit();
        }

        private static string satirAtlat(string metin)
        {
            string[] satirlar = metin.Split("\\n"); // appsettingsin değerini \nlerden kesecek
            string yenimetin = "";

            foreach (string satir in satirlar)
            {
                yenimetin += satir + "\n\n"; // \den sonra gelen satırı alacak sonra tekrar bir daha satır atlatacak
            }

            return yenimetin;
        }
        private static string ReplaceTurkishCharacters(string turkishWord)
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
