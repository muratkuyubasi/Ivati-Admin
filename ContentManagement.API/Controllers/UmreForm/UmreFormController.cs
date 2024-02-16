using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ContentManagement.Repository;
using iTextSharp.text.pdf;
using System.Text;
using iTextSharp.text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UmreFormController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        private readonly IUmreFormRepository _formRepository;

        public UmreFormController(IMediator mediator, IWebHostEnvironment env, IUmreFormRepository formRepository)
        {
            _mediator = mediator;
            _env = env;
            _formRepository = formRepository;
        }

        ///<summary>Umrah registration form</summary>
        ///<return></return>
        [HttpPost("UmrahRegistrationForm")]
        [Produces("application/json", "application/xml", Type = typeof(UmreFormDTO))]
        public async Task<IActionResult> RegistrationForm(AddUmreFormCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Get Umrah Candidate By Id</summary>
        ///<return></return>
        [HttpGet("GetUmrahCandidateById")]
        [Produces("application/json", "application/xml", Type = typeof(UmreFormDTO))]
        public async Task<IActionResult> GetUmrahCandidateById(int id)
        {
            var data = new GetUmreCandidateByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get All Umrah Candidates</summary><return></return>
        [HttpGet("GetAllUmrahCandidates/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(UmrePaginationDto))]
        public async Task<IActionResult> GetAllCandidates(int skip, int pageSize, string? search, int? periodId)
        {
            var datas = new GetPilgrimCandidateQuery
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

        [HttpPost("PostPicture")]
        [Authorize]
        [Produces("application/json", "application/xml")]
        public IActionResult PostPicture([FromForm] FileUploadDTO fileUploadDTO)
        {
            if (fileUploadDTO != null)
            {
                fileUploadDTO.RootPath = _env.WebRootPath;
                fileUploadDTO.FolderName = "Pictures";
                return Ok(FileHelper.UploadDocument(fileUploadDTO));
            }
            else
                return BadRequest();
        }

        [HttpPost("PostPicture2")]
        [Authorize]
        [Produces("application/json", "application/xml")]
        public IActionResult PostPicture2([FromForm] FilePassportPictureDTO fileUploadDTO)
        {
            if (fileUploadDTO != null)
            {
                fileUploadDTO.RootPath = _env.WebRootPath;
                if (fileUploadDTO.Type == 1)
                {
                    fileUploadDTO.FolderName = "Pictures/HacPassportPictures";
                }
                else fileUploadDTO.FolderName = "Pictures/UmrePassportPictures";
                return Ok(FileHelper.UploadPassportPicture(fileUploadDTO));
            }
            else
                return BadRequest();
        }

        ///<summary>Update Candidate By Id </summary>
        ///<return></return>
        [HttpPut("UpdateCandidateById")]
        [Produces("application/json", "application/xml", Type = typeof(UmreFormDTO))]
        [Authorize]
        public async Task<IActionResult> UpdateUmrahCandidateById(UpdateUmreFormCommand updateUmreFormCommand)
        {
            var result = await _mediator.Send(updateUmreFormCommand);
            return Ok(result);
        }

        ///<summary>Delete Application By Id</summary><return></return>
        [HttpDelete("DeleteById")]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            var data = new DeleteUmreFormCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        ///<summary> Get Umrah's Member Card PDF </summary><returns></returns>
        [HttpGet("GetUmrahMemberCardPDF")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml")]
        public IActionResult GetUmrahMemberCardPDF(int id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var user = _formRepository.All.Include(x => x.ClosestAssociation).Where(x => x.Id == id).FirstOrDefault();
            if (user != null)
            {
                string loadPath = _env.WebRootPath + "/Documents" + "/HU_UyeKart.pdf";
                string picturePath = "";
                if (user.PassportPicture != null)
                {
                    string getpicture = _env.WebRootPath + "/Pictures" + user.HeadshotPicture.Replace("/Pictures", "");
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
                            var regularFont = BaseFont.CreateFont(_env.WebRootPath + "/Documents" + "/Montserrat-ExtraBold.ttf", "Cp1254", false);
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

                            Image image = Image.GetInstance(_env.WebRootPath + picturePath);
                            if (image != null)
                            {
                                image.ScaleAbsolute(width, height);
                            }
                            PdfContentByte pdfContentByte = stamper.GetOverContent(1); // 1. sayfa için
                            image.SetAbsolutePosition(x, y);
                            pdfContentByte.AddImage(image);

                            float damga_x = 200.5f; // X koordinatı
                            float damga_y = 13.5f; // Y koordinatı
                            float damga_width = 35.5f; // Resmin genişliği
                            float damga_height = 38.5f; // Resmin yüksekliği

                            string damgapng = _env.WebRootPath + "/Documents" + "/HU_UyeKart.png";
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
                            path = FileHelper.UploadMemberCard(file, "MemberCards/Umre", "MemberCard_" + firstName + "_" + lastName);
                        }
                    }
                }
                return Ok("MemberCards/Umre/" + path);
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
