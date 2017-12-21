using System.Web.Mvc;
using Unity;

namespace DAL.App_Start
{
    class IOCConfiguration
    {

        public static void ConfigureIocUnityContainer()
        {
            //get instance of container
            IUnityContainer container = new UnityContainer();

            //register a type mapping with the container.
            registerService(container);

            DependencyResolver.SetResolver(new DemoUnityDependencyResolver(container));
        }

        private static void registerService(IUnityContainer container)
        {

            //it tell the framework whenever there is a need of instance of ILocalWeatherServiceProvider, provide the instance.
            container.RegisterType<ILocalWeatherServiceProvider, LocalWeatherServiceProvider>();
        }
    }
}
