using Microsoft.AspNetCore.SignalR;
using Services;

namespace Hubs;

public class GameHub(QuestionService service) : Hub
{
    public readonly QuestionService _service = service;

    public async Task QuestionAdded(string gameId)
    {
        int count = await _service.GetNumberOfQuestions(gameId);
        await Clients.All.SendAsync("ReceiveQuestionCount", gameId, count);
    }
}
