using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class Login
    {
        /// <summary>
        /// phone number in the format 79...
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(FastFields.RegexPhoneNumber, ErrorMessage = "Incorrect phone number")]
        public string PhoneNumber { get; set; }
    }
}
