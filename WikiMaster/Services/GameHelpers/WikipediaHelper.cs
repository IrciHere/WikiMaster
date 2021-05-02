using System.Linq;
using System.Threading.Tasks;
using WikiMaster.Models;
using WikiMaster.Services.ExternalAPIs.Wikipedia;

namespace WikiMaster.Services.GameHelpers
{
    public class WikipediaHelper
    {
        private string[] supportedLanguages = { "en", "pl" };
        public WikipediaHelper() { }

        public async Task<SingleGameItem> CreateSingleGameItem(string language = "en")
        {
            WikipediaData wikipediaData = new WikipediaData(language);
            RootWikipediaRandomArticle startArticle = await wikipediaData.GetRandomArticle();
            RootWikipediaRandomArticle targetArticle = await wikipediaData.GetRandomArticle();
            SingleGameItem item = new SingleGameItem(startArticle, targetArticle);
            return item;
        }

        public bool IsSupportedLanguage(string language)
        {
            return supportedLanguages.Contains(language);
        }
    }
}
