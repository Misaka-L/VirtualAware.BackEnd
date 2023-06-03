using VirtualAware.BackEnd.Api.Models;

namespace VirtualAware.BackEnd.Api.Services;

public class FlightRadarService {
    private readonly List<Flight> _flights = new List<Flight>();
    private Timer _timer;

    private readonly ILogger<FlightRadarService> _logger;

    public FlightRadarService(ILogger<FlightRadarService> logger) {
        _timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        _logger = logger;
    }

    public void TrackFlight(Flight flight) {
        var index = _flights.FindIndex
            (f => f.InstanceId == flight.InstanceId & f.WorldId == flight.WorldId & f.Registration == flight.Registration & f.Callsign == flight.Callsign);

        if (index != -1) {
            flight.Id = _flights[index].Id;
            _flights[index] = flight;
            _logger.LogInformation("Flight Update: {FlightData}", flight);
            return;
        }

        _flights.Add(flight);
        _logger.LogInformation("Flight Added: {FlightData}", flight);
    }

    public List<Flight> GetAllFlights() =>
        _flights;
    public List<Flight> GetAllFlights(string worldId) =>
    _flights.Where(flight => flight.WorldId == worldId).ToList();
    public List<Flight> GetAllFlights(string instanceId, string worldId) =>
        _flights.Where(flight => flight.InstanceId == instanceId & flight.WorldId == worldId).ToList();

    private void Tick(object? state) {
        _flights.RemoveAll(flight => DateTimeOffset.Now.ToUnixTimeSeconds() - flight.LastedUpdate.ToUnixTimeSeconds() > 10);
    }
}
