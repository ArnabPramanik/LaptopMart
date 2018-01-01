using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Dtos;
using LaptopMart.Models;
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
            string supplierName = User.Identity.Name;
            var productsInDb = _unitOfWork.Repository<Product>().ReadAllProductsBySupplier(supplierName);
            var productsDto = productsInDb.Select(Mapper.Map<Product,ProductDto>);
            return Ok(productsDto);

        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = _unitOfWork.Repository<Product>().ReadProductBySupplier(User.Identity.Name, id);
            if (productInDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Repository<Product>().Delete(productInDb.Id);
            return Ok();
        } 
    }
}
