using Microsoft.AspNetCore.Mvc;
using Talabat.API.Error;

namespace Talabat.API.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    // this make swagger ignore this api and not create endpoint
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
