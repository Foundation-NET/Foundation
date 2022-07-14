using System;
using Foundation.Data.Types;
using System.Reflection;


namespace Foundation.Data.Entity {
    [AttributeUsage(AttributeTargets.Field)] 
    public class PrimaryKeyAttribute : Attribute {
        public bool Compound;
        public PrimaryKeyAttribute(bool compound)
        {
            Compound = compound;
        }
    }
    [AttributeUsage(AttributeTargets.Field)] 
    public class ForeignKeyAttribute : Attribute {
        public bool Compound;
        public int Id;
        public Type Table;
        public Relationship Type;
        public ForeignKeyAttribute(Type table, bool compound)
        {
            Table = table;
            Compound = compound;
            Type = Relationship.ManyToMany;
        }
    }
    public enum Relationship
    {
        OneToOne,
        OneToMany,
        ManyToMany
    }
}