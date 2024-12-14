using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Validators
{
    public class ContentCreatorValidator : AbstractValidator<ContentCreatorDto>
    {
        public ContentCreatorValidator()
        {
            RuleFor(x => x.ContentCreatorName)
              .NotEmpty().WithMessage("Content creator name cannot be empty")
              .NotNull().WithMessage("Content creator name cannot be null")
              .MaximumLength(50).WithMessage("Content creator name must be less than 50 characters");

            RuleFor(x => x.ContentCreatorDescription)
                .NotEmpty().WithMessage("Content creator description cannot be empty")
                .NotNull().WithMessage("Content creator description cannot be null")
                .MaximumLength(200).WithMessage("Content creator description must be less than 200 characters");

            RuleFor(x => x.ContentCreatorCountry)
                .NotEmpty().WithMessage("Content creator country cannot be empty")
                .NotNull().WithMessage("Content creator country cannot be null")
                .MaximumLength(50).WithMessage("Content creator country must be less than 50 characters");


            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty")
                .NotNull().WithMessage("User ID cannot be null")
                .GreaterThan(0).WithMessage("User ID must be a positive number");
        }
    }
}