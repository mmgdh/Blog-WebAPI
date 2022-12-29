using BlogInfoService.Domain.Entities;
using BlogInfoService.Infrastructure;
using BlogInfoService.WebAPI.ViewModels.RequestModel;
using CommonHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogInfoService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FriendLinkController : Controller
    {
        BlogParamDbContext dbContext;
        private readonly RedisHelper redisHelper;

        private const string Key_lstFriendLink = "FriendLinksKey";
        public FriendLinkController(RedisHelper redisHelper, BlogParamDbContext dbContext)
        {
            this.redisHelper = redisHelper;
            redisHelper.DbNum = 3;
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<FriendLink>> GetList()
        {
            List<FriendLink> friendLinks = new List<FriendLink>();
            if (redisHelper.KeyExists(Key_lstFriendLink))
            {
                friendLinks = await redisHelper.ListRangeAsync<FriendLink>(Key_lstFriendLink);
            }
            else
            {
                friendLinks = await dbContext.friendLinks.ToListAsync();
                await redisHelper.AddListAsync(Key_lstFriendLink, friendLinks);

                await redisHelper.KeyExpireAsync(Key_lstFriendLink, TimeSpan.FromHours(12));
            }
            return friendLinks;
        }

        [HttpPost]
        [Authorize]
        public async Task<bool> Add(FriendLinkRequest request)
        {
            FriendLink friendLink = FriendLink.Create(request.friendName, request.friendUrl, request.headshot, request.description);
            dbContext.Add(friendLink);
            var ret = await dbContext.SaveChangesAsync();
            await redisHelper.ListPushOneAsync(Key_lstFriendLink, friendLink);
            return ret > 0;
        }
        [HttpDelete]
        [Authorize]
        public async Task<bool> Delete(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var link = await dbContext.friendLinks.FindAsync(id);
            if (link == null) throw new Exception("Guid号找不到对应实体");

            dbContext.friendLinks.Remove(link);
            var ret = await dbContext.SaveChangesAsync();
            redisHelper.KeyDelete(Key_lstFriendLink);
            return ret > 0;
        }
        [Authorize]
        [HttpPut]
        public async Task<bool> Modify(FriendLinkRequest request)
        {
            var link = await dbContext.friendLinks.FindAsync(request.Id);
            if (link == null) throw new Exception("Guid号找不到对应实体");

            link.description = request.description;
            link.headshot = request.headshot;
            link.friendUrl = request.friendUrl;
            link.friendName = request.friendName;
            var ret = await dbContext.SaveChangesAsync();
            redisHelper.KeyDelete(Key_lstFriendLink);
            return ret > 0;
        }



    }
}
