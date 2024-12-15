using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class ContentConsumerEntity
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Biography { get; set; }
        public string Country { get; set; }
        public DateTime DateAdded = DateTime.UtcNow;
        public UserEntity User { get; set; }
        public int UserId { get; set; }
    }
}