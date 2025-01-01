using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.Domain.Entities
{
    public class AlbumEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfTracks { get; set; }
        public int ReleaseYear { get; set; }
        public ContentCreatorEntity ContentCreator { get; set; }
        public int ContentCreatorId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsDeleted { get; set; }


        public ICollection<WishlistEntity> Wishlist { get; set; } = new List<WishlistEntity>();
        public ICollection<AlbumSongsEntity> AlbumSongs { get; set; } = new List<AlbumSongsEntity>();
        public ICollection<PurchasedAlbumEntity> PurchasedAlbums { get; set; } = new List<PurchasedAlbumEntity>();

    }
}