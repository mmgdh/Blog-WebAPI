namespace SearchService.Domain
{
    public interface ISearchRepository
    {
        public Task UpsertAsync(ArticleSearch search);
        public Task DeleteAsync(ArticleSearch search);

        public Task<SearchResponse> SearchAsync(string searchType, string Keyword, int PageIndex, int PageSize);
    }
}