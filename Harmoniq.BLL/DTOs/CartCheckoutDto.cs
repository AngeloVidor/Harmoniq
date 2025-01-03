using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.DTOs
{
    public class CartCheckoutDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        [JsonIgnore]
        public virtual List<CartAlbumDto> Albums { get; set; } = new List<CartAlbumDto>();
        public int AlbumId { get; set; }
        public decimal Price { get; set; }
        public int ContentConsumerId { get; set; }
    }
}