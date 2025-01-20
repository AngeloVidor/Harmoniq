using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ContentCreatorEntity> ContentCreators { get; set; }
        public DbSet<AlbumEntity> Albums { get; set; }
        public DbSet<AlbumSongsEntity> AlbumSongs { get; set; }
        public DbSet<ContentConsumerEntity> ContentConsumers { get; set; }
        public DbSet<PurchasedAlbumEntity> PurchasedAlbums { get; set; }
        public DbSet<FavoritesAlbumsEntity> FavoriteAlbums { get; set; }
        public DbSet<WishlistEntity> Wishlist { get; set; }
        public DbSet<CartEntity> ShoppingCart { get; set; }
        public DbSet<CartAlbumEntity> CartAlbums { get; set; }
        public DbSet<CartCheckoutEntity> CartCheckout { get; set; }
        public DbSet<AllPurchasedAlbumsEntity> AllPurchasedAlbums { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumEntity>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CartCheckoutEntity>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<StatisticsEntity>()
            .Property(c => c.TotalValue)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchasedAlbumEntity>()
           .Property(c => c.Price)
           .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AllPurchasedAlbumsEntity>()
            .Property(c => c.Price)
            .HasColumnType("decimal(18,2)");

        }

    }
}