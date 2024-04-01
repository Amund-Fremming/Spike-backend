using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Services;
using System.Security;
using Hubs;

namespace Controllers;

[ApiController]
[Route("spike/questions")]
public class QuestionController(QuestionService service, IHubContext<GameHub> hubContext) : ControllerBase
{
    public readonly QuestionService _service = service;
    public readonly IHubContext<GameHub> _hubContext = hubContext;

    [HttpGet]
    public async Task<ActionResult> GetGameQuestions([FromQuery] string gameId)
    {
        string gameIdSaniticed = SecurityElement.Escape(gameId);

        try
        {
            var questions = await _service.GetGameQuestionsAsync(gameIdSaniticed);
            return Ok(questions);
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

    [HttpPost]
    public async Task<ActionResult> AddQuestion([FromBody] Question question)
    {
        if (question.GameId == null || String.IsNullOrEmpty(question.QuestionStr))
            return BadRequest();

        string gameIdEscaped = SecurityElement.Escape(question.GameId);
        string questionStringEscaped = SecurityElement.Escape(question.QuestionStr);

        try
        {
            Question saniticedQuestion = new Question(gameIdEscaped, questionStringEscaped);
            await _service.AddQuestionAsyncTransaction(saniticedQuestion);

            var count = _service.GetNumberOfQuestions(question.GameId);
            await _hubContext.Clients.All.SendAsync("ReceiveQuestionCount", question.GameId, count);

            return Ok("Question added!");
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
