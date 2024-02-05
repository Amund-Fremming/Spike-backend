using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[ApiController]
[Route("spike/votes")]
public class VoteController(VoteService voteService) : ControllerBase
{
    public readonly VoteService _voteService = voteService;

    [HttpPost]
    public async Task<ActionResult> VoteOnGame([FromBody] Voter voter)
    {
        // Escape string, validere
        try
        {
            await _voteService.VoteOnGame(voter);
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