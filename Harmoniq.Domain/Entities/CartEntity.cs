using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class CartEntity
    {
        [Key]
        public int CartId { get; set; }
        public int AlbumId { get; set; }
        public AlbumEntity Album { get; set; }
        public int ContentConsumerId { get; set; }

        [ForeignKey("ContentConsumerId")]
        public ContentConsumerEntity Consumer { get; set; }

        public bool IsCheckedOut { get; set; } = false;

    }
}