using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPresta
{
    public class Product
    {
        public Product()
        {
            Data = new CsvCollection();
        }
        public int Id {
            get
            {
                return Convert.ToInt32( Data["ID"]);
            }
            set
            {
                Data["ID"] = value.ToString();
            }
        }
        public string Name
        {
            get
            {
                return Data["Name *"];
            }
            set
            {
                Data["Name *"] = value;
            }
        }
        public string File {
            get { return Data["File"]; }
            set { Data["File"] = value; }
        }
        public List<string> ImageFiles
        {
            get
            {
                return new List<string>(Data["ImageFiles"].Split(','));

            }
            set
            {
                string res = "";
                foreach (string image in value)
                {
                    if (res != "")
                        res += ',';
                    res += image;
                }
                Data["ImageFiles"] = res;
            }
        }

        public CsvCollection Data { get; set; }
        public CsvCollection StandardCSV
        {
            get
            {
                CsvCollection collection = new CsvCollection();
                foreach (string key in Data.Keys)
                {
                    if (key != "ImageFiles" && key != "File")
                    {
                        collection.Add(key, Data[key]);
                    }
                }
                return collection;
            }
        }
        public bool CSVPushed { get; set; }
        public bool Saved { get; set; }
        public bool FileAndImagesSaved { get; set; }
    }
}
