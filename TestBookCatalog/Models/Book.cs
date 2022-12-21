using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestBookCatalog.Models
{
    public class Book : BookBase
    {
        #region Constructor
        public Book()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        [JsonIgnore]
        public Guid CoverId { get; set; }
        [ForeignKey("CoverId")]
        public virtual MediaFile Cover { get; set; }

        public List<BookCategory> Categories { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
