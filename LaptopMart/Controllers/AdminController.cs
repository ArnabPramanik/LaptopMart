using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("/Views/Shared/Index.cshtml");
        }
    }
}