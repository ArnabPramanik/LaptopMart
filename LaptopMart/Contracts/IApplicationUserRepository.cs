using LaptopMart.Models;
using System.Collections.Generic;

namespace LaptopMart.Contracts
{
    public interface IApplicationUserRepository
    {
        void Create(ApplicationUser applicationUser);
        ApplicationUser Delete(string id);
        ApplicationUser Read(string id);
        IEnumerable<ApplicationUser> ReadAll();
        void Update(ApplicationUser applicationUser);
    }
}