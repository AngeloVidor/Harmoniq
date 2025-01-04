using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Harmoniq.BLL.DTOs
{
    public class EditedAlbumDto
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfTracks { get; set; }
        public int ReleaseYear { get; set; }
        public int ContentCreatorId { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        [ValidateNever]
        public bool? IsDeleted { get; set; }
    }
}