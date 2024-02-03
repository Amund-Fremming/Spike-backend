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
            .Where(g => g.PublicGame == true)/*
            .Select(g => new
            {
                Game = g,
                Score = g.Voters.Any() ? ((double)g.Voters.Count(v => v != null && v.Vote) / g.Voters.Count()) * 100 : 0
            })
            .OrderByDescending(g => g.Score)
            .Take(15)
            .Select(g => g.Game)*/
            .ToListAsync();
    }

    public async Task CreateGame(Game game)
    {
        try
        {
            _context.Add(game);
            await _context.SaveChangesAsync();
        }
        catch(Exception)
        {
            Console.WriteLine("Error in crating game in repo.");
        }
    }

    public async Task DeleteGame(Game game)
    {
        _context.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task StartGame(Game game)
    {
        game.GameStarted = true;
        int numQ = await _context.Questions.CountAsync(q => q.GameId == game.GameId);
        game.NumberOfQuestions = numQ;

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

    public async Task UpdateVoterForGame(string deviceId, string gameId, bool vote)
    {
        Voter oldVoter = await _context.Voters
            .FirstOrDefaultAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId) ?? throw new KeyNotFoundException($"Voter with gameId: {gameId}, and deviceId: {deviceId}, does not exist!");

        oldVoter.Vote = vote;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesVoterExistForGame(string gameId, string deviceId)
    {
        return await _context.Voters
            .AnyAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId);
    }

    public async Task<bool> HaveGameStarted(string gameId)
    {
        return await _context.Games.AnyAsync(g => g.GameId == gameId && g.GameStarted);
    }

    public async Task<bool> DoesGameExist(string gameId)
    {
        return await _context.Games.AnyAsync(g => g.GameId == gameId);
    }

    public async Task<ICollection<Game>> SearchForGames(string searchString)
    {
        return await _context.Games
            .Where(g => g.GameId.Contains(searchString))
            .Take(25)
            .ToListAsync();
    }
}