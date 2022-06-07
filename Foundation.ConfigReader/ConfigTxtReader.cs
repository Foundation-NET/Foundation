using System;
using System.Collections.Generic;
using System.IO;

namespace Foundation.ConfigReader
{
    public class ConfigTxtReader : IConfigReader
    {
        private List<string>? Rows;
        private string? Path;
        public bool Connect(string Path, bool Create = false)
        {
            this.Path = Path;
            if (!File.Exists(Path) && !Create)
            {
                return false;
            } else if (!File.Exists(Path) && Create)
            {
                File.WriteAllText(Path, "");
                Rows = new List<string>();
                return true;
            }
            Rows = new List<string>(File.ReadAllText(Path).Split("\n"));
            return true;
        }

        public string Read(string Key)
        {
            if (Rows == null)
                throw new Exception("Not populated Rows");
            foreach (string row in Rows)
            {
                var fields = row.Split('=');
                if (fields.Length>1)
                {
                    if (fields[0] == Key)
                    {
                        return fields[1];
                    }
                }
            }
            return "";
        }

        public void Write(string Key, string Value)
        {
            if (Rows == null)
                throw new Exception("Not populated Rows");
            if (Path == null)
                throw new Exception("Not populated Path");
            int index = Exists(Key);
            if (index > -1)
            {
                Rows[index] = Key + "=" + Value;
            } else {
                Rows.Add(Key + "=" + Value);
            }
            File.WriteAllLines(Path, Rows);
        }
        public int Exists(string Key)
        {
            if (Rows == null)
                throw new Exception("Not populated Rows");
            int rowid = 0;
            foreach (string row in Rows)
            {
                var fields = row.Split('=');
                if (fields.Length>1)
                {
                    if (fields[0] == Key)
                    {
                        return rowid;
                    }
                }
                rowid++;
            }
            return -1;
        }
    }
}
