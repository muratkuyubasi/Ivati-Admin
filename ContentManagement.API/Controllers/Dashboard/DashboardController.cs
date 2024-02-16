using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.MediatR.Queries.Dashboard.HacDashoard;
using System;
using ContentManagement.MediatR.Queries.Dashboard.Debtor;

namespace ContentManagement.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi =true)]
    //[Authorize]
    public class DashboardController : ControllerBase
    {

        public IMediator _mediator { get; set; }

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Active User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetActiveUserCount()
        {
            var getUserQuery = new GetActiveUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Inactive User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInactiveUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetInactiveUserCount()
        {
            var getUserQuery = new GetInactiveUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Total Deleted User Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalDeletedUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetTotalDeletedUserCount()
        {
            var getUserQuery = new GetDeletedUsersCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get Total user count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTotalUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetTotalUserCount()
        {
            var getUserQuery = new GetTotalUserCountQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }


        /// <summary>
        /// Get Died Members Count By Year
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDiedMembersCountByYear")]
        [Produces("application/json", "application/xml", Type = typeof(Object))]
        public async Task<IActionResult> GetDiedMembersCountByYear()
        {
            var getUserQuery = new GetDiedMembersCountByYearQuery { };
            var result = await _mediator.Send(getUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Gets the online users.
        /// </summary>
        /// <param name="userIds">The user ids.</param>
        /// <returns></returns>
        [HttpGet("GetOnlineUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetOnlineUsers()
        {
            var query = new GetOnlineUsersQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Hac Pilgrim Count By Association
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHacPilgrimCountByAssociation")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetHacPilgrimCountByAssociation()
        {
            var query = new GetCountByAssociationQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Hac Men And Women Count 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHacMenAndWomenCount")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetHacMenAndWomenCount()
        {
            var query = new GetGendersCountQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Get Hac Pilgrim Count By Departure Airport
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHacPilgrimCountByDepartureAirport")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetHacPilgrimCountByDepartureAirport()
        {
            var query = new GetCountByAirportsQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Hac Pilgrim Count By Landing Airport
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHacPilgrimCountByLandingAirport")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetHacPilgrimCountByLandingAirport()
        {
            var query = new GetCountByLandingAirportQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Get Umre Pilgrim Count By Association
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUmrePilgrimCountByAssociation")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetUmrePilgrimCountByAssociation()
        {
            var query = new GetUmreCountByAssociationQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Umre Men And Women Count 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUmreMenAndWomenCount")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetUmreMenAndWomenCount()
        {
            var query = new GetUmreGendersCountQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Get Umre Pilgrim Count By Departure Airport
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUmrePilgrimCountByDepartureAirport")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetUmrePilgrimCountByDepartureAirport()
        {
            var query = new GetUmreCountByAirportsQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Umre Pilgrim Count By Landing Airport
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUmrePilgrimCountByLandingAirport")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetUmrePilgrimCountByLandingAirport()
        {
            var query = new GetUmreCountByLandingAirportQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        /// <summary>
        /// Get Unpaid Debtors Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUnpaidDebtorsCount")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetUnpaidDebtorsCount()
        {
            var query = new GetUnpaidDebtorCountQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Paid Debtors Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPaidDebtorsCount")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetPaidDebtorsCount()
        {
            var query = new GetPaidDebtorCountQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Debtors Income By Year
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDebtorsIncomeByYear")]
        [Produces("application/json", "application/xml", Type = typeof(List<object>))]
        public async Task<IActionResult> GetDebtorsIncomeByYear()
        {
            var query = new GetDebtorIncomeByYearsQuery { };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
