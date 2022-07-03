using System.Collections.Generic;
using Foundation.Data.Entity;

namespace Foundation.Data
{
    public class QueryBuilder
    {
        public DataEntity? From;
        public RelationBuilder Relations;
        public FilterBuilder Where;

        public QueryBuilder()
        {
            Relations = new RelationBuilder();
            Where = new FilterBuilder();
        }

        public RelationBuilder.Relation.On On(IColumn left, Operators op, IColumn right)
        {
            RelationBuilder.Relation.On on = new RelationBuilder.Relation.On();
            on.LeftDataColumn = left;
            on.Operator = op;
            on.RightDataColumn = right;
            return on;
        }
        public FilterBuilder.Filter.Condition And(FilterBuilder.Filter.Condition c1, FilterBuilder.Filter.Condition c2)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = c1;
            c.Op = Operators.And;
            c.Right = c2;
            return c;
        }
        public FilterBuilder.Filter.Condition Or(FilterBuilder.Filter.Condition c1, FilterBuilder.Filter.Condition c2)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = c1;
            c.Op = Operators.Or;
            c.Right = c2;
            return c;
        }

        public FilterBuilder.Filter.Condition EqualTo(Object o)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = null;
            c.Op = Operators.Eq;
            c.Right = o;
            return c;
        }
        public FilterBuilder.Filter.Condition NotEqualTo(Object o)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = null;
            c.Op = Operators.NEq;
            c.Right = o;
            return c;
        }
        public FilterBuilder.Filter.Condition MoreThan(Object o)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = null;
            c.Op = Operators.Gt;
            c.Right = o;
            return c;
        }
        public FilterBuilder.Filter.Condition LessThan(Object o)
        {
            FilterBuilder.Filter.Condition c = new FilterBuilder.Filter.Condition();
            c.Left = null;
            c.Op = Operators.Lt;
            c.Right = o;
            return c;
        }
        
        public class RelationBuilder {
            public List<Relation> Joins;

            public RelationBuilder()
            {
                Joins = new List<Relation>();
            }

            public void Add(DataEntity left, DataEntity right, params Relation.On[] ons)
            {
                Relation r = new Relation();
                r.LeftTable = left;
                r.RightTable = right;
                foreach (Relation.On on in ons)
                {
                    r.OnClauses.Add(on);
                }
            }

            public class Relation
            {
                public DataEntity LeftTable;
                public DataEntity RightTable;

                public List<On> OnClauses;
                public Relation()
                {
                    LeftTable = new DataEntity();
                    RightTable = new DataEntity();
                    OnClauses = new List<On>();
                }

                public class On
                {
                    public IColumn? LeftDataColumn;
                    public IColumn? RightDataColumn;
                    public Operators? Operator;
                }
            }
        }
        public class FilterBuilder
        {
            public List<Filter> FilterTreCollection;
            public FilterBuilder()
            {
                FilterTreCollection = new List<Filter>();
            }
            public class Filter
            {
                public IColumn? Column;
                public Condition? ConditionTree;

                public class Condition
                {
                    public Object? Left;
                    public Operators Op;
                    public Object? Right;                    
                }

                
            }

            public void Add(IColumn col, Filter.Condition condition)
            {
                Filter f = new Filter();
                f.Column = col;
                f.ConditionTree = condition;

                this.FilterTreCollection.Add(f);
            }
        }
    }
}