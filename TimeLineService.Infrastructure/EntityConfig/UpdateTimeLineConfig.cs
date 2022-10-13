using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeLineService.Domain.Entities;

namespace TimeLineService.Infrastructure.EntityConfig
{
    internal class TimeLineConfig : IEntityTypeConfiguration<TimeLine>
    {
        public void Configure(EntityTypeBuilder<TimeLine> builder)
        {
            builder.ToTable("T_TimeLine");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasIndex(x => x.Time).IsClustered(true);
        }
    }
}
 