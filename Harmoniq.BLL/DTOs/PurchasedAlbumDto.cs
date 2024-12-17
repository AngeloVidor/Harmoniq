using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.BLL.DTOs
{
    public class PurchasedAlbumDto
    {
        [Key]
        public int Id { get; set; }
        public string AlbumId { get; set; }
        public int ContentConsumerId { get; set; }
    }
}