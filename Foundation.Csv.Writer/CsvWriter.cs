using Foundation.Csv.Shared;
using System.Collections.Generic;
using System.IO;

namespace Foundation.Csv.Writer
{
    public class CsvWriter : CsvShared, ICsvWriter
    {
        private List<Row> Rows;
        private string _headrow;
        private string _outputFile;

        public CsvWriter()
            :base()
        {
            Rows = new List<Row>();
            _outputFile = string.Empty;
            _headrow = string.Empty;
        }

        public new void SetHeader(string header)
        {
            base.SetHeader(header);
            string s = "";
            foreach (var col in Headers)
            {
                if (s == "") {
                    s = col.Key;
                } else {
                    s = s + _sep + col.Key;
                }
            }
            _headrow = s;
        }

        public void SetOutputFile(string path)
        {
            _outputFile = path;
        }
        public Row NewRow()
        {
            return (Row)_template.Clone();
        }
        public void CommitRow(Row r)
        {
            Rows.Add(r);
        }
        public void CommitFile()
        {
            if (_outputFile == string.Empty)
                throw new Exception("Filename Required");
            List<string> outrows = new List<string>();
            outrows.Add(_headrow);
            foreach (var row in Rows)
            {
                string r = "";
                foreach (var col in Headers)
                {
                    var a = row.GetColByName(col.Key);
                    if (r=="" && a != null)
                    {
                        r = (string)a;
                    } else {
                    r = r + _sep + a; 
                    }
                }
                outrows.Add(r);
            }
            File.WriteAllLines(_outputFile, outrows.ToArray());
        }
    }
}
