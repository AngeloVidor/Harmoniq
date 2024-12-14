using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Validators
{
    public class AlbumSongsValidator : AbstractValidator<AlbumSongsDto>
    {
        public AlbumSongsValidator()
        {
            RuleFor(x => x.SongDescription)
             .MaximumLength(200).WithMessage("Song description must be less than 200 characters");

            RuleFor(x => x.SongTitle)
                .NotEmpty().WithMessage("Song title cannot be empty")
                .NotNull().WithMessage("Song title cannot be null")
                .MaximumLength(100).WithMessage("Song title must be less than 100 characters");

            RuleFor(x => x.AlbumId)
                .GreaterThan(0).WithMessage("Album ID must be greater than zero");

            RuleFor(x => x.ContentCreatorId)
                .GreaterThan(0).WithMessage("Content Creator ID must be greater than zero");

        }
    }
}