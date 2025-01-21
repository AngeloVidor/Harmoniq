using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.Domain.Entities;

namespace Harmoniq.DAL.Interfaces.Reviews
{
    public interface IReviewsRepository
    {
        Task<ReviewEntity> ReviewAlbumAsync(ReviewEntity review);
        Task<List<ReviewEntity>> GetMyReviewsAsync(int contentConsumerId);
        Task<ReviewEntity> DeleteReviewAsync(int reviewId);
        Task<ReviewEntity> EditReviewAsync(ReviewEntity review);
    }
}