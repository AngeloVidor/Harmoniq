using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class AlbumEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfTracks { get; set; }
        public int ReleaseYear { get; set; }
        public ContentCreatorEntity ContentCreator { get; set; }
        public int ContentCreatorId { get; set; }

        public ICollection<AlbumSongsEntity> AlbumSongs { get; set; } = new List<AlbumSongsEntity>();

    }
}