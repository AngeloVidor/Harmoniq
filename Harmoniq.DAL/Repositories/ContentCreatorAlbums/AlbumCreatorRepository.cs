using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Albums
{
    public class AlbumCreatorRepository : IAlbumCreatorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumCreatorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumEntity> AddAlbumAsync(AlbumEntity album)
        {
            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();
            return album;
        }

        public async Task<AlbumEntity> GetAlbumByIdAsync(int albumId)
        {
            return await _dbContext.Albums.Where(m => m.Id == albumId).FirstOrDefaultAsync();
        }
    }
}