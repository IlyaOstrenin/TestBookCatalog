using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TestBookCatalog.Controllers
{
    public class TestBookCatalogController : ControllerBase
    {
        protected IHttpContextAccessor _context;

        public Guid UserId 
        { 
            get 
            { 
                if (!String.IsNullOrEmpty(_context.HttpContext.User.Identity.Name)) 
                    return Guid.Parse(_context.HttpContext.User.Identity.Name); 
                else return Guid.Empty;
            } 
        }

        
    }
}
