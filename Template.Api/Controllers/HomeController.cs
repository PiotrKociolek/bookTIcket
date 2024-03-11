using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Return API info.
        /// </summary>
        [HttpGet]
        public string Get()
        {
            return "Template.Api";
        }

        /// <summary>
        /// Return API version.
        /// </summary>
        [HttpGet("version")]
        public string Version()
        {
            return "0.0.0";
        }
    }
}