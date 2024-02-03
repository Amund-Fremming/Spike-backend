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
            .Take(40)
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

    public async Task SetGamePublicAndSetIcon(Game game, string icon)
    {
        game.PublicGame = true;
        game.IconImage = icon;
        await _context.SaveChangesAsync();
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