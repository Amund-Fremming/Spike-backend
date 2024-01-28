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

    public async Task StartGame(string gameId)
    {

    }

    public async Task SetGamePublic(string gameId)
    {

    }

    public async Task

    public async Task
}