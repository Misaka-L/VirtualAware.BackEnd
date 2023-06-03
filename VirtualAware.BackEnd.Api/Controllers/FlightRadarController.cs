using Microsoft.AspNetCore.Mvc;
using VirtualAware.BackEnd.Api.Models;
using VirtualAware.BackEnd.Api.Services;

namespace VirtualAware.BackEnd.Api.Controllers;
[Route("flightradar")]
[ApiController]
public class FlightRadarController : ControllerBase {
    private readonly FlightRadarService _flightRadarService;

    public FlightRadarController(FlightRadarService flightRadarService) {
        _flightRadarService = flightRadarService;
    }

    [HttpGet, HttpPost]
    [Route("track")]
    public IActionResult Track(
        string callsign,
        string typeCode,
        string registration,
        string instanceId,
        string worldId,
        float latitude,
        float longitude,
        int altitude,
        int heading,
        int groundspeed) {

        var flight = new Flight(
            Callsign: callsign,
            TypeCode: typeCode,
            Registration: registration,
            InstanceId: instanceId,
            WorldId: worldId,
            Latitude: latitude,
            Longitude: longitude,
            Altitude: altitude,
            Heading: heading,
            Groundspeed: groundspeed
        );

        _flightRadarService.TrackFlight(flight);
        return NoContent();
    }

    [HttpGet]
    [Route("list")]
    public ActionResult<List<Flight>> List(string? instanceId = null, string? worldId = null) {
        if (worldId is { } world) {
            if (instanceId is { } instance) {
                return _flightRadarService.GetAllFlights(instance, world);
            }

            return _flightRadarService.GetAllFlights(world);
        }

        return _flightRadarService.GetAllFlights();
    }
}
