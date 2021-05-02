using HtmlAgilityPack;
using System.Threading.Tasks;

namespace WikiMaster.Services.WebsiteScrapper
{
    public class BaseScrapper
    {
        private string baseUrl;
        private HtmlWeb htmlWeb;

        public BaseScrapper(string _baseUrl)
        {
            baseUrl = _baseUrl;
            htmlWeb = new HtmlWeb();
        }

        public async Task<HtmlDocument> ScrapHtml(string bodyUrl = "")
        {
            string fullUrl = baseUrl + bodyUrl;
            HtmlDocument htmlDocument = await htmlWeb.LoadFromWebAsync(fullUrl);
            return htmlDocument;
        }
    }
}
