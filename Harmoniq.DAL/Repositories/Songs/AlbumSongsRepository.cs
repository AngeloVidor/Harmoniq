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

        public async Task<AlbumSongsEntity> DeleteSongAsync(int songId, int albumId, int contentCreatorId)
        {
            var song = await _dbContext.AlbumSongs.FirstOrDefaultAsync(song => song.Id == songId && song.AlbumId == albumId && song.ContentCreatorId == contentCreatorId);
            if (song == null)
            {
                throw new KeyNotFoundException("Song Not Found");
            }

            _dbContext.AlbumSongs.Remove(song);
            await _dbContext.SaveChangesAsync();
            return song;
        }

        public async Task<AlbumSongsEntity> EditAlbumSongsAsync(AlbumSongsEntity editedSongs)
        {
            _dbContext.AlbumSongs.Update(editedSongs);
            await _dbContext.SaveChangesAsync();
            return editedSongs;
        }

        public async Task<List<AlbumSongsEntity>> GetContentCreatorSongsAsync(int contentCreatorId)
        {
            return await _dbContext.AlbumSongs.Where(c => c.ContentCreatorId == contentCreatorId).ToListAsync();
        }
    }
}