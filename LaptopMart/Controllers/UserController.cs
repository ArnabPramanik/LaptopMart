using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("/Views/Shared/Index.cshtml");
        }
    }
}