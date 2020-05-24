using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvMerge
{
    class CsvRecords
    {
        private Dictionary<string, string> keyVal;
       
        public CsvRecords()
        {
            keyVal = new Dictionary<string, string>();
        }
        public void printDictionary()
        {
            foreach (KeyValuePair<string, string> kvp in keyVal)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);

            }
        }
        public Dictionary<string, string> getKeyVal()
        {
            return keyVal;
        }
        public void setKeyVal(Dictionary<string, string> keyVal)
        {
            this.keyVal = keyVal;
        }
        public void put(string key, string val)
        {
            if(!keyVal.ContainsKey(key))
            {
                keyVal.Add(key, val);
            }
        }
        public string get(string key)
        {
            string temp = "temp";
            if (keyVal.TryGetValue(key, out temp))
            {
            //Console.WriteLine(key);
                return keyVal[key];
            }
            else return "null";
        }
        public void removeValue(string value)
        {
            foreach (var item in keyVal.Where(kvp => kvp.Value == value).ToList())
            {
                keyVal.Remove(item.Key);
            }
        }
        public string getByValue(string value)
        {
            foreach (KeyValuePair<string, string> kvp in keyVal)
            {
                if (kvp.Value == value)
                    return kvp.Key;
            }
            return null;
        }
    }
}
