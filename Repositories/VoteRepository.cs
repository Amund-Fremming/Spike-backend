using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class VoteRepository(AppDbContext context) {

    public readonly AppDbContext _context = context;

    public async Task CreateVoterForGame(Voter voter)
    {
        _context.Add(voter);

        await UpdateGameWithVote(voter.GameId, voter.Vote);
    }

    public async Task UpdateVoterForGame(string deviceId, string gameId, int vote)
    {
        Voter oldVoter = await _context.Voters
            .FirstOrDefaultAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId) ?? throw new KeyNotFoundException($"Voter with gameId: {gameId}, and deviceId: {deviceId}, does not exist!");

        oldVoter.Vote = vote;

        await UpdateGameWithVote(gameId, vote);
    }

    public async Task UpdateGameWithVote(string gameId, int vote)
    {
        // Updates game here, rather than when fetching games for better user experience.
        using(var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                Game game = await _context.Games.FirstAsync(g => g.GameId == gameId);
                int voteValue = vote == 0 ? - 1 : vote == 1 ? 1 : 0;

                game.Upvotes = game.Upvotes + voteValue;
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

    public async Task<Voter?> DoesVoterExistForGame(string gameId, string deviceId)
    {
        return await _context.Voters
            .FirstOrDefaultAsync(v => v.GameId == gameId && v.UserDeviceId == deviceId);
    }
}