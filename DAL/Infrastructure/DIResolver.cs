using System;
using System.Collections.Generic;
using Unity;

namespace DAL.Infrastructure
{
    class DIResolver
    {

        private IUnityContainer _unityContainer;
        public DIResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return new List<Object>();
            }
        }
    }
}
