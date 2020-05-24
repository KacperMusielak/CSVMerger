using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            string cwd = Directory.GetCurrentDirectory();
            List<string> csv1Headers = CsvMerger.getHeadersFromACsv(cwd + @"/" + args[0]);
            List<string> csv2Headers = CsvMerger.getHeadersFromACsv(cwd + @"/" + args[1]);
            List<string> allCsvHeaders = new List<string>();
            allCsvHeaders.AddRange(csv1Headers);
            allCsvHeaders.AddRange(csv2Headers);
            HashSet<string> uniqueHeaders = new HashSet<string>(allCsvHeaders);
            List<CsvRecords> csv1Records = CsvMerger.getRecodrsFromACsv(cwd + @"/" + args[0], csv1Headers);
            List<CsvRecords> csv2Records = CsvMerger.getRecodrsFromACsv(cwd + @"/" + args[1], csv2Headers);
            List<CsvRecords> allCsvRecords = new List<CsvRecords>();
            allCsvRecords.AddRange(csv1Records);
            allCsvRecords.AddRange(csv2Records);
            List<CsvRecords> removeRecords = new List<CsvRecords>();
            HashSet<CsvRecords> uniquerecords = new HashSet<CsvRecords>(allCsvRecords);

            if (csv1Records.Count < csv2Records.Count)
            {
                mergeLists(ref csv1Records, ref csv2Records, ref removeRecords);
            }
            else
            {
                mergeLists(ref csv2Records, ref csv1Records, ref removeRecords);
            }

            foreach (CsvRecords rec in removeRecords)
            {
                allCsvRecords.Remove(rec);
            }
                CsvMerger.writeToCsv(uniqueHeaders, allCsvRecords);
        }
        static void mergeLists(ref List<CsvRecords> csv1Records, ref List<CsvRecords> csv2Records, ref List<CsvRecords> removeRecords)
        {
            string[] temp1 = new string[3];
            string[] temp2 = new string[3];
            foreach (CsvRecords rec in csv1Records)
            {
                temp1[0] = rec.get("Customer");
                temp1[1] = rec.get("Product");
                temp1[2] = rec.get("Invoice Number");
                foreach (CsvRecords rec2 in csv2Records)
                {
                    temp2[0] = rec2.get("Customer");
                    temp2[1] = rec2.get("Product");
                    temp2[2] = rec2.get("Invoice Number");
                    if (temp2[0] == temp1[0] && temp2[1] == temp1[1] && temp2[2] == temp1[2])
                    {
                        foreach (KeyValuePair<string, string> entry in rec2.getKeyVal())
                        {
                            rec.put(entry.Key, entry.Value);
                        }
                        removeRecords.Add(rec2);
                    }

                }

            }
        }
    }


}
