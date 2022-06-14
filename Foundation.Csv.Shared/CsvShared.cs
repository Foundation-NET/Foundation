using System.Collections.Generic;

namespace Foundation.Csv.Shared 
{
    public class CsvShared
    {
        public Dictionary<string, Type> Headers;
        protected Row _template;
        protected char _sep;
        public CsvShared()
        :this(',') {}
        public CsvShared(char sep)
        {
            _sep = sep;
            _template = new Row();
            Headers = new Dictionary<string, Type>();
        }
        public void SetHeader(string header)
        {
            string[] split = header.Split('|');

            foreach(string row_heading in split)
            {
                int op = row_heading.IndexOf('(');
                int lp = row_heading.IndexOf(')');

                string TypeDef = row_heading.Substring(op + 1, (lp-op - 1));
                string Name = row_heading.Substring(0, op);

                //Primitives
                Type? t = Type.GetType(TypeDef);

                //ComplexClasses
                if (t==null)
                    t = Type.GetType("System."+TypeDef);

                //Default string
                if(t == null)
                    t = typeof(string);

                Headers.Add(Name, t);
                _template.CreateColumn(Name, t);
            }
        }
        
    }
}