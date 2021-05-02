namespace WikiMaster.Models
{
    public class SingleGameItem
    {
        public ArticleItem StartArticle { get; set; }
        public ArticleItem TargetArticle { get; set; }


        public SingleGameItem(RootWikipediaRandomArticle startArticle, RootWikipediaRandomArticle targetArticle)
        {
            StartArticle = new ArticleItem(startArticle);
            TargetArticle = new ArticleItem(targetArticle);
        }     
    }
}
