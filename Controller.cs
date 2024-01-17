using System.Web;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace backend;

[ApiController]
[Route("spike")]
public class Controller : ControllerBase
{
    public readonly QuestionRepo _repo;

    public Controller(QuestionRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetGameQuestions([FromQuery] string gameId)
    {
        string gameIdEscaped;
        try
        {
            gameIdEscaped = HttpUtility.HtmlEncode(gameId);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        var questions = await _repo.GetGameQuestionsAsync(gameIdEscaped);
        return Ok(questions);
    }

    [HttpPost]
    public async Task<ActionResult> AddQuestion([FromBody] Question question)
    {
        try
        {
            // Sanitice input
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        await _repo.AddQuestionAsyncTransaction(question);

        return Ok("Question added!");
    }
}