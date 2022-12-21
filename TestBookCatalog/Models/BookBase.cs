using System;
using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class BookBase : ModelBase<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Range(868, 2022)]
        public int Year { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(1, 23675)]
        public int NumberOfPages { get; set; }
    }
}
