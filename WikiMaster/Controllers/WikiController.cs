using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WikiMaster.Services.GameHelpers;
using WikiMaster.Services.WebsiteScrapper.Wikipedia;

namespace WikiMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        private WikipediaHelper wikipediaHelper;
        public WikiController()
        {
            wikipediaHelper = new WikipediaHelper();
        }

        // GET api/<WikiController>/en/url
        [HttpGet("{language}/{articleBodyUrl}")]
        public async Task<string> Get(string language, string articleBodyUrl)
        {
            if (!wikipediaHelper.IsSupportedLanguage(language))
            {
                throw new ArgumentException($"Unsupported language: {language}");
            }

            if (string.IsNullOrEmpty(articleBodyUrl))
            {
                throw new ArgumentException("No article passed");
            }
               
            WikiScrapper ws = new WikiScrapper(language);
            return await ws.ScrapArticle(articleBodyUrl);
        }
    }
}
