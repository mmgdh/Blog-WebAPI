using CommonInitializer;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLineService.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TimeLineDbContext>
    {
        public TimeLineDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<TimeLineDbContext>();
            return new TimeLineDbContext(optionsBuilder.Options, null);
        }
    }
}
