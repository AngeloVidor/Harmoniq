using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumSongs;
using Harmoniq.Domain.Entities;

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
            await _dbContext.AddAsync(albumSongsEntity);
            await _dbContext.SaveChangesAsync();
            return albumSongsEntity;
        }

        
    }
}