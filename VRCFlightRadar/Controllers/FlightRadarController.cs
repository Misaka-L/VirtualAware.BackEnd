using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VRCFlightRadar.Models;
using VRCFlightRadar.Services;

namespace VRCFlightRadar.Controllers;
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

        var flight = new Flight(
            Callsign: Callsign,
            TypeCode: TypeCode,
            Registration: Registration,
            InstanceId: InstanceId,
            WorldId: WorldId,
            Latitude: Latitude,
            Longitude: Longitude,
            Altitude: Altitude,
            Heading: Heading,
            Groundspeed: Groundspeed
        );

        _flightRadarService.TrackFlight(flight);
        return NoContent();
    }

    [HttpGet]
    [Route("list")]
    public ActionResult<List<Flight>> List(string? instanceId = null, string? worldId = null) {
        if (worldId is string world) {
            if (instanceId is string instance) {
                return _flightRadarService.GetAllFlights(instance, world);
            }

            return _flightRadarService.GetAllFlights(world);
        }

        return _flightRadarService.GetAllFlights();
    }
}
