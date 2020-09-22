using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDos.Models
{
    public class ToDoItem
    {

        public string Id { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int Points { get; set; }

        public int Quality { get; set; }

        public bool IsDone { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime TaskDate { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public DateTime? AchievingDate { get; set; }

        public DateTime? CancelationDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public string UserId { get; set; }

        public List<Attachment> Attachments { get; set; }


    }
}
