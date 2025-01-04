using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumSongs;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.AlbumSongs
{
    public class AlbumSongsRepository : IAlbumSongsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumSongsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumSongsEntity> AddSongsToAlbumAsync(AlbumSongsEntity albumSongsEntity)
        {
            await _dbContext.AlbumSongs.AddAsync(albumSongsEntity);
            await _dbContext.SaveChangesAsync();
            return albumSongsEntity;
        }

        public async Task<AlbumSongsEntity> EditAlbumSongsAsync(AlbumSongsEntity editedSongs)
        {
            _dbContext.AlbumSongs.Update(editedSongs);
            await _dbContext.SaveChangesAsync();
            return editedSongs;
        }
    }
}