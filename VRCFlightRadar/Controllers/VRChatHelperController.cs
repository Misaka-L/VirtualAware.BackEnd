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

    [HttpGet]
    [Route("{username}/activity")]
    public async ValueTask<ActionResult<VRChatActivity>> GetActivityByUsername(string username) {
        try {
            var detail = await getUserDetailAsync(username);
            if (getActivity(detail) is VRChatActivity activity) {
                return activity;
            }
        } catch { }

        return NoContent();
    }

    [HttpGet]
    [Route("{username}/activity/instance")]
    public async ValueTask<ActionResult<string>> GetInstanceActivityByUsername(string username) {
        try {
            var detail = await getUserDetailAsync(username);
            if (getActivity(detail) is VRChatActivity activity) {
                return activity.InstanceId;
            }
        } catch { }

        return NoContent();
    }

    [HttpGet]
    [Route("{username}/activity/world")]
    public async ValueTask<ActionResult<string>> GetWorldActivityByUsername(string username) {
        try {
            var detail = await getUserDetailAsync(username);
            if (getActivity(detail) is VRChatActivity activity) {
                return activity.WorldId;
            }
        } catch { }

        return NoContent();
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

    private async ValueTask<VRChatUserDetail> getUserDetailAsync(string username) {
        List<VRChatUser> users;
        try {
            users = await _vrchatApi.SearchUsers(username);
        } catch (Exception ex) {
            _logger.LogError(ex, "Exception occured when search user.");
            throw new Exception("UnableSerachUser", ex);
        }

        if (users.Find(user => user.DisplayName == username) is VRChatUser user) {
            try {
                var userDetail = await _vrchatApi.GetUser(user.Id);
                return userDetail;
            } catch (Exception ex) {
                _logger.LogError(ex, "Exception occured when get user detail.");
                throw new Exception("UnableGetUserDetail", ex);
            }
        }

        throw new Exception("UserNotFound");
    }
}
