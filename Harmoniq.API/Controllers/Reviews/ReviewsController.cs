using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;
using Harmoniq.BLL.Interfaces.Reviews;
using Harmoniq.BLL.Interfaces.UserContext;
using Microsoft.AspNetCore.Mvc;

namespace Harmoniq.API.Controllers.Reviews
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;
        private readonly IUserContextService _userContextService;

        public ReviewsController(IReviewsService reviewsService, IUserContextService userContextService)
        {
            _reviewsService = reviewsService;
            _userContextService = userContextService;
        }

        [HttpPost("album/{albumId}")]
        public async Task<IActionResult> ReviewAlbumAsync(ReviewDto review)
        {
            var userId = _userContextService.GetUserIdFromContext();
            review.ContentConsumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);

            try
            {
                var result = await _reviewsService.ReviewAlbumAsync(review);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("my-reviews/{contentConsumerId}")]
        public async Task<IActionResult> GetMyReviewsAsync()
        {
            var userId = _userContextService.GetUserIdFromContext();
            var contentConsumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);

            try
            {
                var reviews = await _reviewsService.GetMyReviewsAsync(contentConsumerId);
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var deletedReview = await _reviewsService.DeleteReviewAsync(reviewId);
                return Ok(deletedReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> EditReviewAsync(EditReviewDto review)
        {
            var userId = _userContextService.GetUserIdFromContext();
            review.ContentConsumerId = (int)await _userContextService.GetContentConsumerIdByUserIdAsync(userId);
            try
            {
                var editedReview = await _reviewsService.EditReviewAsync(review);
                return Ok(editedReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}