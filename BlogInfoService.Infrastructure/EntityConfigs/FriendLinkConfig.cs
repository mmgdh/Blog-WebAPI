using BlogInfoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogInfoService.Infrastructure.EntityConfigs;

internal class FriendLinkConfig : IEntityTypeConfiguration<FriendLink>
{
    public void Configure(EntityTypeBuilder<FriendLink> builder)
    {
        builder.ToTable("T_FriendLink");

    }
}
