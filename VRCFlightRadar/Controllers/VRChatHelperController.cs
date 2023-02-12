using Microsoft.AspNetCore.Mvc;
using VRCFlightRadar.Models;
using VRCFlightRadar.Services;

namespace VRCFlightRadar.Controllers;

[ApiController]
[Route("vrchat")]
public class VRChatHelperController : ControllerBase {
    private readonly IVRChatApi _vrchatApi;
    private readonly ILogger<VRChatHelperController> _logger;

    public VRChatHelperController(IVRChatApi vrchatApi, ILogger<VRChatHelperController> logger) {
        _vrchatApi = vrchatApi;
        _logger = logger;
    }
}
