using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Controllers;

[ApiController]
[Route("spike/games")]
public class GameController(GameService gameService, QuestionService questionService, AppDbContext context) : ControllerBase
{
    public readonly GameService _gameService = gameService;
    public readonly QuestionService _questionService = questionService;
    public readonly AppDbContext _context = context;

    [HttpGet("gamestarted")]
    public async Task<ActionResult<bool>> HaveGameStarted([FromQuery] string gameId)
    {
        return await _context.Games.AnyAsync(g => g.GameId == gameId && g.GameStarted);
    }

    [HttpGet("gameexists")]
    public async Task<ActionResult<bool>> DoesGameExist([FromQuery] string gameId)
    {
        return await _context.Games.AnyAsync(g => g.GameId == gameId);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> CreateGame([FromBody] Game newGame)
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

    // GET GAMES BY RATING!!

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
}