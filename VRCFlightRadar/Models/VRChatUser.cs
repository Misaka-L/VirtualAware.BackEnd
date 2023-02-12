using System.Text.Json.Serialization;

namespace VRCFlightRadar.Models;

public class VRChatUser {
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("displayName")] public string DisplayName { get; set; }
    [JsonPropertyName("bio")] public string Bio { get; set; }
    [JsonPropertyName("currentAvatarImageUrl")] public string CurrentAvatarImageUrl { get; set; }
    [JsonPropertyName("currentAvatarThumbnailImageUrl")] public string CurrentAvatarThumbnailImageUrl { get; set; }
    [JsonPropertyName("fallbackAvatar")] public string FallbackAvatar { get; set; }
    [JsonPropertyName("userIcon")] public string UserIcon { get; set; }
    [JsonPropertyName("profilePicOverride")] public string ProfilePicOverride { get; set; }
    [JsonPropertyName("statusDescription")] public string StatusDescription { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("last_platform")] public string LastPlatform { get; set; }
    [JsonPropertyName("tags")] public string[] Tags { get; set; }
    [JsonPropertyName("developerType")] public string DeveloperType { get; set; }
    [JsonPropertyName("isFriend")] public bool IsFrien { get; set; }
}

public class VRChatUserDetail : VRChatUser {
    //[JsonPropertyName("last_activity")] public DateTimeOffset LastActivity { get; set; }
    [JsonPropertyName("worldId")] public string WorldId { get; set; }
    [JsonPropertyName("instanceId")] public string InstanceId { get; set; }
    [JsonPropertyName("location")] public string Location { get; set; }
    [JsonPropertyName("travelingToWorld")] public string TravelingToWorld { get; set; }
    [JsonPropertyName("travelingToInstance")] public string TravelingToInstance { get; set; }
    [JsonPropertyName("travelingToLocation")] public string TravelingToLocation { get; set; }
}