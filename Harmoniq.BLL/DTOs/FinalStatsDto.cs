using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class FinalStatsDto
    {
        public int Id { get; set; }
        public int ContentCreatorId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> AlbumIds { get; set; } = new List<int>();
        public int Month { get; set; }
        public int Year { get; set; }

        public int Quantity { get; set; }
    }
}