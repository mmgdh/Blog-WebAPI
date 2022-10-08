using EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchService.Domain;
using SearchService.WebAPI.Controllers.Request;

namespace SearchService.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchController : Controller
    {
        private readonly ISearchRepository repository;
        private readonly IEventBus eventBus;

        public SearchController(ISearchRepository repository, IEventBus eventBus)
        {
            this.repository = repository;
            this.eventBus = eventBus;
        }
        [HttpGet]
        public Task<SearchResponse> SearchArticle([FromQuery]ArticleSearchRequest request)
        {
            return repository.SearchAsync(ConstSearchType.ConstSearchArticle, request.keyword,request.pageIndex,request.pageSize);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ReIndexAll()
        {
            //避免耦合，这里发送ReIndexAll的集成事件
            //所有向搜索系统贡献数据的系统都可以响应这个事件，重新贡献数据
            eventBus.publish(ConstEventName.Search_ReSetAllIndex, null);
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public ActionResult<bool> Delete(Guid Id)
        {
            var request = new ArticleSearch(Id);
            repository.DeleteAsync(request);
            return true;
        }
    }
}
