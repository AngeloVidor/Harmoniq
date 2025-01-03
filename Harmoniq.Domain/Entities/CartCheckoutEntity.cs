using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class CartCheckoutEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public CartEntity Cart { get; set; }
        public virtual List<CartAlbumEntity> Albums { get; set; } = new List<CartAlbumEntity>();
        public int AlbumId { get; set; }
        public decimal Price { get; set; }
        public int ContentConsumerId { get; set; }
        [ForeignKey("ContentConsumerId")]
        public ContentConsumerEntity ContentConsumer { get; set; }
    }
}