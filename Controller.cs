using System.Web;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace backend;

[Route("frug")]
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
        int gameIdAsInteger;
        try
        {
            gameIdAsInteger = int.Parse(gameId);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        var questions = await _repo.GetGameQuestionsAsync(gameIdAsInteger);
        return Ok(questions);
    }

    [HttpPost]
    public async Task<ActionResult> AddQuestion([FromQuery] string question, [FromQuery] string gameId)
    {
        int gameIdAsInteger;
        string validatedString;
        try
        {
            gameIdAsInteger = int.Parse(gameId);
            validatedString = HttpUtility.HtmlEncode(question);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }

        Question Question = new Question(gameIdAsInteger, validatedString);
        await _repo.AddQuestionAsyncTransaction(Question);

        return Ok("Question added!");
    }
}