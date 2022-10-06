using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class ConstEventName
    {
        public const string Stream_FileUpload = "FileUpload";
        public const string Article_FileCallBackUpdated = "ArticleFileCallBackUpdated";
        public const string Search_Article = "ArticleSearch";
        public const string Search_ReSetAllIndex = "ReSetAllIndex";
    }

    public enum EnumCallBackEntity
    {
        ArticleClassify,
        ArticleImage,
        ArticleTitleImage
    }
    
}
