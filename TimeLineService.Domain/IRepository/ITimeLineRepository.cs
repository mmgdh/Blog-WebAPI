using TimeLineService.Domain.Entities;

namespace TimeLineService.Domain.IRepository
{
    public interface ITimeLineRepository
    {
        public Task<List<TimeLine>> GetByPageSizeAsync(int Page, int PageSize);
        public Task<List<TimeLine>> GetAllAsync();

        public Task<TimeLine> GetByIdAsync(Guid id);
    }
}
