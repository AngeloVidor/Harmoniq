using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class FavoritesAlbumsDto
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int AlbumId { get; set; }
        
        [JsonIgnore]
        [NotMapped]
        public int ContentConsumerId { get; set; }
        public DateTime DateFavorited { get; set; } = DateTime.UtcNow;
    }
}