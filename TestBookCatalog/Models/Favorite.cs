using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestBookCatalog.Models
{
    public class Favorite
    {
        [Key]
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Key]
        [JsonIgnore]
        public Guid BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public bool IsHidden { get; set; }
    }
}
