using Models;
using Repositories;

namespace Services;

public class GameService(GameRepository gameRepository, VoteRepository voteRepository)
{
    public readonly GameRepository _gameRepository = gameRepository;
    public readonly VoteRepository _voteRepository = voteRepository;

    public async Task<ICollection<Game>> GetPublicGamesByRating()
    {
        return await _gameRepository.GetPublicGamesByRating();
    }

    public async Task CreateGame(Game newGame)
    {
        Game? game = await _gameRepository.GetGameById(newGame.GameId);

        if(game != null)
        {
            throw new InvalidDataException($"Game with ID {newGame.GameId}, already exists!");
        }
        else
        {
            await _gameRepository.CreateGame(newGame);
        }
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

    public async Task SetGamePublicAndSetIcon(string gameId, string icon)
    {           
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        await _gameRepository.SetGamePublicAndSetIcon(game, icon);
    }

    public async Task<bool> HaveGameStarted(string gameId)
    {
        return await _gameRepository.HaveGameStarted(gameId);   
    }

    public async Task<bool> DoesGameExist(string gameId)
    {
        return await _gameRepository.DoesGameExist(gameId);
    }

    public async Task<ICollection<Game>> SearchForGames(string searchString)
    {
        return await _gameRepository.SearchForGames(searchString);
    }
}