using Microsoft.EntityFrameworkCore;
using UpdateTimeLineService.Domain.Entities;
using UpdateTimeLineService.Domain.IRepository;

namespace UpdateTimeLineService.Infrastructure
{
    public class UpdateTimeLineRepository : IUpdateTimeLineRepository
    {
        UpdateTimeLineDbContext dbContext;

        public UpdateTimeLineRepository(UpdateTimeLineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UpdateTimeLine>> GetAllAsync()
        {
            return await dbContext.updateTimeLines.ToListAsync();
        }

        public async Task<UpdateTimeLine> GetByIdAsync(Guid id)
        {
            var line = await dbContext.updateTimeLines.FindAsync(id);
            if (line == null) throw new Exception("获取时间线失败");
            return line;
        }

        public async Task<List<UpdateTimeLine>> GetByPageSizeAsync(int Page, int PageSize)
        {
            return await dbContext.updateTimeLines.Skip(Page).Take(PageSize).ToListAsync();
        }
    }
}
