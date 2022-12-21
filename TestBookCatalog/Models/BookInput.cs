using System;
using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class BookInput : BookBase
    {
        [Required]
        public Guid? CoverId { get; set; }

        [Required]
        public int[] CategoriesIds { get; set; }
    }
}
