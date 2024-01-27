using Models;
using Repositories;

namespace Services;

public class GameService(GameRepository gameRepository)
{
    public readonly GameRepository _gameRepository = gameRepository;

    public async Task<Game?> GetGameById(string gameId)
    {
        return await _gameRepository.GetGameById(gameId);
    }
    
}