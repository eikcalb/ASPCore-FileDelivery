using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
        public string EmailAddress { get; set; }

        public int Age { get; set; }

        public int TransactionID { get; set; }

        public virtual ICollection<Upload> Uploads { get; set; }
    }
}
