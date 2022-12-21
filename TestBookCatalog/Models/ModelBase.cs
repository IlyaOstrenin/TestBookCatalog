using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class ModelBase<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
