using System.ComponentModel.DataAnnotations;

namespace Models;

public class Game
{
    [Key]
    public string GameId { get; set; }
    public bool GameStarted { get; set; }
    public bool PublicGame { get; set; }
    public string IconImage { get; set; }
    public int NumberOfQuestions { get; set; }
    public int? PercentageUpvotes { get; set; }
    public int? UsersVote { get; set; }
    public ICollection<Question>? Questions;
    public ICollection<Voter>? Voters;

    public Game() { GameId = ""; IconImage = "NICE"; }

    public Game(string gameId, bool gameStarted, bool publicGame, string iconImage, int numberOfQuestions)
    {   
        GameId = gameId ?? throw new ArgumentNullException(nameof(gameId));
        GameStarted = gameStarted;
        PublicGame = publicGame;
        IconImage = iconImage;
        NumberOfQuestions = numberOfQuestions;
    }
}