using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class AllPurchasedAlbumsDto
    {
        public int Id { get; set; }
        public int ContentCreatorId { get; set; }
        public decimal Price { get; set; }
        public int AlbumId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}