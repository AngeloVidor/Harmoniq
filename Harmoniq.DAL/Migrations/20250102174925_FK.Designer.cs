﻿// <auto-generated />
using System;
using Harmoniq.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Harmoniq.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250102174925_FK")]
    partial class FK
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Harmoniq.Domain.Entities.AlbumEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContentCreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfTracks")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentCreatorId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.AlbumSongsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("ContentCreatorId")
                        .HasColumnType("int");

                    b.Property<string>("SongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("AlbumSongs");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.CartEntity", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("ContentConsumerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCheckedOut")
                        .HasColumnType("bit");

                    b.HasKey("CartId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ContentConsumerId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentConsumerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ContentConsumers");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentCreatorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContentCreatorCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentCreatorDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentCreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ContentCreators");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.FavoritesAlbumsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("AlbumTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContentConsumerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFavorited")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ContentConsumerId");

                    b.ToTable("FavoriteAlbums");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.PurchasedAlbumEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("AlbumTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContentConsumerId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ContentConsumerId");

                    b.ToTable("PurchasedAlbums");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.WishlistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("AlbumTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContentConsumerEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ContentConsumerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ContentConsumerEntityId");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.AlbumEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.ContentCreatorEntity", "ContentCreator")
                        .WithMany("Albums")
                        .HasForeignKey("ContentCreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContentCreator");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.AlbumSongsEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.AlbumEntity", "Album")
                        .WithMany("AlbumSongs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.CartEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.AlbumEntity", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Harmoniq.Domain.Entities.ContentConsumerEntity", "Consumer")
                        .WithMany("CartItems")
                        .HasForeignKey("ContentConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Consumer");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentConsumerEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentCreatorEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.UserEntity", "User")
                        .WithOne("ContentCreator")
                        .HasForeignKey("Harmoniq.Domain.Entities.ContentCreatorEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.FavoritesAlbumsEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.AlbumEntity", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Harmoniq.Domain.Entities.ContentConsumerEntity", "ContentConsumer")
                        .WithMany("FavoriteAlbums")
                        .HasForeignKey("ContentConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("ContentConsumer");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.PurchasedAlbumEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.AlbumEntity", "Album")
                        .WithMany("PurchasedAlbums")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Harmoniq.Domain.Entities.ContentConsumerEntity", "ContentConsumer")
                        .WithMany("PurchasedAlbums")
                        .HasForeignKey("ContentConsumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("ContentConsumer");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.WishlistEntity", b =>
                {
                    b.HasOne("Harmoniq.Domain.Entities.AlbumEntity", "Album")
                        .WithMany("Wishlist")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Harmoniq.Domain.Entities.ContentConsumerEntity", null)
                        .WithMany("Wishlist")
                        .HasForeignKey("ContentConsumerEntityId");

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.AlbumEntity", b =>
                {
                    b.Navigation("AlbumSongs");

                    b.Navigation("PurchasedAlbums");

                    b.Navigation("Wishlist");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentConsumerEntity", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("FavoriteAlbums");

                    b.Navigation("PurchasedAlbums");

                    b.Navigation("Wishlist");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.ContentCreatorEntity", b =>
                {
                    b.Navigation("Albums");
                });

            modelBuilder.Entity("Harmoniq.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("ContentCreator")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
