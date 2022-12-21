using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestBookCatalog.Models
{
    public class User : ModelBase<Guid>
    {
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public int RoleId { get; set; }
        [JsonIgnore]
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [JsonIgnore]
        public Guid RefreshToken { get; set; }
    }
}
