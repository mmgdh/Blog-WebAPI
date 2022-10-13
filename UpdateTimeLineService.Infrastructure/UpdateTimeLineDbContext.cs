using CommonInfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateTimeLineService.Domain.Entities;

namespace UpdateTimeLineService.Infrastructure
{
    public class UpdateTimeLineDbContext : BaseDbContext
    {
        public UpdateTimeLineDbContext(DbContextOptions options, IMediator? mediator) : base(options, mediator)
        {
        }

        public DbSet<UpdateTimeLine> updateTimeLines;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
