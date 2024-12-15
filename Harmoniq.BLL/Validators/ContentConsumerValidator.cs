using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Validators
{
    public class ContentConsumerValidator : AbstractValidator<ContentConsumerDto>
    {
        public ContentConsumerValidator()
        {
            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("The 'Nickname' field is required.")
                .Length(3, 50).WithMessage("The 'Nickname' field must be between 3 and 50 characters long.");

            RuleFor(x => x.Biography)
                .MaximumLength(500).WithMessage("The 'Biography' field can have a maximum of 500 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("The 'Country' field is required.")
                .MaximumLength(40).WithMessage("The 'Country' field must contain less than 40 characters.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("The 'UserId' field must be greater than 0.");

        }
    }
}