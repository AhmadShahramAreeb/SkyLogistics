namespace Entities.Models;

public class Drone
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }

    // Serial number for tracking.
    // Takip için seri numarası.
    public string SerialNumber { get; set; }
    public double BatteryLevel { get; set; }

    // Current operational status: "Idle", "Flying", "Charging", "Maintenance".
    // Mevcut operasyonel durum: "Boşta", "Uçuşta", "Şarjda", "Bakımda".
    public string Status { get; set; }
}