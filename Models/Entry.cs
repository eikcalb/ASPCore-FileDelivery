using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileDelivery.Models
{
    public class Entry
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [MinLength(1)]
        public int Age { get; set; }

        public int TransactionID { get; set; }

        public virtual ICollection<Upload> Uploads { get; set; }
    }

    public class EntryViewModel: Entry
    {
        public IFormFileCollection Files { get; set; }
    }
}
