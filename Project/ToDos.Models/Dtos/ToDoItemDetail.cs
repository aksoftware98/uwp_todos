using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ToDos.Models.Dtos
{
    public class ToDoItemDetail
    {

        public string Id { get; set; }

        [Required]
        public string Descrption { get; set; }

        public int Quality { get; set; }

        [Range(typeof(int), "0", "5000")]
        public int Points { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime? TaskDate { get; set; }

        [Required]
        public string Category { get; set; }
        public bool IsDone { get; set; }

        public IEnumerable<AttachmentDetail> Attachments { get; set; }
        
    }

    public static class ObjectsMapper
    {
        public static ToDoItemDetail ToItemDetail(this ToDoItem item)
        {
            return new ToDoItemDetail
            {
                Attachments = item.Attachments?.Select(a => new AttachmentDetail
                {
                    Name = a.Name,
                    Url = a.Url
                }),
                Category = item.Category,
                CreationDate = item.CreationDate,
                Descrption = item.Description,
                Id = item.Id,
                IsDone = item.IsDone,
                Points = item.Points,
                Quality = item.Quality,
                TaskDate = item.TaskDate
            }; 
        }
    }
}
