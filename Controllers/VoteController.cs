using System.Security;
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
        voter.GameId = SecurityElement.Escape(voter.GameId);
        voter.UserDeviceId = SecurityElement.Escape(voter.UserDeviceId);

        try
        {
            await _voteService.VoteOnGame(voter);
            return Ok("Vote Submitted!");
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}
