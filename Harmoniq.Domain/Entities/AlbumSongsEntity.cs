using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class AlbumSongsEntity
    {
        [Key]
        public int Id { get; set; }
        public string SongTitle { get; set; }
        public string? SongDescription { get; set; }

        public AlbumEntity Album { get; set; }
        public int AlbumId { get; set; }
    }
}