using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class PurchasedAlbumEntity
    {
        [Key]
        public int Id { get; set; }
        public AlbumEntity Album { get; set; }
        public string AlbumTitle { get; set; }
        public int AlbumId { get; set; }
        public ContentConsumerEntity ContentConsumer { get; set; }
        public string Username { get; set; }
        public int ContentConsumerId { get; set; }

    }
}