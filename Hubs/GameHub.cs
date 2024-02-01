using Microsoft.AspNetCore.SignalR;
using Services;

namespace Hubs;

public class GameHub : Hub
{
    private readonly QuestionService _service;

    public GameHub(QuestionService service)
    {
        _service = service;
    }

    public async Task QuestionAdded(string gameId)
    {
        int count = _service.GetNumberOfQuestions(gameId);
        await Clients.All.SendAsync("ReceiveQuestionCount", gameId, count);
    }
}
