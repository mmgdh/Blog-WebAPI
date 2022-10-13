using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateTimeLineService.Domain.Entities;

namespace UpdateTimeLineService.Domain.IRepository
{
    public interface IUpdateTimeLineRepository
    {
        public Task<List<UpdateTimeLine>> GetByPageSizeAsync(int Page, int PageSize);
        public Task<List<UpdateTimeLine>> GetAllAsync();

        public Task<UpdateTimeLine> GetByIdAsync(Guid id);
    }
}
