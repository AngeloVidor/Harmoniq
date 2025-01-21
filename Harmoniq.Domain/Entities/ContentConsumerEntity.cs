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

        public ICollection<CartEntity> CartItems { get; set; } = new List<CartEntity>();
        public ICollection<FavoritesAlbumsEntity> FavoriteAlbums { get; set; } = new List<FavoritesAlbumsEntity>();
        public ICollection<PurchasedAlbumEntity> PurchasedAlbums { get; set; } = new List<PurchasedAlbumEntity>();
        public ICollection<WishlistEntity> Wishlist { get; set; } = new List<WishlistEntity>();
        public ICollection<FollowersEntity> FollowingCreators { get; set; } = new List<FollowersEntity>();

    }

}