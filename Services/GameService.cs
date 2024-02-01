using Models;
using Repositories;

namespace Services;

public class GameService(GameRepository gameRepository)
{
    public readonly GameRepository _gameRepository = gameRepository;

    public async Task<ICollection<Game>> GetPublicGamesByRating()
    {
        return await _gameRepository.GetPublicGamesByRating();
    }

    public async Task CreateGame(Game newGame)
    {
        Game? game = await _gameRepository.GetGameById(newGame.GameId);

        if(game != null)
        {
            throw new Exception($"Game with ID {newGame.GameId}, already exists!");
        }

        await _gameRepository.CreateGame(newGame);
    }

    public async Task DeleteGame(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        await _gameRepository.DeleteGame(game);
    }

    public async Task StartGame(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        await _gameRepository.StartGame(game);
    }

    public async Task SetGamePublic(string gameId)
    {           
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        await _gameRepository.SetGamePublic(game);
    }

    public async Task VoteOnGame(Voter voter)
    {
        Game game = await _gameRepository.GetGameById(voter.GameId) ?? throw new KeyNotFoundException($"Game with ID {voter.GameId}, does not exist!");

        bool voterExistsForGame = await _gameRepository.DoesVoterExistForGame(voter.GameId, voter.UserDeviceId);

        if(voterExistsForGame)
        {
            await _gameRepository.UpdateVoterForGame(voter.UserDeviceId, voter.GameId, voter.Vote);
        }
        else
        {
            await _gameRepository.CreateVoterForGame(voter);
        }
    }

    public async Task<ICollection<Game>> SearchForGame(string searchString)
    {
        // TODO
        return null;   
    }
}