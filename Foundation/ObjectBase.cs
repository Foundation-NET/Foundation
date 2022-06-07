using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Foundation.Annotations;

namespace Foundation
{
    public partial class ObjectBase
    {
        private static IServiceProvider _Resolver;

        static ObjectBase()
        {
            if (ApplicationBase._Host == null)
            {
                throw new Exception("Host not created, run ApplicationStart first");
            }
            _Resolver = ApplicationBase._Host.Services;
        }

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