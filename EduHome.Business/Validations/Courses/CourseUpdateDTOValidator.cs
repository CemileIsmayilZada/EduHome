using EduHome.Business.DTOs.CourseDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validations.Courses
{
    public class CourseUpdateDTOValidator : AbstractValidator<CourseUpdateDTO>
    {
        public CourseUpdateDTOValidator()
        {
            RuleFor(c => c.Id).Custom((Id, context) =>
            {
                if (!int.TryParse(Id.ToString(), out var id))
                {
                    context.AddFailure("enter correct format");
                }
            });
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name is required")
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(150).WithMessage("Maximum length : 150");
            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Maximum length : 500");
            
        }
    }
}
