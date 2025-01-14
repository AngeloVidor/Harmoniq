using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Harmoniq.BLL.DTOs
{
    public class EditedAlbumSongsDto
    {
        [Key]
        public int Id { get; set; }
        public string SongTitle { get; set; }
        public string? SongDescription { get; set; }
        public int AlbumId { get; set; }
        public int ContentCreatorId { get; set; }
        public IFormFile TrackFile { get; set; }

        [NotMapped]
        [JsonIgnore]
        [ValidateNever]
        public string TrackUrl { get; set; }
    }
}