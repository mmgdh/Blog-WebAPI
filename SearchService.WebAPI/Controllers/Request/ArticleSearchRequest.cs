namespace SearchService.WebAPI.Controllers.Request
{
    public record ArticleSearchRequest(string keyword,int pageIndex,int pageSize);
}
