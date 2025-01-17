using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class StatisticsAlbumsEntity
    {
        [Key]
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }
        public int ContentCreatorId { get; set; }
        public ContentCreatorEntity ContentCreator { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public virtual StatisticsEntity Statistics { get; set; }
        public int StatisticsId { get; set; }

    }
}