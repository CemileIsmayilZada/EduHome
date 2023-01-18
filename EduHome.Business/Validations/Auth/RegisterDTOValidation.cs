using EduHome.Business.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validations.Auth
{
    public class RegisterDTOValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidation()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(x => x.Fullname)
                        .MaximumLength(50)
                        .MinimumLength(2);

            RuleFor(x => x.Email)
                    .MaximumLength(256)
                    .MinimumLength(5)
                    .NotEmpty()
                    .NotNull()
                    .EmailAddress();
            RuleFor(x => x.Password)
                    .MaximumLength(250)
                    .MinimumLength(8)
                    .NotEmpty()
                    .NotNull();

        }

    }
}
