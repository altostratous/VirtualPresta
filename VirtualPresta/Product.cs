using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPresta
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public List<string> ImageFiles { get; set; }
        public bool Saved { get; set; }
        public bool FileAndImagesSaved { get; set; }
    }
}
