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

    public async Task JoinGroup(string gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
    }

}
