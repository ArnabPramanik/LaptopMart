using LaptopMart.ApplicationDb;
using LaptopMart.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaptopMart.Startup))]
namespace LaptopMart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            string superadmin = "SuperAdmin";
            string admin = "Admin";
            string user = "User";
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists(superadmin))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = superadmin;
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var tempuser = new ApplicationUser();
                tempuser.UserName = "Arnab";
                tempuser.Email = "arnab_pmk@yahoo.com";

                string userPWD = "1234567";

                var chkUser = UserManager.Create(tempuser, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(tempuser.Id, superadmin);

                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists(admin))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = admin;
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists(user))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = user;
                roleManager.Create(role);

            }
        }
    }
}
