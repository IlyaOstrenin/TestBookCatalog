using System;

namespace TestBookCatalog.Models
{
    public class ValidateResponse : ValidateRefreshToken
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
