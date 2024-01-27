using System.ComponentModel.DataAnnotations;
using Models;

namespace Models;

public class Game
{
    [Key]
    public string GameId { get; set; }
    public bool GameStarted { get; set; }
    public bool PublicGame { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public ICollection<Question?> Questions;
    public ICollection<User?> Users;

    public Game() {}

    public Game(string gameId, bool gameStarted, bool publicGame, int upvotes, int downvotes)
    {   
        GameId = gameId ?? throw new ArgumentNullException(nameof(gameId));
        GameStarted = gameStarted;
        PublicGame = publicGame;
        Upvotes = upvotes;
        Downvotes = downvotes;
    }
}