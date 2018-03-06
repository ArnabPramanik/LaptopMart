using LaptopMart.Models;
using System.Net.Http;
using System.Security.Principal;
using System.Web;

namespace LaptopMart.Services
{
    public interface ICartService
    {
        void AddToCart(int productId, HttpRequestMessage httpRequestBase, HttpResponseMessage httpResponseMessage, IPrincipal user);
        Cart GetCart(HttpRequestMessage httpRequestMessage, bool createIfNull,HttpResponseMessage httpResponseMessage, IPrincipal user);
        Cart GetCartView(HttpContextBase httpContextBase,IPrincipal user);
        void Transfer(HttpContextBase httpContextBase, string userId);
        void RemoveFromCart(int productId, HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, IPrincipal user);
    }
}