using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPresta
{
    public class Product
    {
        bool persian = false;
        Dictionary<string, string> persianDic = new Dictionary<string, string>() {
            { "ID", "شناسه" },
            { "File", "File" },
            { "ImageFiles", "ImageFiles" },
            { "Name *", "نام" }
        };
        private string translate(string property)
        {
            if (!persian)
            {
                return property;
            }
            else
            {
                return persianDic[property];
            }
        }
        public Product(bool persian = false)
        {
            this.persian = persian;
            Data = new CsvCollection();
        }
        public int Id {
            get
            {
                return Convert.ToInt32( Data[translate("ID")]);
            }
            set
            {
                Data[translate("ID")] = value.ToString();
            }
        }
        public string Name
        {
            get
            {
                return Data[translate("Name *")];
            }
            set
            {
                Data[translate("Name *")] = value;
            }
        }
        public string File {
            get { return Data[translate("File")]; }
            set { Data[translate("File")] = value; }
        }
        public List<string> ImageFiles
        {
            get
            {
                return new List<string>(Data[translate("ImageFiles")].Split(','));

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
                Data[translate("ImageFiles")] = res;
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
                    if (key != translate("ImageFiles") && key != translate("File"))
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
