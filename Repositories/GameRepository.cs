using Data;
using Models;

namespace Repositories;

public class GameRepository
{
    public readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetGameById(string gameId)
    {
        try
        {
            return await _context.Games.FindAsync(gameId);
        }
        catch(Exception e)
        {
            throw new InvalidOperationException($"");
        }
    }
}