using LaptopMart.Contracts;
using LaptopMart.Models;
using LaptopMart.Services;
using LaptopMart.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaptopMart.Controllers
{
    [Authorize(Roles = Roles.RoleUser)]
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        public UserController(IUnitOfWork unitOfWork, ICartService cartService, ApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var productsInDb = _unitOfWork.ProductRepository.ReadAll();
            var cart = _cartService.GetCartView(HttpContext, User);
            IEnumerable<CartItemViewModel> viewModel;
            if (cart != null)
            {
                 viewModel = from c in cart.CartItems
                                                           join p in _unitOfWork.ProductRepository.ReadAll() on c.ProductId equals p.Id
                                                           select new CartItemViewModel()
                                                           {
                                                               ProductId = p.Id,
                                                               CartItemId = c.Id,
                                                               Quantity = c.Quantity,
                                                               Name = p.Name,
                                                               Price = p.Price,
                                                               Image = p.Image


                                                           };
            }
            else
            {
                viewModel = new List<CartItemViewModel>();
            }
            ProductsAndCartItemsViewModel productsAndCartItemsViewModel = new ProductsAndCartItemsViewModel()
            {
                Products = productsInDb,
                CartItems = viewModel
            };

                return View(productsAndCartItemsViewModel);
        }

        [AllowAnonymous]
        public ActionResult ProductDetails(int  id)
        {
            var productInDb = _unitOfWork.ProductRepository.Read(id);
            return View(productInDb);

        }


        [AllowAnonymous]
        public ActionResult ViewCart()
        {
            var cart = _cartService.GetCartView(HttpContext,User);
            if (cart != null)
            {
                IEnumerable<CartItemViewModel> viewModel = from c in cart.CartItems join p in _unitOfWork.ProductRepository.ReadAll() on c.ProductId equals p.Id select new CartItemViewModel()
                {
                    ProductId = p.Id,
                    CartItemId = c.Id,
                    Quantity = c.Quantity,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image
                    
                 
                } ;
                return View(viewModel);
            }

            return View(new List<CartItemViewModel>());
        }

        [AllowAnonymous]
        public ActionResult RemoveCartItem(int cartId)
        {
            
            _unitOfWork.CartItemRepository.Delete(cartId);
            _unitOfWork.Complete();

            return RedirectToAction("ViewCart");

        }

        [AllowAnonymous]
        public ActionResult ViewCategories(int id)
        {
            IEnumerable<Category> categoriesInDb = _unitOfWork.CategoryRepository.ReadSpecificCategories(id);

            return View(categoriesInDb);
        }
        

        public ActionResult ViewUserPage()
        {
            var user = _unitOfWork.ApplicationUserRepository.Read(User.Identity.GetUserId());
            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        public ActionResult ShowBecomeASupplierForm()
        {
            BecomeASupplierViewModel viewModel = new BecomeASupplierViewModel();

            return View("BecomeASupplierForm",viewModel);
        }

        public ActionResult BecomeASupplier(BecomeASupplierViewModel viewModel)
        {
            UserManager.RemoveFromRole(User.Identity.GetUserId(), Roles.RoleUser);
            UserManager.AddToRole(User.Identity.GetUserId(), Roles.RoleSupplier);
            Supplier supplier = new Supplier();
            supplier.Name = viewModel.BrandName;
            supplier.Description = viewModel.Description;
            _unitOfWork.SupplierRepository.Create(supplier);
            _unitOfWork.Complete();
            return RedirectToAction("ViewUserPage");

        }
        
    }
}