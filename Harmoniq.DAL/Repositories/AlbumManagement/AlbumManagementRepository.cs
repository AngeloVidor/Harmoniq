using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.AlbumManagement
{
    public class AlbumManagementRepository : IAlbumManagementRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlbumManagementRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumEntity> GetAlbumAsync(int albumId)
        {
            return await _dbContext.Albums.Where(ai => ai.Id == albumId).FirstOrDefaultAsync();
        }
    }
}