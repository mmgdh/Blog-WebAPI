using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Domain
{
    public interface ISearch
    {
        public string searchType { get;}
        public  Guid Id { get; set; }
    }
}
