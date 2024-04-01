using Models;
using Repositories;

namespace Services;

public class GameService(GameRepository gameRepository, VoteRepository voteRepository, DeviceRepository deviceRepository)
{
    public readonly GameRepository _gameRepository = gameRepository;
    public readonly VoteRepository _voteRepository = voteRepository;
    public readonly DeviceRepository _deviceRepository = deviceRepository;


    public async Task<ICollection<Game>> GetPublicGamesByRating(string deviceId)
    {
        Device devide = await _deviceRepository.GetDeviceById(deviceId) ?? throw new KeyNotFoundException($"Device with ID {deviceId}, does not exist!");

        return await _gameRepository.GetPublicGamesByRating(deviceId);
    }

    public async Task CreateGame(Game newGame)
    {
        Game? game = await _gameRepository.GetGameById(newGame.GameId);

        if (game != null)
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
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        return await _gameRepository.HaveGameStarted(gameId);
    }

    public async Task<bool> DoesGameExist(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        return await _gameRepository.DoesGameExist(gameId);
    }

    public async Task<ICollection<Game>> SearchForGames(string searchString, string deviceId)
    {
        Device devide = await _deviceRepository.GetDeviceById(deviceId) ?? throw new KeyNotFoundException($"Device with ID {deviceId}, does not exist!");

        return await _gameRepository.SearchForGames(searchString, deviceId);
    }

    public async Task<ICollection<Game>> GetLikedGames(string deviceId)
    {
        Device devide = await _deviceRepository.GetDeviceById(deviceId) ?? throw new KeyNotFoundException($"Device with ID {deviceId}, does not exist!");

        return await _gameRepository.GetLikedGames(deviceId);
    }

    public async Task<ICollection<Game>> GetCreatedGames(string deviceId)
    {
        Device devide = await _deviceRepository.GetDeviceById(deviceId) ?? throw new KeyNotFoundException($"Device with ID {deviceId}, does not exist!");

        return await _gameRepository.GetCreatedGames(deviceId);
    }

}
