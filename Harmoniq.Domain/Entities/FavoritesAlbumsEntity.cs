using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class FavoritesAlbumsEntity
    {
        [Key]
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }
        public int ContentConsumerId { get; set; }
        public ContentConsumerEntity ContentConsumer { get; set; }
        public DateTime DateFavorited { get; set; } = DateTime.UtcNow;

        public string AlbumTitle { get; set; }
        public string ConsumerUsername { get; set; }

    }
}