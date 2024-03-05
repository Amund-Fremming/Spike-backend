using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Repositories;
using System.Security;

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
        string escapedGameId = SecurityElement.Escape(gameId);

        try
        {
            return await _gameService.HaveGameStarted(escapedGameId);
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

    [HttpGet("gameexists")]
    public async Task<ActionResult<bool>> DoesGameExist([FromQuery] string gameId)
    {
        string escapedGameId = SecurityElement.Escape(gameId);

        try
        {
            return await _gameService.DoesGameExist(escapedGameId);
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

    [HttpGet("likedgames")]
    public async Task<ActionResult<ICollection<Game>>> GetLikedGames([FromQuery] string deviceId) 
    {
        string escapedDeviceId = SecurityElement.Escape(deviceId);

        try
        {
            return Ok(await _gameService.GetLikedGames(escapedDeviceId));
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

    [HttpGet("usersgames")]
    public async Task<ActionResult<ICollection<Game>>> GetCreatedGames([FromQuery] string deviceId)       
    {
        string escapedDeviceId = SecurityElement.Escape(deviceId);

        try
        {
            return Ok(await _gameService.GetCreatedGames(escapedDeviceId));
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

    [HttpPost("gamesbyrating")]
    public async Task<ActionResult<ICollection<Game>>> GetGamesSorted([FromBody] string deviceId) {       
        string escapedDeviceId = SecurityElement.Escape(deviceId);

        try
        {
            return Ok(await _gameService.GetPublicGamesByRating(escapedDeviceId));
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

    [HttpPost("searchgames")]
    public async Task<ActionResult> SearchForGames([FromQuery] string searchString, [FromBody] string deviceId)
    {
        string escapedSearchString = SecurityElement.Escape(searchString);

        try
        {
            ICollection<Game> result = await _gameService.SearchForGames(escapedSearchString, deviceId);
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
    
    [HttpPost]
    public async Task<ActionResult> CreateGame([FromBody] Game newGame)
    {
        newGame.GameId = SecurityElement.Escape(newGame.GameId);
        newGame.CreatorId = SecurityElement.Escape(newGame.CreatorId);

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

    [HttpPost("adddevice")]
    public async Task<ActionResult> AddDevice([FromBody] string deviceId)
    {
        string escapedDeviceId = SecurityElement.Escape(deviceId);  

        try
        {
            bool result = await _deviceRepo.AddDevice(escapedDeviceId);
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

    [HttpPost("deviceexists")]
    public async Task<ActionResult> DoesDeviceExist([FromQuery] string deviceId)
    {
        string escapedDeviceId = SecurityElement.Escape(deviceId);  

        try
        {
            bool result = await _deviceRepo.DoesDeviceExist(escapedDeviceId);
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

    [HttpPut("startgame")]
    public async Task<ActionResult> StartGame([FromBody] string gameId)
    {
        string escapedGameId = SecurityElement.Escape(gameId);  
        
        if(String.IsNullOrEmpty(gameId))
            return BadRequest("Input Invalid!");

        try
        {
            await _gameService.StartGame(escapedGameId);
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
    public async Task<ActionResult> PublishGame([FromBody] string icon, [FromQuery]string gameId)
    {
        string escapedGameId = SecurityElement.Escape(gameId);
        string escapedIcon = SecurityElement.Escape(icon);

        if(String.IsNullOrEmpty(gameId) || String.IsNullOrEmpty(icon))
            return BadRequest("Input invalid!");

        try
        {
            await _gameService.SetGamePublicAndSetIcon(escapedGameId, escapedIcon);
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
}
