using Data;
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

    public async Task<ICollection<Game>> GetPublicGamesByRating(string deviceId)
    {
        ICollection<Game> games = await _context.Games
            .Where(g => g.PublicGame == true)
            .Take(40)
            .OrderByDescending(g => g.Upvotes)
            .ToListAsync();

        games = await AttachUsersVotes(games, deviceId);

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

        games = await AttachUsersVotes(games, deviceId);
        games = games.OrderByDescending(g => g.Upvotes).ToList();

        return games;
    }

    public async Task<ICollection<Game>> AttachUsersVotes(ICollection<Game> games, string deviceId)
    {
        foreach(var game in games)
        {
            Voter? userHaveVoted = await _voteRepo.DoesVoterExistForGame(game.GameId, deviceId);
            
            game.UsersVote = userHaveVoted == null ? 3 : userHaveVoted.Vote;
        }               

        return games;
    }

    public async Task<ICollection<Game>> GetLikedGames(string deviceId)
    {
        ICollection<Game> games = await _context.Voters
            .Where(v => v.UserDeviceId == deviceId && v.Vote == 1)
            .Select(v => v.Game!)
            .Where(g => g != null && g.CreatorId != deviceId)
            .ToListAsync();

        if(games.Count == 0)
        {
            return games;
        }

        games = await AttachUsersVotes(games, deviceId);
        games = games.OrderByDescending(g => g.Upvotes).ToList();

        return games;
    }

    public async Task<ICollection<Game>> GetCreatedGames(string deviceId)
    {
        ICollection<Game> games = await _context.Games
            .Where(g => g.CreatorId == deviceId)
            .ToListAsync();

        if(games.Count == 0)
        {
            return games;
        }

        games = await AttachUsersVotes(games, deviceId);
        games = games.OrderByDescending(g => g.Upvotes).ToList();

        return games;
    }
}