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
            RuleFor(album => album.SongDescription).NotEmpty()
                .NotNull().WithMessage("Description cannot be null")
                .MaximumLength(100).WithMessage("Song title must be less than 100 characters");

            RuleFor(album => album.SongTitle).NotEmpty()
                .NotNull()
                .WithMessage("Title cannot be null");

            RuleFor(x => x.AlbumId)
                .GreaterThan(0).WithMessage("Album ID must be greater than zero");

        }
    }
}