using Microsoft.EntityFrameworkCore;
using TimeLineService.Domain.Entities;
using TimeLineService.Domain.IRepository;

namespace TimeLineService.Infrastructure
{
    public class TimeLineRepository : ITimeLineRepository
    {
        TimeLineDbContext dbContext;

        public TimeLineRepository(TimeLineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<TimeLine>> GetAllAsync()
        {
            return await dbContext.TimeLines.AsNoTracking().ToListAsync();
        }

        public async Task<TimeLine> GetByIdAsync(Guid id)
        {
            var line = await dbContext.TimeLines.FindAsync(id);
            if (line == null) throw new Exception("获取时间线失败");
            return line;
        }

        public async Task<List<TimeLine>> GetByPageSizeAsync(int Page, int PageSize)
        {
            return await dbContext.TimeLines.Skip(Page).Take(PageSize).AsNoTracking().ToListAsync();
        }
    }
}
