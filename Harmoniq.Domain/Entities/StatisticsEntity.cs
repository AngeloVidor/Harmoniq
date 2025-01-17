using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class StatisticsEntity
    {
        public int Id { get; set; }
        public int UnitSold { get; set; }
        public decimal TotalValue { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public virtual ICollection<StatisticsAlbumsEntity> Albums { get; set; } = new List<StatisticsAlbumsEntity>();


    }
}