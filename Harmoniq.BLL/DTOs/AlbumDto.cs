using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Harmoniq.BLL.DTOs
{
    public class AlbumDto
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

    }
}