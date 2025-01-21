using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class ReviewEntity
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public virtual AlbumEntity Album { get; set; }
        public int ContentConsumerId { get; set; }
        public virtual ContentConsumerEntity ContentConsumer { get; set; }
        public string Review { get; set; }
    }
}