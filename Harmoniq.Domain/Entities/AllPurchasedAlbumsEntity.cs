using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class AllPurchasedAlbumsEntity
    {
        public int Id { get; set; }
        public int ContentCreatorId { get; set; }
        public virtual ContentCreatorEntity ContentCreator { get; set; }
        public decimal Price { get; set; }
        public virtual AlbumEntity Album { get; set; }
        public int AlbumId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}