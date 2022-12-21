using System;

namespace TestBookCatalog.Models
{
    public class ValidateRefreshToken
    {
        public Guid RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
