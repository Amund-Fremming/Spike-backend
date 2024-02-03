using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Controllers;

[ApiController]
[Route("spike/games")]
public class GameController(GameService gameService, QuestionService questionService, DeviceRepository deviceRepo) : ControllerBase
{
    public readonly GameService _gameService = gameService;
    public readonly QuestionService _questionService = questionService;
    public readonly DeviceRepository _deviceRepo = deviceRepo;

    [HttpGet("gamestarted")]
    public async Task<ActionResult<bool>> HaveGameStarted([FromQuery] string gameId)
    {
        return await _gameService.HaveGameStarted(gameId);
    }

    [HttpGet("gameexists")]
    public async Task<ActionResult<bool>> DoesGameExist([FromQuery] string gameId)
    {
        return await _gameService.DoesGameExist(gameId);
    }

    [HttpGet("gamesbyrating")]
    public async Task<ActionResult<ICollection<Game>>> GetGamesSorted() {
        try
        {
            return Ok(await _gameService.GetPublicGamesByRating());
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpPost]
    public async Task<ActionResult> CreateGame([FromBody] Game newGame)
    {
        try
        {
            await _gameService.CreateGame(newGame);
            return Ok("Game created!");
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(InvalidDataException) 
        {
            return BadRequest("GAME_EXISTS");
        }
        catch(Exception e)
        {
            Console.WriteLine("JHer fanger vi");
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteGame([FromBody] string gameId)
    {
        if(String.IsNullOrEmpty(gameId))
            return BadRequest("Input invalid!");

        try
        {
            await _gameService.DeleteGame(gameId);
            return Ok("Game Deleted!");
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("startgame")]
    public async Task<ActionResult> StartGame([FromBody] string gameId)
    {
        if(String.IsNullOrEmpty(gameId))
            return BadRequest("Input Invalid!");

        try
        {
            await _gameService.StartGame(gameId);
            return Ok("Game started!");
        }
        catch(KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("publishgame")]
    public async Task<ActionResult> PublishGame([FromBody] string gameId)
    {
        if(String.IsNullOrEmpty(gameId))
            return BadRequest("Input invalid!");

        try
        {
            await _gameService.SetGamePublic(gameId);
            return Ok("Game Published!");
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("vote")]
    public async Task<ActionResult> VoteOnGame([FromBody] Voter voter)
    {
        try
        {
            await _gameService.VoteOnGame(voter);
            return Ok("Vote Submitted!");
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("searchgames")]
    public async Task<ActionResult> SearchForGames([FromQuery] string searchString)
    {
        try
        {
            ICollection<Game> result = await _gameService.SearchForGames(searchString);
            return Ok(result);
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("adddevice")]
    public async Task<ActionResult> AddDevice([FromBody] string deviceId)
    {
        try
        {
            bool result = await _deviceRepo.AddDevice(deviceId);
            return Ok(result);
        }
        catch(KeyNotFoundException e)
        {
           return NotFound(e.Message);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}