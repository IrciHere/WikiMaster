using WikiMaster.ExternalAPIs.Wikipedia;

namespace WikiMaster.Models
{
    public class ArticleItem
    {
        public string ArticleTitle { get; set; }
        public string ArticleUrl { get; set; }
        public string ArticleThumbnailUrl { get; set; }
        public string ArticleSummary { get; set; }

        public ArticleItem(RootWikipediaRandomArticle article)
        {
            ArticleTitle = article.title;
            ArticleUrl = CutUrlBody(article.content_urls.desktop.page);
            ArticleThumbnailUrl = article.thumbnail.source;
            ArticleSummary = article.description;
        }

        private string CutUrlBody(string fullUrl)
        {
            return fullUrl.Substring(fullUrl.LastIndexOf("/wiki"));
        }
    }
}
