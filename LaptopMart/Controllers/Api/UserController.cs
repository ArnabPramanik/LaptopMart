using LaptopMart.Contracts;
using LaptopMart.Dtos;
using LaptopMart.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;

namespace LaptopMart.Controllers.Api
{
    [Authorize(Roles = Roles.RoleUser)]
    public class UserController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public UserController(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }


        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage AddToCart(int id)
        {
            
            var resp = new HttpResponseMessage();
            _cartService.AddToCart(id, Request,resp,User);
            Debug.WriteLine(resp);
            return resp;
        }


        

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult UpdateCart([FromBody] CartInfoDto cartInfoDto)
        {
            
            var cartItemInDb = _unitOfWork.CartItemRepository.Read(cartInfoDto.CartItemId);
            if (cartItemInDb == null)
            {
                return BadRequest("Something went wrong");
            }
            cartItemInDb.Quantity = cartInfoDto.Quantity;
            _unitOfWork.Complete();

            

            return Ok();
        }
    }
}
