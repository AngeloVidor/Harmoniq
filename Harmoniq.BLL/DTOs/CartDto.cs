using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class CartDto
    {
        [Key]
        public int CartId { get; set; }
        public int ContentConsumerId { get; set; }
        public bool IsCheckedOut { get; set; } = false;
    }
}