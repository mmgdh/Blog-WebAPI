using CommomInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLineService.Domain.IRepository;

namespace TimeLineService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ITimeLineRepository, TimeLineRepository>();
        }
    }
}
