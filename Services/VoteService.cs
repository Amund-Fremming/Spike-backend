using Models;
using Repositories;

namespace Services;

public class VoteService(VoteRepository voteRepository, GameRepository gameRepository)
{
    public readonly VoteRepository _voteRepository = voteRepository;
    public readonly GameRepository _gameRepository = gameRepository;

    public async Task VoteOnGame(Voter voter)
    {
        Game game = await _gameRepository.GetGameById(voter.GameId) ?? throw new KeyNotFoundException($"Game with ID {voter.GameId}, does not exist!");

        Voter? voterExistsForGame = await _voteRepository.DoesVoterExistForGame(voter.GameId, voter.UserDeviceId);

        if(voterExistsForGame != null && voterExistsForGame.Vote == voter.Vote)
        {
            return;
        }

        if(voterExistsForGame != null)
        {
            await _voteRepository.UpdateVoterForGame(voter.UserDeviceId, voter.GameId,voter.Vote, voterExistsForGame);
        }
        else
        {
            await _voteRepository.CreateVoterForGame(voter);
        }
    }
}