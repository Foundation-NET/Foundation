﻿using System.Collections.Generic;
using System.IO;
using Foundation.Csv.Shared;

namespace Foundation.Csv.Reader
{
    public class CsvReader : ICsvReader
    {
        public Dictionary<string, Type> Headers;
        private Row _template;
        public bool UseFileHeaderOrder;
        public bool FirstRowIsHeader;
        private Dictionary<int, string> _colmap;
        private char _sep;
        private List<string> _rows;

        private List<string> _sentrows;

        public CsvReader()
        :this(',') {}

        public CsvReader(char sep)
        {
            _sep = sep;
            _rows = new List<string>();
            _sentrows = new List<string>();
            _colmap = new Dictionary<int, string>();
            _template = new Row();
            Headers = new Dictionary<string, Type>();
            UseFileHeaderOrder = true;
            FirstRowIsHeader = true;
        }
        public void SetUseFileColumnOrder(bool set)
        {
            UseFileHeaderOrder = set;
        }
        public void SetFileHasHeaderRow(bool set)
        {
            FirstRowIsHeader = set;
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
        public void SetFile(string file)
        {
            if (!File.Exists(file))
                throw new Exception("No Such File.");
            _rows = File.ReadAllLines(file).ToList<string>();
            if(FirstRowIsHeader && UseFileHeaderOrder)
            {
                var _head = _rows[0];
                var sp = _head.Split(_sep);

                for (int ind = 0; ind < sp.Length; ind++)
                {
                     var val = sp[ind];
                     _colmap.Add(ind, val);
                }
                _rows.RemoveAt(0);
            } else if (FirstRowIsHeader && !UseFileHeaderOrder)
            {
                _rows.RemoveAt(0);
            } else if (!FirstRowIsHeader && UseFileHeaderOrder)
            {
                throw new Exception("Invalid Combo");
            } 
        }
        private Row Parse(int index)
        {
            Row r = (Row)_template.Clone();
            var row = _rows[index];
            var fields = row.Split(_sep);

            if (fields.Length != _colmap.Count())
                throw new Exception("Missing data on row.");

            if (UseFileHeaderOrder)
            {
                foreach (var v in _colmap)
                {
                    var f = fields[v.Key];
                    var type = Headers[v.Value];
                    r.SetColByName(v.Value, Convert.ChangeType(f, type));
                }
            } else {
                int i = 0;
                foreach(var v in Headers)
                {
                    var f = fields[i];
                    var type = v.Value;
                    r.SetColByName(v.Key, Convert.ChangeType(f, type));
                    i++;
                }
            }
            return r;
        } 
        public List<Row> GetRows(int count)
        {
            List<Row> rows = new List<Row>();
            for (int i = 0; i < count; i++)
            {
                rows.Add(Parse(i));
                _sentrows.Add(_rows[i]);
            }
            _rows.RemoveRange(0, count);
            return rows;
        }

        public Row[] GetAllRows()
        {
            var rows = new Row[_rows.Count];
            for (int i = 0; i < _rows.Count; i++)
            {
                rows[i] = Parse(i);
                _sentrows.Add(_rows[i]);
            }
            _rows = new List<string>();
            return rows;
        }

        public Row GetNextRow()
        {
            Row row;
            row = Parse(0);
            _sentrows.Add(_rows[0]);
            _rows.RemoveAt(0);
            return row;
        }
    }
}
