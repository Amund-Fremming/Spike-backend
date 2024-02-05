using Data;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Calculates the upvote procentage, and sets it to the game object.
    /// Gets the users vote, and sets the users vote to the game property so we can display the suers votes in the frontend.
    /// </summary>
    /// <returns>A list of updated game objects</returns>
    public async Task<ICollection<Game>> GetPublicGamesByRating(string deviceId)
    {
        ICollection<Game> games = await _context.Games
            .Where(g => g.PublicGame == true)
            .Take(40)
            .ToListAsync();

        games = CalculateUpvotePercentage(games);
        games = AttachUsersVotes(games, deviceId);

        return games;
    }

    public async Task CreateGame(Game game)
    {
        await using(var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Add(game);
                await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch(Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    // Not in use
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

    // Denne må oppdatere votes!
    public async Task<ICollection<Game>> SearchForGames(string searchString)
    {
        return await _context.Games
            .Where(g => g.GameId.Contains(searchString))
            .Take(25)
            .ToListAsync();
    }

    public ICollection<Game> CalculateUpvotePercentage(ICollection<Game> games)
    {
        foreach(var game in games)
        {
            if(game.Voters != null && game.Voters.Any())
            {
                int percentageUpvotes = (int) (game.Voters.Count(v => v.Vote) / game.Voters.Count * 100);
                game.PercentageUpvotes = percentageUpvotes;
            } else {
                game.PercentageUpvotes = 0;
            }
        }

        return games;
    }

    public ICollection<Game> AttachUsersVotes(ICollection<Game> games, string deviceId)
    {
        foreach(var game in games)
        {
            if(game.Voters != null && game.Voters.Any())
            {
                bool usersVote = game.Voters
                    .Where(v => v.UserDeviceId == deviceId)
                    .Select(v => v.Vote)
                    .FirstOrDefault();

                int votedValue = usersVote ? 1 : !usersVote ? 0 : 3;
                game.UsersVote = votedValue;
            }
        }               

        return games;
    }
}