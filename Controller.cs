using System.Web;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace backend;

[Route("spike")]
[ApiController]
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
    public async Task<ActionResult> AddQuestion([FromQuery] string question, [FromQuery] string gameId)
    {
        string gameIdEscaped;
        string questionEscaped;
        try
        {
            gameIdEscaped = HttpUtility.HtmlEncode(gameId);
            questionEscaped = HttpUtility.HtmlEncode(question);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        Question Question = new Question(gameIdEscaped, questionEscaped);
        await _repo.AddQuestionAsyncTransaction(Question);

        return Ok("Question added!");
    }
}