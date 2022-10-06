using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Domain
{
    public record ArticleSearch: ISearch
    {
        private Guid _id;
        public ArticleSearch(Guid Id)
        {
            _id = Id;
        }
        public string searchType { get => ConstSearchType.ConstSearchArticle; }
        public Guid Id { get => _id; set => _id=value; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
