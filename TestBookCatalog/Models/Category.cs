using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class Category : ModelBase<int>
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
