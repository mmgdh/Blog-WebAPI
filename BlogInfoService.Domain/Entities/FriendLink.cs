using DomainCommon;

namespace BlogInfoService.Domain.Entities;

public class FriendLink : BaseEntity
{
    public string friendName { get; set; }
    public string friendUrl { get; set; }

    public string headshot { get; set; }

    public string? description { get; set; }

    public static FriendLink Create(string friendName, string friendUrl, string headshot, string description = "")
    {
        FriendLink friendLink = new FriendLink();
        friendLink.Id = new Guid();
        friendLink.friendName = friendName;
        friendLink.friendUrl = friendUrl;
        friendLink.headshot = headshot;
        friendLink.description = description;
        return friendLink;
    }
}
