using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Reviews;
using Harmoniq.DAL.Interfaces.AlbumManagement;
using Harmoniq.DAL.Interfaces.Reviews;
using Harmoniq.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Harmoniq.BLL.Services.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly IMapper _mapper;
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IAlbumManagementRepository _albumManagementRepository;

        public ReviewsService(IMapper mapper, IReviewsRepository reviewsRepository, IAlbumManagementRepository albumManagementRepository)
        {
            _mapper = mapper;
            _reviewsRepository = reviewsRepository;
            _albumManagementRepository = albumManagementRepository;
        }

        public async Task<ReviewDto> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
            {
                throw new ArgumentException("Invalid review id");
            }
            var deletedReview = await _reviewsRepository.DeleteReviewAsync(reviewId);
            return _mapper.Map<ReviewDto>(deletedReview);
        }

        public async Task<EditReviewDto> EditReviewAsync(EditReviewDto review)
        {
            if (review.Id <= 0)
            {
                throw new ArgumentException("Invalid review id");
            }
            var reviewEntity = _mapper.Map<ReviewEntity>(review);
            var editedReview = await _reviewsRepository.EditReviewAsync(reviewEntity);
            return _mapper.Map<EditReviewDto>(editedReview);
        }

        public async Task<List<ReviewDto>> GetMyReviewsAsync(int contentConsumerId)
        {
            var reviews = await _reviewsRepository.GetMyReviewsAsync(contentConsumerId);
            if (reviews == null)
            {
                throw new KeyNotFoundException("No reviews found");
            }
            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> ReviewAlbumAsync(ReviewDto review)
        {
            var isAlbumPurchased = await _albumManagementRepository.IsAlbumPurchasedAsync(review.AlbumId, review.ContentConsumerId);
            if (!isAlbumPurchased)
            {
                throw new Exception("Only purchased albums can be reviewed");
            }
            var reviewEntity = _mapper.Map<ReviewEntity>(review);
            var result = await _reviewsRepository.ReviewAlbumAsync(reviewEntity);
            return _mapper.Map<ReviewDto>(result);
        }
    }
}