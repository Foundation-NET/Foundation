using System;
using System.Collections.Generic;
using System.IO;

namespace Foundation.ConfigReader
{
    public class ConfigIniReader : IConfigReader
    {
        private Dictionary<string, Dictionary<string,string>>? INIFile;
        private string? Path;
        public bool Connect(string Path, bool Create = false)
        {
            this.Path = Path;
            if(!File.Exists(Path) && !Create)
            {
                return false;
            } else if (!File.Exists(Path) && Create)
            {
                File.WriteAllText(Path, "");
                INIFile = new Dictionary<string, Dictionary<string, string>>();
            } else {
                INIFile = new Dictionary<string, Dictionary<string, string>>();
                string _currentSection = "";
                foreach (string line in File.ReadLines(Path))
                {
                    if(line == "\n" || line.Length == 0)
                        continue;
                    var split = line.Split('=');
                    if (split.Length == 1)
                    {
                        _currentSection = split[0].Substring(1, split[0].Length -2);
                    }
                    if (split.Length == 2)
                    {
                        if (!INIFile.ContainsKey(_currentSection))
                            INIFile.Add(_currentSection, new Dictionary<string, string>());
                        INIFile[_currentSection].Add(split[0],split[1]);
                    }
                }
            }
            return true;
        }

        public string Read(string Key)
        {
            if (INIFile == null)
                throw new Exception("Empty Ini file");
            var split = Key.Split('.');
            if (split.Length == 2)
            {
                if (!INIFile.ContainsKey(split[0]))
                    throw new Exception("No such Key");
                return INIFile[split[0]][split[1]];
            } else if (split.Length == 1) {
                return INIFile[""][Key];
            }
            return "";
        }

        public void Write(string Key, string? Value)
        {
            if (INIFile == null)
                throw new Exception("Empty Ini file");
            var split = Key.Split('.');
            if (Value == null)
                Value = "";
            if (split.Length == 2)
            {
                if (!INIFile.ContainsKey(split[0]))
                    INIFile.Add(split[0], new Dictionary<string, string>());
                if (!INIFile[split[0]].ContainsKey(split[1]))
                {
                    INIFile[split[0]].Add(split[1], Value);
                } else {
                    INIFile[split[0]][split[1]] = Value;
                }
            } else if (split.Length == 1) {
                if (!INIFile.ContainsKey(""))
                    INIFile.Add("", new Dictionary<string, string>());
                if (!INIFile[""].ContainsKey(Key))
                {
                    INIFile[""].Add(Key, Value);
                } else {
                    INIFile[""][Key] = Value;
                }
            }
            WriteToFile();
        }
        public int Exists(string Key)
        {
            if (INIFile == null)
                throw new Exception("Empty Ini file");
            var split = Key.Split('.');
            if (split.Length == 2)
            {
                if (INIFile.ContainsKey(split[0]))
                {
                    if (INIFile[split[0]].ContainsKey(split[1]))
                    {
                        return 1;
                    }
                }
            } else if (split.Length == 1) {
                if (INIFile[""].ContainsKey(Key))
                {
                    return 1;
                }
            }
            return -1;
        }

        private void WriteToFile()
        {
            if (INIFile == null)
                throw new Exception("Empty Ini file");
            if (Path == null)
                throw new Exception("Empty Path file");
            
            List<string> contents = new List<string>();

            if (INIFile.ContainsKey(""))
            {
                foreach(KeyValuePair<string,string> keyval in INIFile[""])
                {
                    contents.Add(keyval.Key + "=" + keyval.Value);
                }
            }
            foreach(KeyValuePair<string, Dictionary<string,string>> keyval in INIFile)
            {
                if (keyval.Key == "")
                    continue;   
                contents.Add("[" + keyval.Key + "]");
                foreach(KeyValuePair<string,string> subkeyval in keyval.Value)
                {
                    contents.Add(subkeyval.Key + "=" + subkeyval.Value);
                }
            }
            File.WriteAllLines(Path, contents);
        }
    }
}
