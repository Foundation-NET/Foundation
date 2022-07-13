using System;


namespace Foundation.Annotations {
    [AttributeUsage(AttributeTargets.Class)] 
    public class FactoryAttribute : Attribute {}
    
    [AttributeUsage(AttributeTargets.Method)]
    public class ContractedMethodAttribute : Attribute {}
}