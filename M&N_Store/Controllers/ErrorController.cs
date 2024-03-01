using M_N_Store.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace M_N_Store.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        //[HttpGet("Error")]
        public IActionResult Error (int statusCode)
        {
            return new ObjectResult(new BaseCommonResponse(statusCode));
        }
    }
}
