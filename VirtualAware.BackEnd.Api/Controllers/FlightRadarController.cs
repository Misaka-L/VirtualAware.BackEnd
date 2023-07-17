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

    [HttpPost]
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
    public ActionResult<IReadOnlyList<Flight>> List(string? instanceId = null, string? worldId = null) {
        if (worldId is { } world) {
            if (instanceId is { } instance) {
                return Ok(_flightRadarService.GetAllFlights(instance, world));
            }

            return Ok(_flightRadarService.GetAllFlights(world));
        }

        return Ok(_flightRadarService.GetAllFlights());
    }

    [HttpGet]
    [Route("worlds")]
    public ActionResult<IReadOnlyList<string>> ListWorlds()
    {
        return Ok(_flightRadarService.GetAllWorlds());
    }

    [HttpGet]
    [Route("instances")]
    public ActionResult<List<string>> ListInstance(string worldId)
    {
        return Ok(_flightRadarService.GetAllInstances(worldId));
    }
}