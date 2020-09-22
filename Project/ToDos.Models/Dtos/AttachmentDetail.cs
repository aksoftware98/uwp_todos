using Microsoft.AspNetCore.Http;

namespace ToDos.Models.Dtos
{
    public class AttachmentDetail
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
