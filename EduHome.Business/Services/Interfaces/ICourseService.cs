using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Core.Contexts;
using EduHome.Core.Entities;
using System.Linq.Expressions;

namespace EduHome.Business.Services.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDTO>> FindAllAsync();
        Task<List<CourseDTO>> FindByConditionAsync(Expression<Func<Course, bool>> expression);
        Task<CourseDTO> FindByIdAsync(int id);
        Task CreateAsync(CoursePostDTO entity);
        Task UpdateAsync(int id, CourseUpdateDTO entity);
        Task Delete(int id);
    }
}
