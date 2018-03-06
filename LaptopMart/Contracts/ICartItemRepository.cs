using System.Collections.Generic;
using LaptopMart.Models;

namespace LaptopMart.Contracts
{
    public interface ICartItemRepository
    {
        void Create(CartItem cartItem);
        CartItem Delete(int id);
        CartItem Read(int id);
        IEnumerable<CartItem> ReadAll();
        void Update(CartItem cartItem);
    }
}