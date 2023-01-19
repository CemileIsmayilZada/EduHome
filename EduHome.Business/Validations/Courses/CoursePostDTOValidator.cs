using EduHome.Business.DTOs.CourseDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validations.Courses
{
    public class CoursePostDTOValidator:AbstractValidator<CoursePostDTO>
    {
        public CoursePostDTOValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(150).WithMessage("Maximum length : 150");
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Maximum length : 500");
            RuleFor(c => c.Image)
                .NotEmpty()
                .NotNull();


        }
    }
}
