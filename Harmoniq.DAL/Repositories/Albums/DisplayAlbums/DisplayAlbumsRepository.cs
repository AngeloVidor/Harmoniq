using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.DisplayAlbums;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.DisplayAlbums
{
    public class DisplayAlbumsRepository : IDisplayAlbumsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DisplayAlbumsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AlbumEntity>> GetContentCreatorAlbumsAsync(int contentCreatorId)
        {
            return await _dbContext.Albums.Where(a => a.ContentCreatorId == contentCreatorId).ToListAsync();
        }
    }
}