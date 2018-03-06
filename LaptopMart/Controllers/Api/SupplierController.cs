using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Dtos;
using LaptopMart.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace LaptopMart.Controllers.Api
{
    [Authorize(Roles = Roles.RoleSupplier)]
    public class SupplierController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            string supplierName = _unitOfWork.ApplicationUserRepository.Read(User.Identity.GetUserId()).Name;
            var productsInDb = _unitOfWork.ProductRepository.ReadAllProductsBySupplier(supplierName);
            var productsDto = productsInDb.Select(Mapper.Map<Product,ProductDto>);
            return Ok(productsDto);

        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _unitOfWork.ProductRepository.ReadProductBySupplier(_unitOfWork.ApplicationUserRepository.Read(User.Identity.GetUserId()).Name, id);
            if (productInDb == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(productInDb.Id);
            _unitOfWork.Complete();
            return Ok();
        } 
    }
}
