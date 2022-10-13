using CommomInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateTimeLineService.Domain.IRepository;

namespace UpdateTimeLineService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUpdateTimeLineRepository, UpdateTimeLineRepository>();
        }
    }
}
