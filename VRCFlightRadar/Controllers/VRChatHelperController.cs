using Microsoft.AspNetCore.Mvc;
using VRCFlightRadar.Models;
using VRCFlightRadar.Services;

namespace VRCFlightRadar.Controllers;

[ApiController]
[Route("vrchat")]
public class VRChatHelperController : ControllerBase {
    private readonly IVRChatApi _vrchatApi;

    public VRChatHelperController(IVRChatApi vrchatApi) {
        _vrchatApi = vrchatApi;
    }

    [HttpGet]
    [Route("{username}/activity")]
    public async ValueTask<ActionResult<VRChatActivity>> GetActivityByUsername(string username) {
        var users = await _vrchatApi.SearchUsers(username);

        if (users.Find(user => user.DisplayName == username) is VRChatUser user) {
            var userDetail = await _vrchatApi.GetUser(user.Id);
            if (getActivity(userDetail) is VRChatActivity activity)
                return activity;

            return NoContent();
        }

        return NotFound();
    }

    [HttpGet]
    [Route("{username}/activity/instance")]
    public async ValueTask<ActionResult<string>> GetInstanceActivityByUsername(string username) {
        var users = await _vrchatApi.SearchUsers(username);

        if (users.Find(user => user.DisplayName == username) is VRChatUser user) {
            var userDetail = await _vrchatApi.GetUser(user.Id);
            if (getActivity(userDetail) is VRChatActivity activity)
                return activity.InstanceId;

            return NoContent();
        }

        return NotFound();
    }

    [HttpGet]
    [Route("{username}/activity/world")]
    public async ValueTask<ActionResult<string>> GetWorldActivityByUsername(string username) {
        var users = await _vrchatApi.SearchUsers(username);

        if (users.Find(user => user.DisplayName == username) is VRChatUser user) {
            var userDetail = await _vrchatApi.GetUser(user.Id);
            if (getActivity(userDetail) is VRChatActivity activity)
                return activity.WorldId;

            return NoContent();
        }

        return NotFound();
    }

    private VRChatActivity? getActivity(VRChatUserDetail userDetail) {
        try {
            var args = userDetail.InstanceId.Split('~');
            var region = args[1].Replace("region(", "").Replace(")", "");

            return new VRChatActivity {
                InstanceId = args[0],
                WorldId = userDetail.WorldId,
                Region = region
            };
        } catch {
            return null;
        }
    }
}
