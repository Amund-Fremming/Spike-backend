using System.ComponentModel.DataAnnotations;

namespace Models;

public class Device {

    [Key]
    public long Id { get; set; }
    public string UserDeviceId { get; set; }

    public Device() {}

    public Device(string userDeviceId) {
        UserDeviceId = userDeviceId;
    }
}