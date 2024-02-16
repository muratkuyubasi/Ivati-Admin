using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HacFormController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHacRepository _hacRepository;
        private IWebHostEnvironment _appEnvironment;
        public HacFormController(IMediator mediator, IHacRepository hacRepository, IWebHostEnvironment appEnvironment)
        {
            _mediator = mediator;
            _hacRepository = hacRepository;
            _appEnvironment = appEnvironment;
        }

        ///<summary>Hajj registration form</summary>
        ///<return></return>
        [HttpPost("HajjRegistrationForm")]
        [Produces("application/json", "application/xml", Type = typeof(HacFormDTO))]
        public async Task<IActionResult> RegistrationForm(AddHacFormCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Get All Hajj Candidates</summary><return></return>
        [HttpGet("GetAllHajjCandidates/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(HacPaginationDto))]
        public async Task<IActionResult> GetAllCandidates(int skip, int pageSize, string? search, int? periodId)
        {
            var datas = new GetHacPilgrimCandidateQuery
            {
                Skip = skip,
                PageSize = pageSize,
                Search = search,
                PeriodId = periodId
            };
            var result = await _mediator.Send(datas);
            if (result.StatusCode == 204)
            {
                return ReturnFormattedResponse(result);
            }
            if (result.Errors.Count > 0)
            {
                return Ok(result);
            }
            var paginationMetadata = new
            {
                TotalCount = result.Data.TotalCount,
                Skip = result.Data.Skip,
                PageSize = result.Data.PageSize,
                Data = result.Data
            };
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(paginationMetadata);
        }

        ///<summary>Get Hajj Candidate By Id</summary>
        ///<return></return>
        [HttpGet("GetCandidateById")]
        [Produces("application/json", "application/xml", Type = typeof(HacFormDTO))]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var datas = new GetHacCandidateByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Candidate By Id </summary>
        ///<return></return>
        [HttpPut("UpdateCandidateById")]
        [Produces("application/json", "application/xml", Type = typeof(HacFormDTO))]
        [Authorize]
        public async Task<IActionResult> UpdateCandidateById(UpdateHacFormCommand updateHacFormCommand)
        {
            var result = await _mediator.Send(updateHacFormCommand);
            return Ok(result);
        }

        ///<summary>Delete Application By Id</summary><return></return>
        [HttpDelete("DeleteById")]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            var data = new DeleteHacFormCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
        //[HttpPost]
        //public async Task<IActionResult> Post([FromForm] IFormFile file)
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        var fileContents = stream.ToArray();

        //        var result = await _mediator.Send(new AddHacFormCommand/*(fileContents)*/());

        //        return Ok(result);
        //    }
        //}

        ///<summary> Get Hac's Member Card PDF </summary><returns></returns>
        [HttpGet("GetHACMemberCardPDF")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml")]
        public IActionResult GetHACMemberCardPDF(int id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var user = _hacRepository.All.Include(x => x.ClosestAssociation).Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                string loadPath = _appEnvironment.WebRootPath + "/Documents" + "/HU_UyeKart.pdf";
                string picturePath = "";
                if (user.HeadshotPicture != null)
                {

                    string getpicture = _appEnvironment.WebRootPath + "/Pictures" + user.HeadshotPicture.Replace("/Pictures", "");
                    if (!System.IO.File.Exists(getpicture))
                    {
                        picturePath = "/Documents/nogender.jpg";
                    }
                    else picturePath = user.HeadshotPicture;
                    //picturePath = "/Pictures/HacPassportPictures/65077097674.png";
                }
                string path;
                //var addicted = JsonConvert.DeserializeObject<AddictedEnum[]>(result.IsAddicted);
                using (var reader = new PdfReader(loadPath))
                {
                    using (var stream = new MemoryStream())
                    {
                        using (var stamper = new PdfStamper(reader, stream))
                        {
                            AcroFields fields = stamper.AcroFields;
                            fields.GenerateAppearances = true;
                            var regularFont = BaseFont.CreateFont(_appEnvironment.WebRootPath + "/Documents" + "/Montserrat-ExtraBold.ttf", "Cp1254", false);
                            fields.AddSubstitutionFont(regularFont);
                            var firstName = ReplaceTurkishCharactersAndTrim(user.Name.ToUpper());
                            var lastName = ReplaceTurkishCharactersAndTrim(user.Surname.ToUpper());
                            fields.SetField("AdSoyad", user.Name + " " + user.Surname);
                            fields.SetField("PassNo", user.PassportNumber);
                            fields.SetField("Org", user.ClosestAssociation.Name);

                            float x = 189.5f; // X koordinatı
                            float y = 53.5f; // Y koordinatı
                            float width = 55.5f; // Resmin genişliği
                            float height = 58.5f; // Resmin yüksekliği

                            Image image = Image.GetInstance(_appEnvironment.WebRootPath + picturePath);
                            if (image != null)
                            {
                                image.ScaleAbsolute(width, height);
                            }

                            PdfContentByte pdfContentByte = stamper.GetOverContent(1); // 1. sayfa için
                            image.SetAbsolutePosition(x, y);
                            pdfContentByte.AddImage(image);

                            //var svg = SvgDocument.Open(damgaPath);
                            //var ad = new iText.Layout.Element.Image(ImageDataFactory.Create(damgaPath));
                            float damga_x = 200.5f; // X koordinatı
                            float damga_y = 13.5f; // Y koordinatı
                            float damga_width = 35.5f; // Resmin genişliği
                            float damga_height = 38.5f; // Resmin yüksekliği

                            string damgapng = _appEnvironment.WebRootPath + "/Documents" + "/HU_UyeKart.png";
                            Image damgaImage = Image.GetInstance(damgapng);
                            if (damgaImage != null)
                            {
                                damgaImage.ScaleAbsolute(damga_width, damga_height);
                            }
                            PdfContentByte damgaPdfContentByte = stamper.GetOverContent(1); // 1. sayfa için
                            damgaImage.SetAbsolutePosition(damga_x, damga_y);
                            damgaPdfContentByte.AddImage(damgaImage);

                            //var pp = Path.Combine(picturePath);
                            //var llal = _appEnvironment.WebRootFileProvider.GetFileInfo(picturePath)?.PhysicalPath;
                            //fields.SetField("Picture", llal);
                            stamper.Writer.CloseStream = false;
                            stamper.FormFlattening = true;
                            stamper.Close();
                            stream.Position = 0;
                            var file = stream.ToArray();
                            path = FileHelper.UploadMemberCard(file, "MemberCards/Hac", "MemberCard_" + firstName + "_" + lastName);
                        }
                    }
                }
                return Ok("MemberCards/Hac/" + path);
            }
            else return Ok(false);
        }

        public static string ReplaceTurkishCharactersAndTrim(string turkishWord)
        {
            string source = "ığüşöçĞÜŞİÖÇ";
            string destination = "igusocGUSIOC";

            string result = "";
            if (turkishWord.Contains(' '))
            {
                result = turkishWord.Replace(" ", "");
            }
            else result = turkishWord;

            for (int i = 0; i < source.Length; i++)
            {
                result = result.Replace(source[i], destination[i]);
            }

            return result;
        }
    }

}
