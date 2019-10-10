using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileDelivery.Models
{
    /**
     * Model for representing a submission.
     * <see cref="Entry"/> is the database model, while <see cref="EntryViewModel"/> represents the model used for the view.
     */
    public class Entry
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

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

        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Age { get; set; }

        [Required]
        public string TransactionID { get; set; }

        [Required]
        public virtual ICollection<Upload> Uploads { get; set; }
    }

    public class EntryViewModel : Entry
    {
        public IFormFileCollection Files { get; set; }
    }
}
