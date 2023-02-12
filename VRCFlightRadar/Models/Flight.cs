namespace VRCFlightRadar.Models;

public record Flight(
    string Callsign,
    string TypeCode,
    string Registration,
    string InstanceId,
    string WorldId,
    float Latitude,
    float Longitude,
    int Altitude,
    int Heading,
    int Groundspeed) {

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset LastedUpdate = DateTimeOffset.Now;
}
