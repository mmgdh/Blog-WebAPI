using CommonHelpers;
using Microsoft.AspNetCore.Mvc;
using TimeLineService.Domain.Entities;
using TimeLineService.Domain.IRepository;
using TimeLineService.Infrastructure;
using TimeLineService.WebAPI.Controllers.ViewModels.Request;
using TimeLineService.WebAPI.Controllers.ViewModels.Response;

namespace TimeLineService.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TimeLineController : Controller
    {
        private ITimeLineRepository repository;
        private TimeLineDbContext dbContext;
        private RedisHelper redisHelper;

        private string LineKey = "Blog-TimeLine-AllLines";
        public TimeLineController(ITimeLineRepository repository, TimeLineDbContext dbContext, RedisHelper redisHelper)
        {
            this.repository = repository;
            this.dbContext = dbContext;
            this.redisHelper = redisHelper;
            this.redisHelper.DbNum = 15;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRequest addRequest)
        {
            TimeLine TimeLine = TimeLine.Create(addRequest.lineType,addRequest.Description, addRequest.DateTime);
            dbContext.Add(TimeLine);
            var ret = await dbContext.SaveChangesAsync();
            if (ret > 0)
            {
                await redisHelper.ListPushOneAsync(LineKey, new TimeLineResponse(TimeLine.Time, TimeLine.Description,TimeLine.LineType));
            }
            return Ok(ret > 0);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var line = await repository.GetByIdAsync(id);
            dbContext.TimeLines.Remove(line);
            var ret = await dbContext.SaveChangesAsync();
            if (ret > 0)
            {
                await ResetTimeLineRedisCache();
                return Ok(ret);
            }
            return Ok("删除失败，没有相应的变更");
        }

        [HttpPut]
        public async Task<IActionResult> Modify(ModifyRequest request)
        {
            var line = await repository.GetByIdAsync(request.Id);
            line.Description = request.Description;
            line.LineType = request.lineType;
            var ret = await dbContext.SaveChangesAsync();
            if (ret > 0)
            {
                await ResetTimeLineRedisCache();
                return Ok(ret);
            }
            return Ok("更新失败，没有相应的变更");
        }

        [HttpGet]
        public async Task<List<TimeLineResponse>> Get(int page,int pageSize)
        {
            if (page == 0) throw new ArgumentException("页数不能为0");
            page = page - 1;
            List<TimeLineResponse> TimeLines;
            if (redisHelper.KeyExists(LineKey))
            {
                TimeLines = await redisHelper.ListRangeAsync<TimeLineResponse>(LineKey, page, pageSize);
            }
            else
            {
                var lines = await repository.GetAllAsync();
                var respLines = TimeLineResponse.CreateList(lines);
                redisHelper.ReSetRedisValue(LineKey, respLines);
                TimeLines = respLines.Skip(page * pageSize).Take(pageSize).ToList();
            }
            return TimeLines;
        }

        private async Task ResetTimeLineRedisCache()
        {
            var lines = await repository.GetAllAsync();
            var respLines = TimeLineResponse.CreateList(lines);
            redisHelper.ReSetRedisValue(LineKey, respLines);
        }

    }
}
