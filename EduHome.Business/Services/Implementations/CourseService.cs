using AutoMapper;
using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace EduHome.Business.Services.Implementations
{
    public class CourseService : ICourseService
    {
        public readonly ICourseRepository _repository;
        public readonly IMapper _mapper;
        public CourseService(ICourseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

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
            if (courses is null) throw new NotFoundException(nameof(courses));
            var courseDto = _mapper.Map<List<CourseDTO>>(courses);
            return courseDto;
        }

        public async Task<CourseDTO> FindByIdAsync(int id)
        {
            var course = await _repository.FindByIdAsync(id);
            if (course is null) throw new NotFoundException(nameof(course));
            var courseDto = _mapper.Map<CourseDTO>(course);
            return courseDto;
        }
        public async Task CreateAsync(CoursePostDTO course)
        {
            if (course is null) throw new ArgumentNullException(nameof(course));
            var newCourse = _mapper.Map<Course>(course);
            await _repository.CreateAsync(newCourse);
            await _repository.SaveAsync();

        }
        public async Task UpdateAsync(int id, CourseUpdateDTO courseUpdate)
        {
            if (id != courseUpdate.Id)
            {
                throw new BadRequestException("Enter valid id");
            }
            var BaseCourse =  _repository.FindByCondition(n => n.Id != 0 ? n.Id==id : true);
            if (BaseCourse is null) throw new NotFoundException("Not Found");
            var newUpdatedCourse=_mapper.Map<Course>(courseUpdate);
            _repository.Update(newUpdatedCourse);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            var BaseCourse = await _repository.FindByIdAsync(id);
            if (BaseCourse is null) throw new NotFoundException("Not Found");
            _repository.Delete(BaseCourse);
           await _repository.SaveAsync();
        }


    }
}
