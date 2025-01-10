using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class EditCartAlbumDto
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public int CartId { get; set; }
        public int AlbumId { get; set; }
    }
}