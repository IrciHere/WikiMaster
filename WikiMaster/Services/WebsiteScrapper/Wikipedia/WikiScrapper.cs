using HtmlAgilityPack;
using System.Threading.Tasks;

namespace WikiMaster.Services.WebsiteScrapper.Wikipedia
{
    public class WikiScrapper
    {
        private string baseUrl;
        private string language;
        private BaseScrapper scrapper;

        public WikiScrapper(string lang = "en")
        {
            language = lang;
            baseUrl = "https://" + language + ".wikipedia.org/";
            scrapper = new BaseScrapper(baseUrl);
        }

        public async Task<string> ScrapArticle(string bodyUrl)
        {
            HtmlDocument doc = await GetArticleHtml(bodyUrl);

            string scrappedCode = CreateScrappedHtml(doc);

            return scrappedCode;
        }

        private async Task<HtmlDocument> GetArticleHtml(string bodyUrl)
        {
            return await scrapper.ScrapHtml(bodyUrl);
        }

        private string CreateScrappedHtml(HtmlDocument htmlDocument)
        {
            WikiArticleAdjuster wikiArticleAdjuster = new WikiArticleAdjuster(htmlDocument, language);
            htmlDocument = wikiArticleAdjuster.AdjustArticle();

            string scrappedCode = htmlDocument.DocumentNode.WriteTo();
            return scrappedCode;
        }
    }
}
