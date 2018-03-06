using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(Cart cart)
        {
            _context.CartTable.Add(cart);
        }

        public Cart Delete(int id)
        {
            var cart = Read(id);
            if (cart == null)
            {
                return null;
            }
            if (_context.Entry(cart).State == EntityState.Detached)
                _context.CartTable.Attach(cart);

            _context.CartTable.Remove(cart);
            return cart;
        }

        public Cart Read(int id)
        {
            return _context.CartTable.Find(id);
        }

        public Cart ReadByUserId(string userId)
        {
            return _context.CartTable.SingleOrDefault(c => c.UserId == userId);
        }

        public IEnumerable<Cart> ReadAll()
        {
            return _context.CartTable;
        }

        public void Update(Cart cart)
        {
            _context.CartTable.Attach(cart);
            _context.Entry(cart).State = EntityState.Modified; ;
        }
    }
}