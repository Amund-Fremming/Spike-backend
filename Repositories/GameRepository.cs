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

    public async Task CreateGame(Game game)
    {

    }

    public async Task DeleteGame(Game game)
    {

    }

    public async Task StartGame(Game game)
    {

    }

    public async Task SetGamePublic(Game game)
    {

    }

    public async Task CreateVoterForGame(Voter voter)
    {

    }

    public async Task UpdateVoterForGame(Voter oldVoter, Voter updatedVoter)
    {

    }

    public async Task DoesVoterExistForGame(int gameId, int deviceId)
    {

    }
}