using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace LaptopMart.Implementations
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(CartItem cartItem)
        {
            _context.CartItemTable.Add(cartItem);
        }

        public CartItem Delete(int id)
        {
            var cartItem = Read(id);
            if (cartItem == null)
            {
                return null;
            }
            if (_context.Entry(cartItem).State == EntityState.Detached)
                _context.CartItemTable.Attach(cartItem);

            _context.CartItemTable.Remove(cartItem);
            return cartItem;
        }

        public CartItem Read(int id)
        {
            return _context.CartItemTable.Find(id);
        }

        public IEnumerable<CartItem> ReadAll()
        {
            return _context.CartItemTable;
        }

        public void Update(CartItem cartItem)
        {
            _context.CartItemTable.Attach(cartItem);
            _context.Entry(cartItem).State = EntityState.Modified; ;
        }
    }
}