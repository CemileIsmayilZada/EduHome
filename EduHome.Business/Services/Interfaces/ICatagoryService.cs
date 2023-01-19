using EduHome.Business.DTOs.CourseDTOs;
using EduHome.Core.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Interfaces
{
    public interface ICatagoryService
    {
        Task<List<CourseDTO>> FindAllAsync();
        Task<List<CourseDTO>> FindByConditionAsync(Expression<Func<Course, bool>> expression);
        Task<CourseDTO> FindByIdAsync(int id);
        Task CreateAsync(CoursePostDTO entity);
        Task UpdateAsync(int id, CourseUpdateDTO entity);
        Task Delete(int id);
    }
}
