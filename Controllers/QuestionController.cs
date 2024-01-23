using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model;
using Repositories;
using System.Security;
using Hubs;

namespace Controllers;

[ApiController]
[Route("spike")]
public class Controller : ControllerBase
{
    public readonly QuestionRepo _repo;
    public readonly IHubContext<GameHub> _hubContext;

    public Controller(QuestionRepo repo, IHubContext<GameHub> hubContext)
    {
        _repo = repo;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult> GetGameQuestions([FromQuery] string gameId)
    {
        string gameIdSaniticed;
        try
        {
            gameIdSaniticed = SecurityElement.Escape(gameId);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        var questions = await _repo.GetGameQuestionsAsync(gameIdSaniticed);
        return Ok(questions);
    }

    [HttpPost]
    public async Task<ActionResult> AddQuestion([FromBody] Question question)
    {
        if(question.GameId == null || question.QuestionStr == null)
            return BadRequest();

        string gameIdEscaped;
        string questionStringEscaped;
        try
        {
            gameIdEscaped = SecurityElement.Escape(question.GameId);
            questionStringEscaped = SecurityElement.Escape(question.QuestionStr);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        Question saniticedQuestion = new Question(gameIdEscaped, questionStringEscaped);
        await _repo.AddQuestionAsyncTransaction(saniticedQuestion);

        var count = _repo.GetNumberOfQuestions(question.GameId);
        await _hubContext.Clients.All.SendAsync("ReceiveQuestionCount", question.GameId, count);

        return Ok("Question added!");
    }
}