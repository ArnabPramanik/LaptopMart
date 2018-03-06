using LaptopMart.Contracts;
using LaptopMart.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web;

namespace LaptopMart.Services
{
    public class CartService : ICartService
    {
        public const string CartSessionName = "eCommerceCart";

        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Transfer(HttpContextBase httpContextBase, string userId)
        {
            Cart cartAnon = GetCartView(httpContextBase,null);
            if (cartAnon != null)
            {
                var cartUser = _unitOfWork.CartRepository.ReadByUserId(userId);
                if (cartUser != null)
                {
                    foreach (CartItem cartItemAnon in cartAnon.CartItems)
                    {
                        CartItem cartItemUser =
                            cartUser.CartItems.SingleOrDefault(c => c.ProductId == cartItemAnon.ProductId);
                        if (cartItemUser != null)
                        {
                            cartItemUser.Quantity += cartItemAnon.Quantity;
                        }
                        else
                        {
                            cartUser.CartItems.Add(cartItemAnon);
                        }
                    }

                    _unitOfWork.CartRepository.Delete(cartAnon.Id);
                }
                else
                {
                    cartAnon.UserId = userId;
                   
                    
                }
                HttpCookie cookie = httpContextBase.Request.Cookies.Get(CartSessionName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                httpContextBase.Response.Cookies.Add(cookie);
                _unitOfWork.Complete();
            }
            
        }


        public Cart GetCartView(HttpContextBase httpContextBase, IPrincipal user)
        {
            Cart cart = null;
            if (user != null && user.Identity.IsAuthenticated)
            {
                cart = _unitOfWork.CartRepository.ReadByUserId(user.Identity.GetUserId());
                return cart;
            }
            HttpCookie cookie = httpContextBase.Request.Cookies.Get(CartSessionName);
            
            
            if (cookie != null)
            {
                string strCartId = cookie.Value;
                int cartId = 0;
                if (!string.IsNullOrEmpty(strCartId))
                {
                    cartId = Convert.ToInt32(strCartId);
                    cart = _unitOfWork.CartRepository.Read(cartId);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    httpContextBase.Response.Cookies.Add(cookie);
                }
            }

            return cart;
        }

        public Cart GetCart(HttpRequestMessage httpRequestMessage, bool createIfNull, HttpResponseMessage httpResponseMessage, IPrincipal user)
        {
            Cart cart = null;
            if (user.Identity.IsAuthenticated)
            {
                cart = _unitOfWork.CartRepository.ReadByUserId(user.Identity.GetUserId());
                return cart;
            }
            var cookie = httpRequestMessage.Headers.GetCookies(CartSessionName).FirstOrDefault();
            
            
            if (cookie != null)
            {
                string strCartId = cookie[CartSessionName].Value;
                int cartId = 0;
                if (!string.IsNullOrEmpty(strCartId))
                {
                    cartId = Convert.ToInt32(strCartId);
                    cart = _unitOfWork.CartRepository.Read(cartId);
                    cookie.Expires = DateTimeOffset.Now.AddDays(1);
                    cookie.Domain = null;
                    cookie.Path = "/";

                    httpResponseMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });

                }
                else if (createIfNull)
                {
                    cart = CreateNewCart( httpResponseMessage);
                }
                
            } else if (createIfNull)
            {
                cart = CreateNewCart( httpResponseMessage);
            }
          
            return cart;
        }

        private Cart CreateNewCart(HttpResponseMessage httpResponseMessage)
        {
            Cart cart = new Cart();
            _unitOfWork.CartRepository.Create(cart);
            _unitOfWork.Complete();
            Debug.WriteLine(cart.Id);
            
            var cookie = new CookieHeaderValue(CartSessionName, Convert.ToString(cart.Id));
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = null;
            cookie.Path = "/";

            httpResponseMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return cart;
        }

        public void AddToCart(int productId, HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage,IPrincipal user)
        {
            Cart cart = GetCart(httpRequestMessage, true,httpResponseMessage,user);
            if (cart == null)
            {
                return;
            }
            if(cart.CartItems.Count >= 10)
            {
                httpResponseMessage.Content = new StringContent("Only 10 items in cart");
                return;
            }
            var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == productId);//Lazy loading
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    ProductId = productId,
                    Quantity = 1
                };

                cart.CartItems.Add(cartItem);  //Lazy loading
            }
            else
            {
                if(cartItem.Quantity >= 10)
                {
                    httpResponseMessage.Content = new StringContent("Only 10 items in cart");
                }
                else
                {
                    cartItem.Quantity += 1;
                }
                
            }

            _unitOfWork.Complete();
        }

        public void RemoveFromCart(int productId, HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage, IPrincipal user)
        {
            Cart cart = GetCart(httpRequestMessage, false,httpResponseMessage, user);
            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == productId);
                cart.CartItems.Remove(cartItem);
                _unitOfWork.Complete();
            }
            
        }

    }
}