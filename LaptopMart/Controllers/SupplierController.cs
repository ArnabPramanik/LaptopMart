using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Models;
using LaptopMart.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
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
                CategoriesDropDownList = _unitOfWork.CategoryRepository.ReadAll(),
                CategoryNames = new List<string>()
               
            
             
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProduct(ProductFormViewModel viewModel, HttpPostedFileBase file)
        {
            if (file == null && viewModel.Id == 0)
            {
                ModelState.AddModelError("Image", "Please upload file");
            }
            else if (file != null)
            {
                if (file.ContentLength > 1 * 1024 * 1024)
                {
                    ModelState.AddModelError("Image", "Image should be less than or equal to 1MB");
                }
                else
                {


                    try
                    {
                        using (var img = Image.FromStream(file.InputStream))
                        {
                            if (!img.RawFormat.Equals(ImageFormat.Png) && !img.RawFormat.Equals(ImageFormat.Jpeg))
                            {
                                ModelState.AddModelError("Image", "Image should be .png or .jpg");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                viewModel.CategoriesDropDownList = _unitOfWork.CategoryRepository.ReadAll();
                return View("ProductForm", viewModel);
            }
            ISet<Category> categories = new HashSet<Category>();

            foreach (int categoryId in viewModel.CategoryIds)
            { 

                categories.Add(_unitOfWork.CategoryRepository.Read(categoryId));
            }


            Product product = Mapper.Map<ProductFormViewModel, Product>(viewModel);
            var user = _unitOfWork.ApplicationUserRepository.Read(User.Identity.GetUserId());
            product.SupplierName = user.Name;
            product.Categories = categories;
            
            

            if (viewModel.Id == 0)
            {
                if (file != null)
                {
                    var prevProduct = _unitOfWork.ProductRepository.ReadLast();
                    if (prevProduct == null)
                    {
                        product.Image = "1" + Path.GetExtension(file.FileName);
                    }
                    else
                    {
                        product.Image = (prevProduct.Id + 1) + Path.GetExtension(file.FileName);
                    }
                   
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                _unitOfWork.ProductRepository.Create(product);
            }
            else
            {
              
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

                Product productInDb = _unitOfWork.ProductRepository.Read(product.Id);
                productInDb.Update(product);
            }

            _unitOfWork.Complete();

            return RedirectToAction("ShowProducts");
        }


        public ActionResult EditProduct(int id)
        {
            var supplierName = _unitOfWork.ApplicationUserRepository.Read(User.Identity.GetUserId()).Name;

            var productInDb = _unitOfWork.ProductRepository.ReadProductBySupplier(supplierName,id);
            if (productInDb == null)
            {
                return HttpNotFound();
            }

            ProductFormViewModel viewModel = Mapper.Map<Product, ProductFormViewModel>(productInDb);
            
            viewModel.CategoriesDropDownList = _unitOfWork.CategoryRepository.ReadAll();
            viewModel.CategoryIds = new List<int>();
            viewModel.CategoryNames = new List<string>();
            foreach(Category category in productInDb.Categories)
            {
               
                viewModel.CategoryIds.Add(category.Id);
                viewModel.CategoryNames.Add(category.Name);
            }
            return View("ProductForm",viewModel);
        }


    }
}