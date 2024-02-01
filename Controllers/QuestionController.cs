using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Services;
using System.Security;
using Hubs;

namespace Controllers;

[ApiController]
[Route("spike/questions")]
public class Controller(QuestionService service, IHubContext<GameHub> hubContext) : ControllerBase
{
    public readonly QuestionService _service = service;
    public readonly IHubContext<GameHub> _hubContext = hubContext;

    [HttpGet]
    public async Task<ActionResult> GetGameQuestions([FromQuery] string gameId)
    {
        if(String.IsNullOrEmpty(gameId))
            return BadRequest("Input invalid!");

        string gameIdSaniticed;

        try
        {
            gameIdSaniticed = SecurityElement.Escape(gameId);
            var questions = await _service.GetGameQuestionsAsync(gameIdSaniticed);
            return Ok(questions);
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
    public async Task<ActionResult> AddQuestion([FromBody] Question question)
    {
        if(question.GameId == null || String.IsNullOrEmpty(question.QuestionStr))
            return BadRequest();

        string gameIdEscaped;
        string questionStringEscaped;

        try
        {
            gameIdEscaped = SecurityElement.Escape(question.GameId);
            questionStringEscaped = SecurityElement.Escape(question.QuestionStr);

            Question saniticedQuestion = new Question(gameIdEscaped, questionStringEscaped);
            await _service.AddQuestionAsyncTransaction(saniticedQuestion);

            var count = _service.GetNumberOfQuestions(question.GameId);
            await _hubContext.Clients.All.SendAsync("ReceiveQuestionCount", question.GameId, count);

            return Ok("Question added!");
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