using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Validators
{
    public class AlbumValidator : AbstractValidator<AlbumDto>
    {
        public AlbumValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title cannot be empty")
               .NotNull().WithMessage("Title cannot be null")
               .MaximumLength(100).WithMessage("Title must be less than 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty")
                .NotNull().WithMessage("Description cannot be null")
                .MaximumLength(100).WithMessage("Description must be less than 100 characters");

            RuleFor(x => x.NumberOfTracks)
                .NotEmpty().WithMessage("Number of tracks cannot be empty")
                .NotNull().WithMessage("Number of tracks cannot be null")
                .GreaterThan(0).WithMessage("Number of tracks must be greater than 0");

            RuleFor(x => x.ReleaseYear)
                .GreaterThan(1900).WithMessage("Release year must be after 1900")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Release year cannot be in the future");

            RuleFor(x => x.ContentCreatorId)
                .NotEmpty().WithMessage("Content creator ID cannot be empty")
                .NotNull().WithMessage("Content creator ID cannot be null")
                .GreaterThan(0).WithMessage("Content creator ID must be a positive number");
        }
    }
}