using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FileDelivery.Models
{
    public class Upload
    {
        public int UploadID { get; set; }
        public int EntryID { get; set; }
        public string Path { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public virtual Entry Entry { get; set; }
    }
}
