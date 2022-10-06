using ArticleService.Infrastructure;
using EventBus;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.WebAPI.EventHandlers
{
    [EventName(ConstEventName.Search_ReSetAllIndex)]
    public class ReSetIndexArticleEventHandler : IIntegrationEventHandler
    {
        private readonly ArticleDbContext dbContext;
        private readonly IEventBus eventBus;

        public ReSetIndexArticleEventHandler(ArticleDbContext dbContext, IEventBus eventBus)
        {
            this.dbContext = dbContext;
            this.eventBus = eventBus;
        }

        public Task Handle(string EventName, string eventData)
        {
            foreach (var article in dbContext.Articles.Include(x=>x.articleContent).AsNoTracking())
            {
                EventBusParameter.ArticleSearch_Parameter search_Parameter = new EventBusParameter.ArticleSearch_Parameter(article.Id,article.Title,article.articleContent.Content);
                eventBus.publish(ConstEventName.Search_Article, search_Parameter);
            }
            return Task.CompletedTask;
        }
    }
}
