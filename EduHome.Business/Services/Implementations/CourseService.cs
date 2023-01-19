using AutoMapper;
using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Business.Utilities;
using EduHome.Business.Utilities.Extensions;
using EduHome.Core.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace EduHome.Business.Services.Implementations
{
    public class CourseService : ICourseService
    {
        public readonly ICourseRepository _repository;
        public readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CourseService(ICourseRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }


        public async Task<List<CourseDTO>> FindAllAsync()
        {
            var courses = await _repository.FindAll().ToListAsync();
            var results = _mapper.Map<List<CourseDTO>>(courses);
            return results;
        }

        public async Task<List<CourseDTO>> FindByConditionAsync(Expression<Func<Course, bool>> expression)
        {
            var courses = await _repository.FindByCondition(expression).ToListAsync();
            if (courses is null) throw new BadRequestException(nameof(courses));
            var courseDto = _mapper.Map<List<CourseDTO>>(courses);
            return courseDto;
        }

        public async Task<CourseDTO> FindByIdAsync(int id)
        {
            var course = await _repository.FindByIdAsync(id);
            if (course is null) throw new BadRequestException(nameof(course));
            var courseDto = _mapper.Map<CourseDTO>(course);
            return courseDto;
        }
        public async Task CreateAsync(CoursePostDTO course)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            string filename = string.Empty;
            if (course.Image != null)
            {
                if (!course.Image.CheckFileSize(100))
                {
                    throw new IncorrectFileSizeException("This file's size is more than 100 kilobyte");
                }
                if (!course.Image.CheckFileFormat("image/"))
                {
                    throw new IncorrectFileFormatException("Enter correct file format (" + course.Image.ContentType + ")");
                }

                filename = course.Image.CopyToAsync(_env.WebRootPath, "assets");

            }
            var newCourse = _mapper.Map<Course>(course);
            newCourse.Image = filename;
            await _repository.CreateAsync(newCourse);
            await _repository.SaveAsync();

        }
        public async Task UpdateAsync(int id, CourseUpdateDTO courseUpdate)
        {
            if (id != courseUpdate.Id)
            {
                throw new NotFoundException("Enter valid id");
            }
            var baseCourse = await _repository.FindByIdAsync(id);
            if (baseCourse is null) throw new BadRequestException("Not Found");

            string filename = string.Empty;
            if (courseUpdate.Image != null)
            {
                if (!courseUpdate.Image.CheckFileSize(100))
                {
                    throw new IncorrectFileSizeException("This file's size is more than 100 kilobyte");
                }
                if (!courseUpdate.Image.CheckFileFormat("image/"))
                {
                    throw new IncorrectFileFormatException("Enter correct file format (" + courseUpdate.Image.ContentType + ")");
                }

                filename = courseUpdate.Image.CopyToAsync(_env.WebRootPath, "assets");

                baseCourse.Image = filename;
            }
            baseCourse.Name = courseUpdate.Name;
            baseCourse.Description = courseUpdate.Description;

            _repository.Update(baseCourse);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var baseCourse = await _repository.FindByIdAsync(id);
            if (baseCourse is null) throw new BadRequestException("Not Found");

            Helper.DeleteFile(_env.WebRootPath, "assets", baseCourse.Image);

            _repository.Delete(baseCourse);
            await _repository.SaveAsync();
        }


    }
}
