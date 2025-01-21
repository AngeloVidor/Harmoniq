using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.DAL.Context;
using Harmoniq.DAL.Interfaces.Reviews;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Harmoniq.DAL.Repositories.Reviews
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReviewsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReviewEntity> ReviewAlbumAsync(ReviewEntity review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<List<ReviewEntity>> GetMyReviewsAsync(int contentConsumerId)
        {
            return await _dbContext.Reviews
                .Where(c => c.ContentConsumerId == contentConsumerId).ToListAsync();
        }

        public async Task<ReviewEntity> DeleteReviewAsync(int reviewId)
        {
            var review = await _dbContext.Reviews.FindAsync(reviewId);
            if (review == null)
            {
                throw new KeyNotFoundException("Review not found");
            }
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<ReviewEntity> EditReviewAsync(ReviewEntity review)
        {
            var existingReview = await _dbContext.Reviews.FindAsync(review.Id);
            if(existingReview == null)
            {
                throw new KeyNotFoundException("Review not found");
            }

            existingReview.Review = review.Review;
            existingReview.ContentConsumerId = review.ContentConsumerId;
            existingReview.AlbumId = review.AlbumId;

            _dbContext.Reviews.Update(existingReview);
            await _dbContext.SaveChangesAsync();
            return existingReview;
        }
    }
}