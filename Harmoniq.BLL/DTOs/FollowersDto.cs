using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.DTOs
{
    public class FollowersDto
    {
        [Key]
        public int FollowedCreatorId { get; set; }
        public int FollowerConsumerId { get; set; }
    }
}