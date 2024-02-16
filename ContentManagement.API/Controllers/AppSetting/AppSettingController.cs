﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using Microsoft.AspNetCore.Authorization;

namespace ContentManagement.API.Controllers.AppSetting
{
    /// <summary>
    /// App Setting
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingController : BaseController
    {
        public IMediator _mediator { get; set; }
        private readonly ILogger<AppSettingController> _logger;
        /// <summary>
        /// App Setting
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public AppSettingController(
            IMediator mediator,
            ILogger<AppSettingController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Create  Appsetting
        /// </summary>
        /// <param name="addAppSettingCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> AddAppSetting(AddAppSettingCommand addAppSettingCommand)
        {
            var result = await _mediator.Send(addAppSettingCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetAppSetting", new { id = result.Data.Id }, result.Data);
        }
        /// <summary>
        /// Update Exist AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAppSettingCommand"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> UpdateAppSetting(Guid id, UpdateAppSettingCommand updateAppSettingCommand)
        {
            updateAppSettingCommand.Id = id;
            var result = await _mediator.Send(updateAppSettingCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get AppSetting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetAppSetting")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> GetAppSetting(Guid id)
        {
            _logger.LogTrace("GetAppSetting");
            var getAppSettingQuery = new GetAppSettingQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getAppSettingQuery);
            return ReturnFormattedResponse(result);

        }
        /// <summary>
        /// Get AppSetting By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("key/{id}", Name = "GetAppSettingByKey")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> GetAppSettingByKey(string key)
        {
            _logger.LogTrace("GetAppSettingByKey");
            var getAppSettingByKeyQuery = new GetAppSettingByKeyQuery
            {
                Key = key
            };

            var result = await _mediator.Send(getAppSettingByKeyQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get AppSetting By Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        [HttpGet("key", Name = "GetAppSettingJustByKey")]
        [Produces("application/json", "application/xml", Type = typeof(AppSettingDto))]
        public async Task<IActionResult> GetAppSettingJustByKey(string key)
        {
            _logger.LogTrace("GetAppSettingByKey");
            var getAppSettingByKeyQuery = new GetAppSettingByKeyQuery
            {
                Key = key
            };

            var result = await _mediator.Send(getAppSettingByKeyQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get All AppSettings
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAppSettings")]
        [Produces("application/json", "application/xml", Type = typeof(List<AppSettingDto>))]
        public async Task<IActionResult> GetAppSettings()
        {
            var getAllAppSettingQuery = new GetAllAppSettingQuery
            {
            };
            var result = await _mediator.Send(getAllAppSettingQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Delete AppSetting By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAppSetting(Guid Id)
        {
            var deleteAppSettingCommand = new DeleteAppSettingCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(deleteAppSettingCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
