using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Harmoniq.BLL.DTOs
{
    public class AlbumSongsDto
    {
        [Key]
        public int Id { get; set; }
        public string SongTitle { get; set; }
        public string? SongDescription { get; set; }

        public int AlbumId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public AlbumEntity Album { get; set; }
        public int ContentCreatorId { get; set; }
    }
}