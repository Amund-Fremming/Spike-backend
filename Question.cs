using System.ComponentModel.DataAnnotations;

namespace Model;

public class Question
{   
    [Key]
    public int Id { get; set; }
    public int GameId { get; set; }
    public string QuestionStr { get; set; }

    public Question(int gameId, string questionStr)
    {
        GameId = gameId;
        QuestionStr = questionStr ?? throw new ArgumentNullException(nameof(questionStr));
    }
}