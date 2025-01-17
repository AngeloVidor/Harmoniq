using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class StatisticsAlbumsDto
    {
        [Key]
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int ContentCreatorId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public int StatisticsId { get; set; }

    }
}