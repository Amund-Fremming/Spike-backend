using System.ComponentModel.DataAnnotations;

namespace Models;

public class Voter
{
    [Key]
    public int VoterId { get; set; }
    public string UserDeviceId { get; set; }
    public string GameId { get; set; }
    public Game? Game { get; set; }
    public int Vote { get; set; }

    public Voter() { GameId = ""; UserDeviceId = ""; }

    public Voter(string userDeviceId, string gameId, int vote)
    {
        UserDeviceId = userDeviceId;
        GameId = gameId;
        Vote = vote;
    }
}