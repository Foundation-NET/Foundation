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

        protected string? FunctionRef;
        private static IServiceProvider _Resolver;

        /// <summary>
        /// Ensure host exists and populate _Resolver
        /// </summary>
        static ObjectBase()
        {
            if (ApplicationBase._Host == null)
                throw new Exception("Host not created, run ApplicationStart first");
            _Resolver = ApplicationBase._Host.Services;
        }

        /// <summary>
        /// Create an instance of <typeparamref name="T"/> pass <see langword="async"/> paramaters to matching costructor length
        /// </summary>
        /// <typeparam name="T">Any class to instanttiate.</typeparam>
        /// <returns></returns>
        protected T Launch<T>(params Object[] o) 
        {
            // Create empty constructor if no params
            if (o.Length==0)
                return Activator.CreateInstance<T>();
            foreach(var v in typeof(T).GetConstructors())
            {
                var v1 = v.GetParameters();
                // Pass params to constructor if exists
                if (v1.Length != 0 && v1.Length == o.Length)
                    return (T)v.Invoke(o);
            }
            // Create empty constructor as default
            return Activator.CreateInstance<T>();
        }

        public override string ToString()
        {
            return "(" + ""??FunctionRef +"):" + this.GetType().FullName; 
        }

        /// <summary>
        /// Resolve a DI service
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
        protected Scope CreateScope() => new Scope();
        

        protected class Scope
        {
            private IServiceScope _scope;
            public Scope()
            {
                _scope = _Resolver.CreateScope();
            }

            public Object? GetServiceScope<T>(Object? o = null)
            {
                bool factory = false;
                T? service = _scope.ServiceProvider.GetService<T>();
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

            public Object GetRequiredServiceScope<T>(Object? o = null) where T : notnull 
            {
                bool factory = false;
                T? service = _scope.ServiceProvider.GetRequiredService<T>();
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
}