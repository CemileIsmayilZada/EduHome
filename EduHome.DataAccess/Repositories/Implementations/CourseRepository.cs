using EduHome.Core.Contexts;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;

namespace EduHome.DataAccess.Repositories.Implementations
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
