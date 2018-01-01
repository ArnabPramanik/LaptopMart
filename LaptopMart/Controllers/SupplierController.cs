using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Models;
using LaptopMart.ViewModels;
using System.Diagnostics;
using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    [Authorize(Roles = Roles.RoleSupplier)]
    public class SupplierController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult ShowProducts()
        {
            return View(); //api endpoint will provide suppliers data to datatables plugin.
        }

        public ActionResult ShowFormProduct()
        {
            var viewModel = new ProductFormViewModel()
            {
                Categories = _unitOfWork.Repository<Category>().ReadAll()
             
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductForm", viewModel);
            }

            if (viewModel.Id == 0)
            {
                Product product = Mapper.Map<ProductFormViewModel, Product>(viewModel);
               
                Debug.WriteLine(product.SupplierName);
                _unitOfWork.Repository<Product>().Create(product);
            }
            else
            {
                _unitOfWork.Repository<Product>().Update(Mapper.Map<ProductFormViewModel, Product>(viewModel));
            }

            _unitOfWork.Complete();

            return RedirectToAction("ShowProducts");
        }


        public ActionResult EditProduct(int id)
        {
            var supplierName = User.Identity.Name;

            var productInDb = _unitOfWork.Repository<Product>().ReadProductBySupplier(supplierName,id);
            if (productInDb == null)
            {
                return HttpNotFound();
            }
            return View("ProductForm", Mapper.Map<Product, ProductFormViewModel>(productInDb));
        }


    }
}