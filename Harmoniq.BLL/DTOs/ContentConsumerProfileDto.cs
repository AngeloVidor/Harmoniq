using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class ContentConsumerProfileDto
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Biography { get; set; }
        public string Country { get; set; }
    }
}