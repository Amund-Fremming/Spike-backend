using System.Web;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;
using System.Security;

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

        return Ok("Question added!");
    }
}