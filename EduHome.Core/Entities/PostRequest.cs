using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class PostRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]


        public string? ImagePath { get; set; }

    }
}
