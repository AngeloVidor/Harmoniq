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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumEntity>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18,2)"); 
        }

    }
}