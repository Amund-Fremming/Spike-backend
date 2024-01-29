using Data;
using Models;

namespace Repositories;

public class GameRepository(AppDbContext context)
{
    public readonly AppDbContext _context = context;

    public async Task<Game?> GetGameById(string gameId)
    {
        try
        {
            return await _context.Games.FindAsync(gameId);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Game>> GetPublicGamesByRating()
    {

    }

    public async Task<Game> CreateGame(Game game)
    {

    }

    public async Task<Game> DeleteGame(Game game)
    {

    }

    /// <summary>
    /// Sets the game GameStarted property to true. 
    /// </summary>
    /// <param name="gameId"></param>
    public async Task StartGame(string gameId)
    {

    }

    /// <summary>
    /// Sets the game PublicGame property to true. 
    /// </summary>
    /// <param name="gameId"></param>
    public async Task SetGamePublic(string gameId)
    {

    }

    /// <summary>
    /// Adds a voter to a game
    /// </summary>
    /// <param name="voter"></param>
    public async Task CreateVoterForGame(Voter voter)
    {

    }

    public async Task UpdateVoterForGame(Voter oldVoter, Voter updatedVoter)
    {

    }


}