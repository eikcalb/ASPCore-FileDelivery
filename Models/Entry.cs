using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace FileDelivery.Models
{
    /**
     * Model for representing a submission.
     * <see cref="Entry"/> is the database model, while <see cref="EntryViewModel"/> represents the model used for the view.
     */
    public class Entry
    {
        public int ID { get; set; }

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

        public virtual ICollection<Upload> Uploads { get; set; }
    }

    public class EntryViewModel : Entry
    {
        [Required]
        public IFormFileCollection Files { get; set; }

        // parse uploaded documents.
        public async Task<Entry> Parse(EntryViewModel entryViewModel,string rootPath)
        {
            entryViewModel.Uploads = new List<Upload>();
            var entryPath = Path.Combine(rootPath,"uploads", entryViewModel.TransactionID);

            if(entryViewModel.Files.Count < 1)
            {
                return entryViewModel;
            }

            foreach (var file in entryViewModel.Files)
            {
                if (file.Length > 0)
                {
                    Directory.CreateDirectory(entryPath);

                    var filePath = Path.Combine(entryPath, Path.GetRandomFileName());
                    using (var stream = File.Create(filePath))
                    {
                        file.CopyToAsync(stream);
                    }
                    var upload = new Upload() { ContentType = file.ContentType, FileName = file.FileName, Path = filePath };
                    entryViewModel.Uploads.Add(upload);
                }
            }
            return entryViewModel;
        }
    }
}
