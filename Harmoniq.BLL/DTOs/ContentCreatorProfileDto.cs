using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class ContentCreatorProfileDto
    {
        [Key]
        public int Id { get; set; }
        public string ContentCreatorName { get; set; }
        public string ContentCreatorDescription { get; set; }
        public string ContentCreatorCountry { get; set; }
        public int TotalFollowers { get; set; }
    }
}