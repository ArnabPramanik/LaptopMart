using LaptopMart.Contracts;
using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    [Authorize(Roles = Roles.RoleAdmin)]
    public class AdminController : Controller
    {
        // GET: Admin
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult ShowCategories()
        {

            return View();
        }

        public ActionResult ShowFormCategory()
        {
            

            return View();
        }

        
    }
}