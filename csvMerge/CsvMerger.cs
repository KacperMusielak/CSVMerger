using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvMerge
{
    class CsvMerger
    {
        public static List<CsvRecords> getRecodrsFromACsv(string filepath, List<String> keys)
        {
            StreamReader file = new StreamReader(filepath);
            List<CsvRecords> records = new List<CsvRecords>();
            string line = null;
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                CsvRecords record = new CsvRecords();
                string[] lineSplit = line.Split(',');
                for(int i = 0; i<lineSplit.Length; i++)
                {
                    record.put(keys[i], lineSplit[i]);
                }
                records.Add(record);
            }
            file.Close();
            return records;
        }
        public static List<string> getHeadersFromACsv(string filepath)
        {
            StreamReader file = new StreamReader(filepath);
            List<string> headers = null;
            string line = null;
            while ((line = file.ReadLine()) != null)
            {
                string[] lineSplit = line.Split(',');
                headers = new List<string>(lineSplit);
                break;
            }
            file.Close();
            return headers;
        }
        public static void writeToCsv(HashSet<string> headers, List<CsvRecords> records)
        {
            string finalFile = null;
            string sep = "";
            string[] headersArr = headers.ToArray();

            foreach (string header in headersArr)
            {
                finalFile += sep;
                finalFile += header;
                sep = ",";
            }
            finalFile += "\n";

            foreach(CsvRecords record in records)
            {
                //record.removeValue("");
                sep = "";
                for(int i = 0; i<headersArr.Length; i++)
                {
                    finalFile += sep;
                    if(record.get(headersArr[i])!="null")
                    {
                        finalFile += record.get(headersArr[i]);
                    }
                    else
                    {
                        
                    }
                    sep = ",";
                }
                finalFile += "\n";
            }
            File.WriteAllText("mergedCsv.csv", finalFile);
        }
    }
}
