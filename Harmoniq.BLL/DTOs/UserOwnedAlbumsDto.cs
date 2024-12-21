using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class UserOwnedAlbumsDto
    {
        [Key]
        public int Id { get; set; }
        public string AlbumId { get; set; }
        public int ContentConsumerId { get; set; }
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }

        public List<AlbumSongsDto> AlbumSongs { get; set; } = new List<AlbumSongsDto>();

    }
}