using Refit;
using VRCFlightRadar.Models;

namespace VRCFlightRadar.Services;

[Headers(
    "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.70",
    "Accept: application/json"
    )]
public interface IVRChatApi {
    [Get("/users")]
    public Task<List<VRChatUser>> SearchUsers([AliasAs("search")] string username, [AliasAs("n")] int number = 60, int offset = 0);

    [Get("/users/{userId}")]
    public Task<VRChatUserDetail> GetUser(string userId);
}
