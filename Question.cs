using System.ComponentModel.DataAnnotations;

namespace Model;

public class Question
{   
    [Key]
    public int Id { get; set; }
    public string GameId { get; set; }
    public string QuestionStr { get; set; }

    public Question(string gameId, string questionStr)
    {
        GameId = gameId ?? throw new ArgumentNullException(nameof(gameId));
        QuestionStr = questionStr ?? throw new ArgumentNullException(nameof(questionStr));
    }
}