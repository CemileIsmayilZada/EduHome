using EduHome.Business.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validations.Auth
{
    public class LoginDTOValidation:AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
        {
            RuleFor(l=>l.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256)
                .MinimumLength(2);
            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(256)
                .MinimumLength(8);

        }
    }
}
