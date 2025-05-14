using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private static List<Review> _reviews = new List<Review>();

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            review.Id = Guid.NewGuid().ToString();
            review.CreatedAt = DateTime.UtcNow;
            _reviews.Add(review);

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(string id)
        {
            var review = _reviews.Find(r => r.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpGet("character/{characterName}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetCharacterReviews(string characterName)
        {
            var reviews = _reviews.FindAll(r => r.CharacterName == characterName);
            return Ok(reviews);
        }
    }

    public class Review
    {
        public string Id { get; set; }
        public string CharacterName { get; set; }
        public string ReviewerName { get; set; }
        public DateTime WatchDate { get; set; }
        public string ReviewDetail { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 