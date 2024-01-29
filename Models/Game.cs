using System.ComponentModel.DataAnnotations;

namespace Models;

public class Game
{
    [Key]
    public string GameId { get; set; }
    public bool GameStarted { get; set; }
    public bool PublicGame { get; set; }
    public string IconImageUrl { get; set; }
    public ICollection<Question?> Questions;
    public ICollection<Voter?> Voters;

    public Game() {}

    public Game(string gameId, bool gameStarted, bool publicGame, string iconImageUrl)
    {   
        GameId = gameId ?? throw new ArgumentNullException(nameof(gameId));
        GameStarted = gameStarted;
        PublicGame = publicGame;
        IconImageUrl = iconImageUrl;
    }
}