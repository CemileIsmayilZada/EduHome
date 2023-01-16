﻿using EduHome.Core.Interfaces;

namespace EduHome.Core.Contexts
{
    public class Course:IEntity
    {
        public int Id { get; set ; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public bool? IsDeleted { get; set; }
        

    }
}
