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
        }
    }
}
