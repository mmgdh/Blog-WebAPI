using BlogInfoService.Domain.Entities;
using FluentValidation;

namespace BlogInfoService.WebAPI.ViewModels.RequestModel
{
    public class FriendLinkRequest : FriendLink
    {
    }

    public class FriendLinkRequestValidator : AbstractValidator<FriendLinkRequest>
    {
        public FriendLinkRequestValidator()
        {
            RuleFor(x => x.friendName).NotNull().NotEmpty();
            RuleFor(x => x.friendUrl).NotNull().NotEmpty();
            RuleFor(x => x.headshot).NotNull().NotEmpty();
        }
    }
}
