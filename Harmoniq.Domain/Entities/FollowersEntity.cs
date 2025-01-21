using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class FollowersEntity
    {
        [Key]
        public int Id { get; set; }
        public int FollowedCreatorId { get; set; }
        public virtual ContentCreatorEntity FollowedCreator { get; set; }
        public int FollowerConsumerId { get; set; }
        public virtual  ContentConsumerEntity FollowerConsumer { get; set; }
    }
}