using Microsoft.AspNetCore.Mvc;
using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StarWarsController(ICharacterService starWarsService) : ControllerBase
    {
        private readonly ICharacterService _starWarsService = starWarsService;

        [HttpGet("characters")]
        public async Task<ActionResult<CharacterResponse>> GetAllCharacters(
            [FromQuery] int page = 1,
            [FromQuery] int? limit = 10,
            [FromQuery] string? search = null)
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
        public async Task<ActionResult<Character>> GetCharacterById(string id)
        {
            try
            {
                var character = await _starWarsService.GetCharacterByIdAsync(id);
                return Ok(character);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}