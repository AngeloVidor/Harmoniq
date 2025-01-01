using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class WishlistEntity
    {
        [Key]
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }
        public int ContentConsumerId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

    }
}