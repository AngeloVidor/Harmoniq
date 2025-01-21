using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int ContentConsumerId { get; set; }
        public string Review { get; set; }
    }
}