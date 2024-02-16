using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Commands.Debtor;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Debtor
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtorController : BaseController
    {
        private readonly IMediator _mediator;
        private IWebHostEnvironment _appEnvironment;

        public DebtorController(IMediator mediator, IWebHostEnvironment appEnvironment)
        {
            _mediator = mediator;
            _appEnvironment = appEnvironment;
        }


        ///<summary>Add Debtor</summary><return></return>
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddDebtorCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///// <summary>
        ///// Get All Debtors Pagination
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetAllDebtorsPagination")]
        //[Produces("application/json", "application/xml", Type = typeof(List<DebtorDTO>))]
        //public async Task<IActionResult> GetAllDebtors(int skip, int pageSize, int year, bool? IsPayment, string search/*, string orderBy */)
        //{
        //    var data = new GetAllDebtorsPaginationQuery { Skip =skip , Year = year, IsPayment = IsPayment, PageSize = pageSize, Search = search
        //        /*, OrderBy = orderBy*/};
        //    var result = await _mediator.Send(data);
        //    return ReturnFormattedResponse(result);
        //}

        [HttpGet("GetAllFDebtorsPagination/{skip}/{pagesize}")]
        [Produces("application/json", "application/xml", Type = typeof(List<DebtorSimpleDTO>))]
        public async Task<IActionResult> GetAllFDebtors(int skip, int pageSize, int year, string? IsPayment, string? search, string? orderBy, int cityId)
        {
            var data = new GetAllFDebtorsPaginationQuery
            {
                Skip = skip,
                Year = year,
                IsPayment = IsPayment,
                PageSize = pageSize,
                Search = search,
                OrderBy = orderBy,
                CityId = cityId
            };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Income By City ID </summary><returns></returns>
        [HttpGet("GetIncomeByCityID/{cityId}")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetIncomeByCity(int cityId)
        {
            var query = new GetIncomeByCityQuery { CityId = cityId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        ///// <summary>
        ///// Unpaid Dues
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("UnpaidDues")]
        //[Produces("application/json", "application/xml", Type = typeof(List<DebtorDTO>))]
        //public async Task<IActionResult> UnpaidDues(string year)
        //{
        //    var data = new GetUnpaidDuesByYearQuery { Year = year };
        //    var result = await _mediator.Send(data);
        //    return ReturnFormattedResponse(result);
        //}     

        ///// <summary>
        ///// Paid Dues
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("PaidDues")]
        //[Produces("application/json", "application/xml", Type = typeof(List<DebtorDTO>))]
        //public async Task<IActionResult> PaidDues(string year)
        //{
        //    var data = new GetPaidDuesByYearQuery { Year = year };
        //    var result = await _mediator.Send(data);
        //    return ReturnFormattedResponse(result);
        //}

        ///<summary> Family Debt Payment</summary>
        /// <returns></returns>
        [HttpPut("FamilyDebtPayment")]
        [Produces("application/json", "application/xml", Type = typeof(DebtorDTO))]
        [Authorize]
        public async Task<IActionResult> FamilyDebtPayment(Guid familyId, string debtorNumber)
        {
            var data = new UpdateDebtorCommand { FamilyId = familyId , DebtorNumber = debtorNumber};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Update Debtor Information </summary>
        /// <returns></returns>
        [HttpPut("UpdateDebtorInformation")]
        [Produces("application/json", "application/xml", Type = typeof(DebtorDTO))]
        [Authorize]
        public async Task<IActionResult> UpdateDebtorInformation(UpdateDebtorsCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Set 150 Kron For Penalty Fee
        /// </summary>
        /// <returns></returns>
        [HttpPost("SetPenaltyFee")]
        [Authorize]
        public async Task<IActionResult> SetPenaltyFee()
        {
            var data = new SetPenaltyFeePaymentQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Debtor 
        /// </summary>
        /// <param name="familyId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDebtorById")]
        [Authorize]
        public async Task<IActionResult> DeleteDebtorById(Guid familyId, string debtorNumber)
        {
            var data = new DeleteDebtorCommand { FamilyId = familyId, DebtorNumber = debtorNumber };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
        /// <summary>
        /// Get Income By Year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("GetIncomeByYear")]
        [Produces("application/json", "application/xml", Type = typeof(List<DebtorDTO>))]
        public async Task<IActionResult> GetIncomeByYear(string year)
        {
            var data = new GetAllIncomeByYearQuery { Year = year};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Debtor By Year
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDebtorByYear")]
        [Produces("application/json", "application/xml", Type = typeof(List<DebtorByYearDTO>))]
        public async Task<IActionResult> GetDebtorByYear()
        {
            var data = new GetDebtorByYearQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Yillik Fatura Bas
        /// </summary>
        /// <returns></returns>
        [HttpGet("YillikFaturaBas")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(bool))]
        public async Task<IActionResult> YillikFaturaBas(string durum)
        {
            var data = new YillikFaturaCommand { Durum = durum };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Save Debtors to Folder
        /// </summary>
        /// <returns></returns>
        [HttpGet("SaveDebtorsToFolder")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(bool))]
        public async Task<IActionResult> SaveDebtorsToFolder(bool approve, int year)
        {
            var data = new PrintAllDebtorsToFileCommand { Approve = approve, Year = year};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Check Debtor PDF File </summary> <returns></returns>
        [HttpGet("CheckDebtorFile")]
        [Produces("application/json", "application/xml", Type = typeof(string))]
        public async Task<IActionResult> CheckDebtorFile(int year)
        {
            var wwwroot = _appEnvironment.WebRootPath;
            var fileName = year + "_Debtors.rar";
            var file = Path.Combine(wwwroot, "Debtors", fileName);
            var durum = System.IO.File.Exists(file);
            if (durum)
            {
                return Ok("Debtors/" + fileName);
            }
            else return Ok(false);
        }

        [HttpGet("GetPdftoPath")]
        [AllowAnonymous]
        public IActionResult GetPdf(string path)
        {
            var stream = System.IO.File.OpenRead(_appEnvironment.WebRootPath + "/" + path);
            return File(stream, "application/pdf");
        }
    }
}
