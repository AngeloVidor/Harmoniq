using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class CartAlbumDto
    {
        [Key]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int AlbumId { get; set; }
    }
}