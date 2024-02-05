using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class GameRepository(AppDbContext context, VoteRepository voteRepo)
{
    public readonly AppDbContext _context = context;
    public readonly VoteRepository _voteRepo = voteRepo;

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

        games = await CalculateUpvotePercentage(games);
        games = await AttachUsersVotes(games, deviceId);
        games = games.OrderBy(g => g.PercentageUpvotes).ToList();

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

    public async Task<ICollection<Game>> SearchForGames(string searchString, string deviceId)
    {
        ICollection<Game> games = await _context.Games
            .Where(g => g.GameId.Contains(searchString))
            .Take(40)
            .ToListAsync();

        games = await CalculateUpvotePercentage(games);
        games = await AttachUsersVotes(games, deviceId);
        games = games.OrderByDescending(g => g.PercentageUpvotes).ToList();

        return games;
    }

    public async Task<ICollection<Game>> CalculateUpvotePercentage(ICollection<Game> games)
    {
        foreach(var game in games)
        {
            ICollection<Voter> voters = await _voteRepo.GetVotersForGame(game.GameId); 

            if(voters != null && voters.Count > 0)
            {
                double percentageUpvotes = (double)voters.Count(v => v.Vote == 1) / voters.Count * 100;

                game.PercentageUpvotes = (int)percentageUpvotes;
            } else {
                game.PercentageUpvotes = 0;
            }
        }

        return games;
    }

    public async Task<ICollection<Game>> AttachUsersVotes(ICollection<Game> games, string deviceId)
    {
        foreach(var game in games)
        {
            bool userHaveVoted = await _voteRepo.DoesVoterExistForGame(game.GameId, deviceId);
            ICollection<Voter> voters = null;
            if(userHaveVoted)
            {
                voters = await _voteRepo.GetVotersForGame(game.GameId); 
            }

            if(voters != null && voters.Count > 0)
            {
                var userVote = voters.FirstOrDefault(v => v.UserDeviceId == deviceId);
                game.UsersVote = userVote == null ? 2 : userVote.Vote;
            }
        }               

        return games;
    }
}