using System.ComponentModel.DataAnnotations;

namespace TestBookCatalog.Models
{
    public class Validate : Login
    {
        /// <summary>
        /// code from sms
        /// </summary>
        [RegularExpression(FastFields.RegexSMSCode, ErrorMessage = "Invalid code")]
        public string Code { get; set; }
    }
}
