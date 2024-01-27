using Data;

namespace Repositories;

public class GameRepository
{
    public readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }
}