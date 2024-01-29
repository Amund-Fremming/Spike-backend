using Models;
using Repositories;

namespace Services;

public class GameService(GameRepository gameRepository)
{
    public readonly GameRepository _gameRepository = gameRepository;

    public async Task<ICollection<Game>> GetPublicGamesByRating()
    {

    }

    public async Task CreateGame(Game game)
    {

    }

    public async Task DeleteGame(string gameId)
    {

    }

    public async Task StartGame(string gameId)
    {

    }

    public async Task SetGamePublic(string gameId)
    {

    }

    public async Task VoteOnGame(int deviceId, int gameId, bool vote)
    {
        // Check if a voter with the given deviceId exists for a game
            // If, update the Voter
            // Else, create a voter
    }
}