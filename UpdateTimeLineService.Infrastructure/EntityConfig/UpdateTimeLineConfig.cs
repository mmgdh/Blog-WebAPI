using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpdateTimeLineService.Domain.Entities;

namespace UpdateTimeLineService.Infrastructure.EntityConfig
{
    internal class UpdateTimeLineConfig : IEntityTypeConfiguration<UpdateTimeLine>
    {
        public void Configure(EntityTypeBuilder<UpdateTimeLine> builder)
        {
            builder.ToTable("T_UpdateTimeLine");
            builder.HasKey(e => e.Id).IsClustered(false);//对于Guid主键，不要建聚集索引，否则插入性能很差
            builder.HasIndex(x => x.UpdateDate).IsClustered(true);
        }
    }
}
