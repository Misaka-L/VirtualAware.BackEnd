using Microsoft.Extensions.Options;
using VRCFlightRadar.Options;
using VRCFlightRadar.Services;

namespace VRCFlightRadar;

public class VRChatApiHandler : DelegatingHandler {
    private VRChatApiOption _option;

    public VRChatApiHandler(IOptions<VRChatApiOption> option) {
        _option = option.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        request.Headers.Add("Cookie", $"apiKey={_option.ApiKey}; auth={_option.ApiAuth}");

        return await base.SendAsync(request, cancellationToken);
    }
}
