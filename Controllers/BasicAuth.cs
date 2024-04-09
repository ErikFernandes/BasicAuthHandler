using BasicAuthHandler.Properties;
using Microsoft.AspNetCore.Mvc;
using RoundTheCode.BasicAuthentication.Authentication.Basic.Attributes;
using System.Net;

namespace BasicAuthHandler.Controllers
{
    [ApiController]
    [Route("BasicAuth")]
    public class BasicAuth : ControllerBase
    {
        [HttpGet]
        [BasicAuthorization]
        public ActionResult Get()
        {
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "text/html",
                Content = Resources.AcceptPage
            };
        }
    }
}
