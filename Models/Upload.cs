using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDelivery.Models
{
    public class Upload
    {
        public int UploadID { get; set; }
        public int EntryID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }

        public virtual Entry Entry { get; set; }
    }
}
