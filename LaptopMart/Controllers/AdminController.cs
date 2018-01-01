using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Models;
using LaptopMart.ViewModels;
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
            //View content rendered by datatable supplied by api endpoint.
            return View();
        }

        public ActionResult ShowFormCategory()
        {
            
           
               
            CategoryFormViewModel viewModel = new CategoryFormViewModel();
            
            

            return View("CategoryForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCategory(CategoryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CategoryForm", viewModel);
            }

            Category category = new Category();
            category.SetObject(viewModel);
            if (category.Id == 0)
            {
                _unitOfWork.Repository<Category>().Create(category);
            }
            else
            {
                _unitOfWork.Repository<Category>().Update(category);
            }

            _unitOfWork.Complete();
            return RedirectToAction("ShowCategories");//Redirect otherwise form will resubmit on refresh as the url remains the same
        }


        public ActionResult EditCategory(int id)
        {
            var categoryInDb = _unitOfWork.Repository<Category>().Read(id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }


            return View("CategoryForm", Mapper.Map<Category, CategoryFormViewModel>(categoryInDb));

        }


        public ActionResult ShowSuppliers()
        {
            return View();
        }

      

    }


}