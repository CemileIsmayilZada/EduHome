using AutoMapper;
using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Mappers
{
    public class CourseMapper:Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<CoursePostDTO, Course>().ReverseMap();
            CreateMap<CourseUpdateDTO, Course>().ReverseMap();

        }
    }
}
