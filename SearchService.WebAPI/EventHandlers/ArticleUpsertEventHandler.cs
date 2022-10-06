using EventBus;
using SearchService.Domain;

namespace SearchService.WebAPI.EventHandlers
{
    [EventName(ConstEventName.Search_Article)]
    public class ArticleUpsertEventHandler : JsonIntegrationEventHandler<EventBusParameter.ArticleSearch_Parameter>
    {
        private readonly ISearchRepository repository;

        public ArticleUpsertEventHandler(ISearchRepository repository)
        {
            this.repository = repository;
        }

        public override Task HandleJson(string eventName, EventBusParameter.ArticleSearch_Parameter? eventData)
        {
            if(eventData == null)throw new ArgumentNullException(nameof(eventData));
            ArticleSearch articleSearch = new ArticleSearch(eventData.id);
            articleSearch.Content = eventData.content;
            articleSearch.Title = eventData.Title;
            return repository.UpsertAsync(articleSearch); 
        }
    }
}
