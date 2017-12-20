using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("/Views/Shared/Index.cshtml");
        }
    }
}