using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestBookCatalog.Models
{
    public class BookCategory
    {
        [Key]
        [JsonIgnore]
        public Guid BookId { get; set; }
        [JsonIgnore]
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [Key]
        [JsonIgnore]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
