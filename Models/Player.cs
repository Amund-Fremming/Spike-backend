using System.ComponentModel.DataAnnotations;

namespace Models;

public class Player
{
    [Key]
    public string DeviceId { get; set; }
    public string GameId { get; set; }
    public Game? Game { get; set; }

    public Player(string deviceId, string gameId)
    {
        DeviceId = deviceId;
        GameId = gameId;
    }
}
