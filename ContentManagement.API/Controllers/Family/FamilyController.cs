using Hafiz.Core.Utilities.Mail;
using Hafiz.UI.BackgroudServices;
using HtmlAgilityPack;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Handlers;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FamilyController : BaseController
    {
        private readonly IMediator _mediator;
        private IWebHostEnvironment _appEnvironment;
        private readonly IFamilyRepository _familyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAppSettingRepository _appSettingRepository;

        public FamilyController(IMediator mediator, IWebHostEnvironment webHostEnvironment, IFamilyRepository familyRepository, IUserRepository userRepository, IAppSettingRepository appSettingRepository, IDebtorRepository debtorRepository)
        {
            _mediator = mediator;
            _appEnvironment = webHostEnvironment;
            _familyRepository = familyRepository;
            _userRepository = userRepository;
            _appSettingRepository = appSettingRepository;
        }

        /////<summary>Get Families</summary>
        /////<return></return>
        //[HttpGet("Families")]
        //[Produces("application/json", "application/xml", Type = typeof(List<FamilyDTO>))]
        //public async Task<IActionResult> GetFamilies(int distance)
        //{
        //    var members = new GetAllFamiliesQuery { Distance = distance };
        //    var result = await _mediator.Send(members);
        //    var paginationMetadata = new
        //    {
        //        dstnc = distance,
        //    };
        //    Response.Headers.Add("X-Pagination",
        //        Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
        //    return ReturnFormattedResponse(result);
        //}

        /// <summary>
        /// Funeral Fund Application
        /// </summary>
        /// <param name="userModelCommand"></param>
        /// <returns></returns>
        [HttpPost("FuneralFundApplication")]
        [Produces("application/json", "application/xml", Type = typeof(UserModelDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> FuneralFundApplication(AddUserModelCommand userModelCommand)
        {
            var result = await _mediator.Send(userModelCommand); 
            return Ok(result);
        }

        /// <summary>
        /// Add New Family Member
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddNewFamilyMember")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UserInformationDTO))]
        public async Task<IActionResult> AddNewFamilyMember(AddNewFamilyMemberCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Report Family Member Date of Death
        /// </summary>
        /// <returns></returns>
        [HttpPost("ReportFamilyMemberDateOfDeath")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UserInformationDTO))]
        public async Task<IActionResult> ReportFamilyMemberDateOfDeath(Guid id, DateTime dateOfDeath, string burialPlace, string placeOfDeath)
        {
            var data = new ReportFamilyMemberDateOfDeathCommand { Id = id, DateOfDeath = dateOfDeath, BurialPlace = burialPlace, PlaceOfDeath = placeOfDeath };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        /// <summary>
        /// Parental Divorce
        /// </summary>
        /// <returns></returns>
        [HttpPost("ParentalDivorce")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyDTO))]
        public async Task<IActionResult> ParentalDivorce(Guid familyId)
        {
            var data = new ParentalDivorceCommand { FamilyId = familyId };
            var result = await _mediator.Send(data);
            return Ok(result);
        }


        ///<summary>Check Family Debt</summary>
        ///<return></return>
        [HttpGet("CheckFamilyDebt")]
        [Produces("application/json", "application/xml")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckFamilyDebt(Guid FamilyId)
        {
            var data = new GetFamilyDebtorQuery { FamilyId = FamilyId };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Families Pagination </summary>
        ///<return></return>
        [HttpGet("GetFamiliesPagination/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(FamilyPaginationDto))]
        [AllowAnonymous]
        public async Task<IActionResult> GetFamiliesPagination(int skip, int pageSize, bool? isActive, bool? isDeleted, int memberId, string? search, string? orderBy)
        {
            var datas = new GetAllFamiliesPaginationQuery
            {
                Skip = skip,
                PageSize = pageSize,
                IsActive = isActive,
                IsDeleted = isDeleted,
                MemberId = memberId,
                Search = search,
                OrderBy = orderBy
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

        ///<summary>Get Family Members Pagination </summary>
        ///<return></return>
        [HttpGet("GetFamilyMembersPagination/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(FamilyMemberPaginationDto))]
        [AllowAnonymous]
        public async Task<IActionResult> GetFamilyMembersPagination(int skip, int pageSize, bool? isActive, bool? isDeleted, string? search, string? orderBy, bool? Erkek21Yas, bool? Kadin23Yas, int cityId, bool? AileFertlerineGoreSirala)
        {
            var datas = new GetFamilyMembersPaginationQuery
            {
                Skip = skip,
                PageSize = pageSize,
                IsActive = isActive,
                IsDeleted = isDeleted,
                Search = search,
                OrderBy = orderBy,
                Erkek21Yas = Erkek21Yas,
                Kadin23Yas = Kadin23Yas,
                CityId = cityId,
                AileFertlerineGoreSirala = AileFertlerineGoreSirala
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

        ///<summary>Get Family Detail By Family ID/summary><return></return>
        [HttpGet("GetFamilyDetailByFamilyID")]
        [Produces("application/json", "application/xml", Type = typeof(FamilyDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> GetFamilyDetailByFamilyID(Guid familyId, int? referenceNumber, bool? isActive, bool? isDeleted)
        {
            var data = new GetFamilyByMemberIdQuery { FamilyId = familyId, ReferenceNumber = referenceNumber, IsActive = isActive, IsDeleted = isDeleted };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        ///<summary>Get Member By ID</summary>
        ///<return></return>
        [HttpGet("GetMemberById")]
        [Produces("application/json", "application/xml")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMemberById(Guid id)
        {
            var data = new GetFamilyMemberByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Family By User ID</summary>
        ///<return></return>
        [HttpGet("GetFamilyByUserID")]
        [Produces("application/json", "application/xml")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFamilyByUserID(Guid id)
        {
            var data = new GetFamilyByUserIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }


        ///<summary>Get Deleted Families</summary>
        ///<return></return>
        [HttpGet("GetDeletedFamilies/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(DeletedFamiliesPaginationDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> GetDeletedFamilies(int skip, int pageSize, string? search, string? orderBy)
        {
            var data = new GetDeletedFamiliesQuery { Skip = skip, PageSize = pageSize, Search = search, OrderBy = orderBy};
            var result = await _mediator.Send(data);
            if (result.StatusCode == 204)
            {
                return ReturnFormattedResponse(result);
            }
            if (result.Errors.Count > 0)
            {
                return Ok(result);
            }
            return ReturnFormattedResponse(result);
        }


        ///<summary>Get Deleted All Family's Members</summary>
        ///<return></return>
        [HttpGet("GetDeletedMembers/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(DeletedFamilyMemberWithFamilyDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> GetDeletedMembers(int skip, int pageSize, string? search, string? orderBy)
        {
            var datas = new GetDeletedFamilyMembersQuery
            {
                Skip = skip,
                PageSize = pageSize,
                Search = search,
                OrderBy = orderBy
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
            return ReturnFormattedResponse(result);
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            //return Ok(paginationMetadata);
        }

        ///<summary>Get All Died Members</summary>
        ///<return></return>
        [HttpGet("GetAllDiedMembers")]
        [Produces("application/json", "application/xml")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDiedMembers(int skip, int pageSize, string? search, string? orderBy)
        {
            var datas = new GetDiedUsersQuery
            {
                Skip = skip,
                PageSize = pageSize,
                Search = search,
                OrderBy = orderBy
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
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return ReturnFormattedResponse(result);
        }

        ///// <summary>
        ///// Update Family
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPut("UpdateFamily")]
        //[Authorize]
        //[Produces("application/json", "application/xml", Type = typeof(FamilyDTO))]
        //public async Task<IActionResult> UpdateFamily(UpdateUserModelCommand command)
        //{
        //    var data = await _mediator.Send(command);
        //    return Ok(data);
        //}



        /// <summary>
        /// Transfer Children To Another Family
        /// </summary>
        /// <returns></returns>
        [HttpPut("TransferChildrenToAnotherFamily")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyMemberDTO))]
        public async Task<IActionResult> TransferChildrenToAnotherFamily(Guid familyId, Guid transferFamilyId)
        {
            var data = new TransferChildrenToAnotherFamilyCommand { FamilyId = familyId, TransferFamilyId = transferFamilyId };
            var result = await _mediator.Send(data);
            return Ok(result);
        }


        /// <summary>
        /// Change Head Of The Family
        /// </summary>
        /// <returns></returns>
        [HttpPut("ChangeHeadOfTheFamily")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyMemberDTO))]
        public async Task<IActionResult> ChangeHeadOfTheFamily(Guid familyId, Guid memberId)
        {
            var data = new ChangeHeadOfTheFamilyCommand { MemberId = memberId, FamilyId = familyId };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        /// <summary>
        /// Family Member Death Report
        /// </summary>
        /// <returns></returns>
        [HttpPut("FamilyMemberDeathReport")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UserInformationDTO))]
        public async Task<IActionResult> FamilyMemberDeathReport(Guid id)
        {
            var data = new FamilyMemberDeathReportCommand { UserId = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        /// <summary>
        /// Update Family Member
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateFamilyMember")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UserInformationDTO))]
        public async Task<IActionResult> UpdateFamilyMember(UpdateFamilyMemberCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        ///// <summary>
        ///// Approve Family
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut("ApproveFamily")]
        ////[Authorize]
        //[Produces("application/json", "application/xml", Type = typeof(FamilyDTO))]
        //public async Task<IActionResult> ApproveFamily(int id)
        //{
        //    var data = new ApproveFamilyCommand { MemberId = id };
        //    var result = await _mediator.Send(data);
        //    return Ok(result);
        //}

        /// <summary>
        /// Change Family's Activity Status
        /// </summary>
        /// <returns></returns>
        [HttpPut("ChangeActivityStatus")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyDTO))]
        public async Task<IActionResult> ChangeActivityStatus(Guid familyId)
        {
            var data = new ChangeFamilyActivityStatusCommand { FamilyId = familyId };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        /// <summary>
        /// Update Family's Address
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateFamilyAddress")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddressDTO))]
        public async Task<IActionResult> UpdateFamilyAddress(UpdateFamilyAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        ///// <summary>
        ///// Approve Family Member
        ///// </summary>
        ///// <returns></returns>
        //[HttpPut("Approve")]
        //[Authorize]
        //[Produces("application/json", "application/xml", Type = typeof(UserInformationDTO))]
        //public async Task<IActionResult> Approve(Guid id)
        //{
        //    var data = new ApproveFamilyMemberCommand { Id = id };
        //    var result = await _mediator.Send(data);
        //    return Ok(result);
        //}

        /// <summary>
        /// Delete Family
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        //[Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var data = new DeleteUserModelCommand { FamilyId = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        /// <summary>
        /// Delete Family Member
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMember")]
        //[Authorize]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            var data = new DeleteFamilyMemberCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }


        /// <summary>
        /// Get Debtor PDF
        /// </summary>
        [HttpGet("GetDebtorPDF")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml")]
        public IActionResult ExportContract(Guid familyId, string debtorNumber)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var result = _familyRepository.All
                .Include(x => x.FamilyMembers)
                .ThenInclude(x => x.MemberUser)
                .Include(x => x.Debtors)
                .Include(x => x.Address)
                .Include(x => x.FamilyNotes)
                .Where(x => x.Id == familyId).FirstOrDefault();
            var appsetting = _appSettingRepository.All.Where(x => x.Key == "isvecfaturamessage").FirstOrDefault();
            var arkasayfamesaji = _appSettingRepository.All.Where(x => x.Key == "arkasayfamesaji").FirstOrDefault();
            string loadPath = _appEnvironment.WebRootPath + "/Documents" + "/IDVFatura.pdf";
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
                        var regularFont = BaseFont.CreateFont(_appEnvironment.WebRootPath + "/Documents" + "/calibri-font.ttf", "windows-1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        stamper.FormFlattening = true;
                        //fields.SetField("@@Besök Eller Post Adress@@", result.Address.District + result.Address.Street + result.Address.PostCode);
                        //fields.SetField("@@Tel@@", result.Address.PhoneNumber);
                        //fields.SetField("@@E-Posta@@", result.Address.Email.ToString());
                        //fields.SetField("@@Bankgiro NR@@", "11-1184");
                        string app = appsetting.Value.ToString();
                        string mtn = satirAtlat(app);
                        var innerhtml = ConvertToPlainText(mtn);
                        fields.SetField("BoxInfo", innerhtml);
                        fields.SetField("Faktura Datum", DateTime.Now.Date.ToString("dd-MM-yyyy"));
                        fields.SetField("Ocr", result.MemberId.ToString());
                        fields.SetField("ocrinf", result.MemberId.ToString());
                        if (arkasayfamesaji != null)
                        {
                            string arka = arkasayfamesaji.Value.ToString();
                            string msj = satirAtlat(arka);
                            fields.SetField("arkasayfa", msj);
                        }
                        var amount = "";
                        if (result.Debtors.Count != 0)
                        {
                            fields.SetField("Forfalloddatum", result.Debtors.Where(x => x.DebtorNumber == debtorNumber.ToString()).FirstOrDefault().DueDate.Value.Date.ToString("dd-MM-yyyy"));
                            amount = result.Debtors.Where(x => x.DebtorNumber == debtorNumber).FirstOrDefault().Amount.ToString("0.##");
                            fields.SetField("Kronor", amount);
                        }
                        else
                        {
                            fields.SetField("Forfalloddatum", "-");
                            fields.SetField("Kronor", "0");
                        }

                        fields.SetField("UyeNo", result.MemberId.ToString());
                        fields.SetField("Kendisi", result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper());
                        var spouse = result.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault();
                        if (spouse != null)
                        {
                            if (spouse.MemberUser.FirstName != null)
                            {
                                fields.SetField("Es", result.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + result.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault().MemberUser.LastName.ToUpper());
                            }
                            else fields.SetField("Es", "-");

                        }
                        else { fields.SetField("Es", "-"); }
                        if (result.FamilyMembers.Where(x => x.MemberTypeId == 3).Count() != 0)
                        {
                            int sayac = 0;
                            foreach (var field in result.FamilyMembers.Where(x => x.MemberTypeId == 3))
                            {
                                sayac++;
                                if (sayac == 1)
                                {
                                    fields.SetField("Cocuklari" + sayac, field.MemberUser.FirstName.ToUpper() + " " + field.MemberUser.LastName.ToUpper());
                                }
                                else
                                {
                                    string alan = "Cocuklari" + sayac;
                                    fields.SetField(alan, field.MemberUser.FirstName.ToUpper() + " " + field.MemberUser.LastName.ToUpper());
                                }

                            }
                        }
                        else { fields.SetField("Cocuklari", "-"); }
                        fields.SetField("Betalningavsendare", result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + result.Address.Street + "\n" + result.Address.PostCode + " " + result.Address.District);
                        fields.SetField("Meddalende", result.MemberId.ToString());
                        fields.SetField("Inf", result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName.ToUpper() + " " + result.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName.ToUpper() + "\n" + result.Address.Street + "\n" + result.Address.PostCode + " " + result.Address.District);
                        stamper.Writer.CloseStream = false;
                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;
                        var file = stream.ToArray();
                        var name = ReplaceTurkishCharactersAndTrim(result.Name.ToUpper());
                        path = FileHelper.UploadPDF(file, "PDFs", DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + result.MemberId + "_" + name + "_" + debtorNumber);
                    }
                }
            }
            if (path != null)
            {
                return Ok("PDFs/" + path);
            }
            else return BadRequest();
        }


        /// <summary>
        /// Get Member Card PDF
        /// </summary>
        [HttpGet("GetMemberCardPDF")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml")]
        public IActionResult ExportMemberCard(Guid id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var user = _userRepository.All.Include(x => x.Family).Include(x => x.FamilyMembers).ThenInclude(x => x.Family).Where(x => x.Id == id).FirstOrDefault();
            string loadPath = _appEnvironment.WebRootPath + "/Documents" + "/CFMemberCard.pdf";
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
                        var regularFont = BaseFont.CreateFont(_appEnvironment.WebRootPath + "/Documents" + "/Montserrat-Bold.ttf", "Cp1254", false);
                        fields.AddSubstitutionFont(regularFont);
                        var firstName = ReplaceTurkishCharactersAndTrim(user.FirstName.ToUpper());
                        var lastName = ReplaceTurkishCharactersAndTrim(user.LastName.ToUpper());
                        fields.SetField("name", user.FirstName.ToUpper() + " " + user.LastName.ToUpper());
                        fields.SetField("memberNo", user.FamilyMembers.Where(x => x.MemberUserId == user.Id).FirstOrDefault().Family.MemberId.ToString());
                        stamper.Writer.CloseStream = false;
                        stamper.FormFlattening = true;
                        stamper.Close();
                        stream.Position = 0;
                        var file = stream.ToArray();
                        path = FileHelper.UploadMemberCard(file, "MemberCards", "MemberCard_" + firstName + "_" + lastName);
                    }
                }
            }
            if (path != null)
            {
                return Ok("MemberCards/" + path);
            }
            else return BadRequest();
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
