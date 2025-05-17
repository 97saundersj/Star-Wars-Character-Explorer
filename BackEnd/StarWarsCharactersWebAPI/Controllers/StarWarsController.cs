using Microsoft.AspNetCore.Mvc;
using StarWarsCharactersWebAPI.Services;
using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StarWarsController : ControllerBase
    {
        private readonly IStarWarsService _starWarsService;

        public StarWarsController(IStarWarsService starWarsService)
        {
            _starWarsService = starWarsService;
        }

        [HttpGet("characters")]
        public async Task<ActionResult<StarWarsResponse>> GetAllCharacters(
            [FromQuery] int page = 1,
            [FromQuery] int? limit = null,
            [FromQuery] string search = null)
        {
            try
            {
                var response = await _starWarsService.GetAllCharactersAsync(page, limit, search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("characters/{id}")]
        public async Task<ActionResult<StarWarsCharacter>> GetCharacterById(string id)
        {
            try
            {
                var character = await _starWarsService.GetCharacterByIdAsync(id);
                if (character == null)
                {
                    return NotFound($"Character with ID {id} not found");
                }
                return Ok(character);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 