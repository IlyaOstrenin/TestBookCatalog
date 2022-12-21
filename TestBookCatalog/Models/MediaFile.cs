using System;
using System.Text.Json.Serialization;

namespace TestBookCatalog.Models
{
    public class MediaFile : ModelBase<Guid>
    {
        #region Constructor
        public MediaFile()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        public string Path { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
