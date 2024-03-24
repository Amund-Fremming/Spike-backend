using System.ComponentModel.DataAnnotations;

namespace Models;

public class Player
{
    [Key]
    public string DeviceId { get; set; }

    public Player(string deviceId)
    {
        DeviceId = deviceId;
    }
}
