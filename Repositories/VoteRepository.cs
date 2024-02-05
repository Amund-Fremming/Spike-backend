using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories;

public class VoteRepository(AppDbContext context) {

    public readonly AppDbContext _context = context;

    public async Task CreateVoterForGame(Voter voter)
    {
        _context.Add(voter);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVoterForGame(string deviceId, string gameId, int vote)
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

    public async Task<ICollection<Voter>> GetVotersForGame(string gameId)
    {
        return await _context.Voters
            .Where(v => v.GameId == gameId)
            .ToListAsync();
    }
}