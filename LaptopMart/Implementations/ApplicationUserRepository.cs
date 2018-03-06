using LaptopMart.ApplicationDb;
using LaptopMart.Contracts;
using LaptopMart.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LaptopMart.Implementations
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Create(ApplicationUser applicationUser)
        {
            _context.Users.Add(applicationUser);
        }

        public IEnumerable<ApplicationUser> ReadAll()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser Read(string id)
        {
            return _context.Users.Find(id);
        }

        public void Update(ApplicationUser applicationUser)
        {
            _context.Users.Attach(applicationUser);
            _context.Entry(applicationUser).State = EntityState.Modified;
        }

        public ApplicationUser Delete(string id)
        {
            var applicationUser = Read(id);
            if (applicationUser == null)
            {
                return null;
            }
            if (_context.Entry(applicationUser).State == EntityState.Detached)
                _context.Users.Attach(applicationUser);

            _context.Users.Remove(applicationUser);
            return applicationUser;

        }
    }
}