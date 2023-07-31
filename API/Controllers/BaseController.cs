using Application.Response;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult GetApiResponse<T>(ApiResponse<T> obj) where T : class
        {
            if (obj.IsSuccessStatusCode && obj.HttpStatusCode == System.Net.HttpStatusCode.NoContent)
                return StatusCode(obj.StatusCode);

            if (obj.IsSuccessStatusCode)
                return StatusCode(obj.StatusCode, obj.Data);

            return StatusCode(obj.StatusCode, obj.Errors);
        }
    }
}
