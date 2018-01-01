using AutoMapper;
using LaptopMart.Contracts;
using LaptopMart.Dtos;
using LaptopMart.Models;
using System.Linq;
using System.Web.Http;

namespace LaptopMart.Controllers.Api
{
    [Authorize(Roles = Roles.RoleAdmin)]
    public class AdminController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GET api/admin/GetCategories
        [HttpGet]
        public IHttpActionResult GetCategories()
        {
            var categoriesInDb = _unitOfWork.Repository<Category>().ReadAll();

            var categoriesDto = categoriesInDb.Select(Mapper.Map<Category, CategoryDto>);

            return Ok(categoriesDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {

            var deleteResult = _unitOfWork.Repository<Category>().Delete(id);
            _unitOfWork.Complete();
            if (deleteResult == null)
            {

                return NotFound();
            }

            return Ok();
        }


        public IHttpActionResult GetSuppliers()
        {
            var suppliersInDb = _unitOfWork.Repository<Supplier>().ReadAll();
            var suppliersDto = suppliersInDb.Select(Mapper.Map<Supplier, SupplierDto>);
            return Ok(suppliersDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteSupplier(int id)
        {
            var deleteResult = _unitOfWork.Repository<Supplier>().DeleteSupplierByAdmin(id);
            _unitOfWork.Complete();
            
            if (deleteResult == null)
            {
            
                return NotFound();
            }

            return Ok();
        }


    }
}