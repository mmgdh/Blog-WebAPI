using CommonHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpdateTimeLineService.Domain.Entities;
using UpdateTimeLineService.Domain.IRepository;
using UpdateTimeLineService.Infrastructure;
using UpdateTimeLineService.WebAPI.Controllers.ViewModels.Request;

namespace UpdateTimeLineService.WebAPI.Controllers
{
    public class UpdateTimeLineController : Controller
    {
        private IUpdateTimeLineRepository repository;
        private UpdateTimeLineDbContext dbContext;
        private RedisHelper redisHelper;
        public UpdateTimeLineController(IUpdateTimeLineRepository repository, UpdateTimeLineDbContext dbContext, RedisHelper redisHelper)
        {
            this.repository = repository;
            this.dbContext = dbContext;
            this.redisHelper = redisHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRequest addRequest)
        {
            UpdateTimeLine updateTimeLine = UpdateTimeLine.Create(addRequest.Description, addRequest.DateTime);
            dbContext.Add(updateTimeLine);
            var ret = await dbContext.SaveChangesAsync();
            return Ok(ret > 0);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var line = await repository.GetByIdAsync(id);
            dbContext.updateTimeLines.Remove(line);
            var ret = await dbContext.SaveChangesAsync();
            return Ok(ret > 0);
        }

        [HttpPut]
        public async Task<IActionResult> Modify(ModifyRequest request)
        {
            var line = await repository.GetByIdAsync(request.Id);
            dbContext.updateTimeLines.Remove(line);
            var ret = await dbContext.SaveChangesAsync();
            return Ok(ret > 0);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }


    }
}
