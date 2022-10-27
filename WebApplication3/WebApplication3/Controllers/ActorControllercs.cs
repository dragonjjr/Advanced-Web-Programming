using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Serilog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.WebSockets;
using WebApplication3.DBContext;
using WebApplication3.Model;
using WebApplication3.Services;
using static WebApplication3.Model.General;

namespace WebApplication3.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActorControllercs:ControllerBase
    {
        private ActorServices actorServices;
        private readonly IConfiguration _configuration;
        public ActorControllercs(IConfiguration configuration)
        {
            actorServices = new ActorServices();
            this._configuration = configuration;
        }

        /// <summary>
        /// API lấy danh sách actor
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //Log.Information("Get all actor");
            ResponMessages result = new ResponMessages();
            List<Actor> actors = actorServices.GetAll();
            if (actors.Count > 0)
            {
                result.Status = 200;
                result.messages = "Get Actor infor success!";
                result.Data = actors;
                return new JsonResult(result);
            }
            else
            {
                result.Status = 404;
                result.messages = "Actor not found";
                return new JsonResult(result);
            }
        }

        /// <summary>
        /// tạo mới actor
        /// </summary>
        /// <param name="actor">mô tả actor</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item #1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost("CreateActor")]
        public IActionResult Create([FromBody] ActorModel actor)
        {
            ResponMessages result = new ResponMessages();
            var rs = actorServices.Create(actor);
            if (rs != null)
            {
                result.Status = 201;
                result.messages = "An Actor created!";
                result.Data = rs;
                return new JsonResult(result);
            }
            else
            {
                result.Status = 0;
                result.messages = "An Actor create fail!";
                return new JsonResult(result);
            }
        }
    }
}
