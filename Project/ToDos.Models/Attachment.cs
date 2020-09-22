using System;
using System.Text.Json.Serialization;

namespace ToDos.Models
{
    public class Attachment
    {
        
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
