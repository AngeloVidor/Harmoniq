using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Reviews
{
    public interface IReviewsService
    {
        Task<ReviewDto> ReviewAlbumAsync(ReviewDto review);
        Task<List<ReviewDto>> GetMyReviewsAsync(int contentConsumerId);
        Task<ReviewDto> DeleteReviewAsync(int reviewId);
        Task<EditReviewDto> EditReviewAsync(EditReviewDto review);
    }
}