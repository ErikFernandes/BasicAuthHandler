using BasicAuthHandler.Global;
using BasicAuthHandler.Models;
using Microsoft.AspNetCore.Mvc;
using RoundTheCode.BasicAuthentication.Authentication.Basic.Attributes;
using System.Net;

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

            Console.WriteLine($"Username: {clientAcceptModel.Username} Key: {clientAcceptModel.Key}");

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
