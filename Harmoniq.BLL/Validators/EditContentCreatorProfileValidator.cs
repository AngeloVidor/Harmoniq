using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Validators
{
    public class EditContentCreatorProfileValidator : AbstractValidator<EditContentCreatorProfileDto>
    {
        public EditContentCreatorProfileValidator()
        {
            RuleFor(x => x.ContentCreatorName)
                .NotEmpty().WithMessage("Content creator name is required.")
                .Length(2, 100).WithMessage("Content creator name must be between 2 and 100 characters.");

            RuleFor(x => x.ContentCreatorDescription)
                .MaximumLength(500).WithMessage("Content creator description must not exceed 500 characters.")
                .Length(2, 200).WithMessage("Content creator description must be between 2 and 200 characters.");

            RuleFor(x => x.ContentCreatorCountry)
                .NotEmpty().WithMessage("Content creator country is required.")
                .Length(2, 50).WithMessage("Content creator country must be between 2 and 50 characters.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User ID must be a positive number.");
        }
    }
}