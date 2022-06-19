using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Foundation.Annotations;

namespace Foundation
{
    /// <summary>
    /// Base class for any object that needs DI
    /// </summary>
    public partial class ObjectBase
    {
        private static IServiceProvider _Resolver;

        /// <summary>
        /// Ensure host exists and populate _Resolver
        /// </summary>
        static ObjectBase()
        {
            if (ApplicationBase._Host == null)
            {
                throw new Exception("Host not created, run ApplicationStart first");
            }
            _Resolver = ApplicationBase._Host.Services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected object? Resolve<T>(Object? o = null)
        {
            bool factory = false;
            T? service = _Resolver.GetService<T>();
            if (service == null)
                return service;
            Type t = service.GetType();
            
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            foreach(Attribute attr in attrs)
            {
                if (attr is FactoryAttribute)
                    factory = true;
            }

            if (factory == true && o != null)
            {
                return ((IFactoryBuilder)service).GetService(o);
            } else if (factory == true && o == null) {
                return ((IFactoryBuilder)service).GetService(null);
            }
            return service;
        }
        /// <summary>
        /// Resolve a required DI service (singlton or transient)
        /// </summary>
        /// <param name="o">Parameter to pass to factory builder if nessasary</param>
        /// <typeparam name="T">Interface to resolve</typeparam>
        /// <returns>Instace of the interface</returns>
        protected object ResolveRequired<T>(Object? o = null) where T : notnull 
        {
            bool factory = false;
            T? service = _Resolver.GetRequiredService<T>();
            Type t = service.GetType();
            
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            foreach(Attribute attr in attrs)
            {
                if (attr is FactoryAttribute)
                    factory = true;
            }

            if (factory == true && o != null)
            {
                return ((IFactoryBuilder)service).GetService(o);
            } else if (factory == true && o == null) {
                return ((IFactoryBuilder)service).GetService(null);
            }
            return service;
        }
        protected IServiceScope CreateScope() => _Resolver.CreateScope();
        protected Object? GetServiceScope<T>(IServiceScope scope, Object? o = null){
            bool factory = false;
            T? service = scope.ServiceProvider.GetService<T>();
            if (service == null)
                return service;
            Type t = service.GetType();
            
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            foreach(Attribute attr in attrs)
            {
                if (attr is FactoryAttribute)
                    factory = true;
            }

            if (factory == true && o != null)
            {
                return ((IFactoryBuilder)service).GetService(o);
            } else if (factory == true && o == null) {
                return ((IFactoryBuilder)service).GetService(null);
            }
            return service;
        }
        protected Object GetRequiredServiceScope<T>(IServiceScope scope, Object? o = null) where T : notnull 
        {
            bool factory = false;
            T? service = scope.ServiceProvider.GetRequiredService<T>();
            Type t = service.GetType();
            
            Attribute[] attrs = Attribute.GetCustomAttributes(t);

            foreach(Attribute attr in attrs)
            {
                if (attr is FactoryAttribute)
                    factory = true;
            }

            if (factory == true && o != null)
            {
                return ((IFactoryBuilder)service).GetService(o);
            } else if (factory == true && o == null) {
                return ((IFactoryBuilder)service).GetService(null);
            }
            return service;
        }
    }
}