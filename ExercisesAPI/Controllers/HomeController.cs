using Microsoft.AspNetCore.Mvc;
namespace ExercisesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "server started";
        }
    }
}