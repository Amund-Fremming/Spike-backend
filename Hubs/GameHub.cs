using Microsoft.AspNetCore.SignalR;
using Repositories;

namespace Hubs;

public class GameHub : Hub
{
    public readonly QuestionRepo _repo;

    public GameHub(QuestionRepo repo) 
    {
        _repo = repo;
    }

    public async Task QuestionAdded(string gameId)
    {
        int count = _repo.GetNumberOfQuestions(gameId);
        await Clients.All.SendAsync("ReceiveQuestionCount", gameId, count);
    }
}
