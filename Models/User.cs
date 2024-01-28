using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    [Key]
    public long UserDeviceId { get; set; }
    public int GameId { get; set; }
    public Game? Game { get; set; }

    public User() {}

    public User(long userDeviceId, int gameId)
    {
        UserDeviceId = userDeviceId;
        GameId = gameId;
    }
}