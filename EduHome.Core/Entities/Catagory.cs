using EduHome.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Catagory
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
