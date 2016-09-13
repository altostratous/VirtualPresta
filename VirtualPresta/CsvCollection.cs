using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;

namespace VirtualPresta
{
    public class CsvCollection : IDictionary<string, string>
    {
		public string Data { get { return data; } set
            {
                data = value;
                string check = this["just-to-check-if-data-is-valid"];
            } }

        private string data;

		public CsvConfiguration CsvConfiguration { get; set; }

		public CsvCollection()
        {
            data = "";
            CsvConfiguration = new CsvConfiguration() { Delimiter = ";" };
        }

		private CsvReader createReader()
        {
			//if (data == "")
   //         {
   //             StringWriter stringWriter = new StringWriter();

   //             CsvWriter writer = new CsvWriter(stringWriter, CsvConfiguration);
   //             writer.NextRecord();
			//	data = stringWriter.ToString();
   //         }
            StringReader stringReader = new StringReader(data);
            return new CsvReader(stringReader, CsvConfiguration);
        }

        public string this[string key]
        {
            get
            {
                CsvReader reader = createReader();
                if (data == "") return null;
                reader.Read();
                int index = new List<string>(reader.FieldHeaders).IndexOf(key);
                if (index == -1)
                    return null;
                return reader.GetField(key);
            }

            set
            {
                bool exists = this[key] != null;

                StringWriter stringWriter = new StringWriter();

				
                StringReader stringReader = new StringReader(data);
                CsvReader reader = createReader();

                CsvWriter writer = new CsvWriter(stringWriter, CsvConfiguration);
                if (data != "")
                {
                    reader.Read();
                    foreach (string header in reader.FieldHeaders)
                    {
                        writer.WriteField(header);
                    }
                }
				if (!exists)
					writer.WriteField(key);
                writer.NextRecord();
                if (data != "")
                {
                    foreach (string header in reader.FieldHeaders)
                    {
                        if (header == key)
                        {
                            writer.WriteField(value);
                        }
                        else {
                            writer.WriteField(reader.GetField(header));
                        }
                    }
                }
				if (!exists)
					writer.WriteField(value);
                writer.NextRecord();
                data = stringWriter.ToString();
            }
        }

        public int Count
        {
            get
            {
                return Keys.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                CsvReader reader = createReader();
                reader.Read();
                return reader.FieldHeaders;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                List<string> values = new List<string>();
                CsvReader reader = createReader();
                reader.Read();
				for (int i = 0; i < reader.FieldHeaders.Count(); i++)
                {
                    values.Add(reader.GetField(i));
                }
                return values;
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, string value)
        {
            this[key] = value;
        }

        public void Clear()
        {
            data = "";
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            if (!ContainsKey(item.Key))
                return false;
            else
                return this[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return this[key] != null;
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
			foreach(string key in Keys)
            {
                array[arrayIndex] = new KeyValuePair<string, string>(key, this[key]);
                arrayIndex++;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[Count];
            CopyTo(array, 0);
            return (IEnumerator < KeyValuePair < string, string>> )array.GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            if (Contains(item))
            {
                Remove(item.Key);
                return true;
            }
			else
            {
                return false;
            }
        }

        public bool Remove(string key)
        {

            bool exists = this[key] != null;
            if (!exists) return false;
            StringWriter stringWriter = new StringWriter();

            StringReader stringReader = new StringReader(data);
            CsvReader reader = createReader();
            reader.Read();
            CsvWriter writer = new CsvWriter(stringWriter, CsvConfiguration);
            foreach (string header in reader.FieldHeaders)
            {
				if(header != key)
					writer.WriteField(header);
            }
            writer.NextRecord();
            foreach (string header in reader.FieldHeaders)
            {
                if (header != key)
                {
                    writer.WriteField(reader.GetField(header));
                }
            }
            writer.NextRecord();
            data = stringWriter.ToString();
            return true;
        }

        public bool TryGetValue(string key, out string value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else {
                value = null; return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
