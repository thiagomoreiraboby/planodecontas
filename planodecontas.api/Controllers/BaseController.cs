using Microsoft.AspNetCore.Mvc;
using planodecontas.application.Utils;
using System.Net;

namespace planodecontas.api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult ValidResult(GenericResult result)
        {
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
