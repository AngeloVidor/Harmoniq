using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class ContentCreatorEntity
    {
        [Key]
        public int Id { get; set; }
        public string ContentCreatorName { get; set; }
        public string ContentCreatorDescription { get; set; }
        public string ContentCreatorCountry { get; set; } 
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public UserEntity User { get; set; }
        public int UserId { get; set; }

        public ICollection<AlbumEntity> Albums {get; set;} = new List<AlbumEntity>();
    }
}