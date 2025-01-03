using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class CartAlbumEntity
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public CartEntity Cart { get; set; }
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }

    }
}