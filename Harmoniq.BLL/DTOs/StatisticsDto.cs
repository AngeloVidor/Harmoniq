using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class StatisticsDto
    {
        [Key]
        public int Id { get; set; }
        public int UnitSold { get; set; }
        public decimal TotalValue { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ContentCreatorId { get; set; }
        public int AlbumId { get; set; }

    }
}