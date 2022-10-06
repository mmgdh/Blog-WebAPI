using Nest;
using SearchService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IElasticClient elasticClient;

        public SearchRepository(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public Task DeleteAsync(ArticleSearch search)
        {

            //elasticClient.DeleteByQuery<ISearch>(q => q
            //   .Index(search.searchType)
            //   .Query(rq => rq.Term(f => f.Id, "elasticsearch.pm")));
            //因为有可能文档不存在，所以不检查结果
            //如果Episode被删除，则把对应的数据也从Elastic Search中删除
            return elasticClient.DeleteAsync(new DeleteRequest(search.searchType, search.Id));
        }

        public async Task UpsertAsync(ArticleSearch search)
        {
            switch (search.searchType)
            {
                case ConstSearchType.ConstSearchArticle:
                    search=search as ArticleSearch;
                    break;
            }

            var response = await elasticClient.IndexAsync(search, idx => idx.Index(search.searchType.ToLower()).Id(search.Id));//Upsert:Update or Insert
            if (!response.IsValid)
            {
                throw new ApplicationException(response.DebugInformation);
            }
        }

        public async Task<SearchResponse> SearchAsync(string searchType, string Keyword, int PageIndex, int PageSize)
        {
            int from = PageSize * (PageIndex - 1);
            string kw = Keyword;
            switch (searchType)
            {
                case ConstSearchType.ConstSearchArticle: 
                    {
                        return await ArtilceSearch(from,kw,  PageSize);
                    }
            }
            throw new ApplicationException("未实现对应的ES查询");
        }

        private async Task<SearchResponse> ArtilceSearch(int from, string kw,  int PageSize)
        {
            Func<QueryContainerDescriptor<ArticleSearch>, QueryContainer> query = (q) =>
              q.Match(mq => mq.Field(f => f.Title).Query(kw))
              || q.Match(mq => mq.Field(f => f.Content).Query(kw));
            Func<HighlightDescriptor<ArticleSearch>, IHighlight> highlightSelector = h => h
                .Fields(fs => fs.Field(f => f.Content));
            var result = await this.elasticClient.SearchAsync<ArticleSearch>(s => s.Index(ConstSearchType.ConstSearchArticle).From(from)
                .Size(PageSize).Query(query).Highlight(highlightSelector));
            if (!result.IsValid)
            {
                throw result.OriginalException;
            }
            List<ArticleSearch> articles = new List<ArticleSearch>();
            foreach (var hit in result.Hits)
            {
                string highlightedSubtitle;
                //如果没有预览内容，则显示前50个字
                if (hit.Highlight.ContainsKey("content"))
                {
                    highlightedSubtitle = string.Join("\r\n", hit.Highlight["content"]);
                }
                else
                {
                    highlightedSubtitle = "";
                }
                var article = hit.Source with { Content = highlightedSubtitle };
                articles.Add(article);
            }
            return new SearchResponse(articles, result.Total);
        }
    }
}
