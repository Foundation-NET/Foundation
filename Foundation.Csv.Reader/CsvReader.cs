using System.Collections.Generic;
using Foundation.Csv.Shared;

namespace Foundation.Csv.Reader
{
    public class CsvReader : ICsvReader
    {
        public Dictionary<string, Type> Headers;

        public CsvReader()
        {
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
            }
        }
    }
}
