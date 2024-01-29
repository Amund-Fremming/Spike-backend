using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class GameRepository(AppDbContext context)
{
    public readonly AppDbContext _context = context;

    public async Task<Game?> GetGameById(string gameId)
    {
        return await _context.Games.FindAsync(gameId);
    }

    public async Task<ICollection<Game>> GetPublicGamesByRating()
    {
        return await _context.Games
            .Where(g => g.PublicGame == true)
            .ToListAsync();
    }

    public async Task CreateGame(Game game)
    {
        _context.Add(game);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGame(Game game)
    {
        _context.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task StartGame(Game game)
    {
        game.GameStarted = true;
        await _context.SaveChangesAsync();
    }

    public async Task SetGamePublic(Game game)
    {
        game.PublicGame = true;
        await _context.SaveChangesAsync();
    }

    public async Task CreateVoterForGame(Voter voter)
    {
        _context.Add(voter);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVoterForGame(long deviceId, string gameId, bool vote)
    {
        Voter oldVoter = await _context.Voters
            .FirstOrDefaultAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId) ?? throw new KeyNotFoundException($"Voter with gameId: {gameId}, and deviceId: {deviceId}, does not exist!");

        oldVoter.Vote = vote;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesVoterExistForGame(string gameId, long deviceId)
    {
        return await _context.Voters
            .AnyAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId);
    }
}