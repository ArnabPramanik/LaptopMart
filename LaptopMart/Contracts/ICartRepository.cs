using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface ICartRepository
    {

        void Create(Cart cart);
        Cart Delete(int id);
        Cart Read(int id);
        Cart ReadByUserId(string userId);
        IEnumerable<Cart> ReadAll();
        void Update(Cart cart);
    }
}