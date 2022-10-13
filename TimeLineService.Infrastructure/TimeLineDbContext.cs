using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeLineService.Domain.Entities;

namespace TimeLineService.Infrastructure
{
    public class TimeLineDbContext : BaseDbContext
    {
        public TimeLineDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
        }

        public DbSet<TimeLine> TimeLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
