using BasicAuthHandler.Global;
using BasicAuthHandler.Models;
using Microsoft.AspNetCore.Mvc;
using RoundTheCode.BasicAuthentication.Authentication.Basic.Attributes;
using System.Net;
using System.Text.Json;

namespace BasicAuthHandler.Controllers
{
    [ApiController]
    [BasicAuthorization]
    [Route("BasicAuth")]
    public class BasicAuth : ControllerBase
    {
        
        [HttpGet]
        public ActionResult Get()
        {
            ClientAcceptModel clientAcceptModel =
                GlobalVariables.ClientsAccept[Request.HttpContext.Connection.RemoteIpAddress!];

            Console.WriteLine($"GET: Username: {clientAcceptModel.Username} Key: {clientAcceptModel.Key}");

            //The client will only reach the Controller if the authorization is successful.
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "text",
                Content = "Access Granted!"
            };
        }


        [HttpPost]
        public ActionResult Post([FromBody] JsonElement json)
        {
            ClientAcceptModel clientAcceptModel =
                GlobalVariables.ClientsAccept[Request.HttpContext.Connection.RemoteIpAddress!];

            Console.WriteLine($"POST: Username: {clientAcceptModel.Username} Key: {clientAcceptModel.Key}");
            Console.WriteLine($"BODY: {json}");

            //The client will only reach the Controller if the authorization is successful.
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "text",
                Content = "Access Granted!"
            };


        }
    }
}
