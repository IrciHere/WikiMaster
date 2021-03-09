using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WikiMaster.WebsiteScrapper;

namespace WikiMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiController : ControllerBase
    {
        // GET api/<WikiController>/en/url
        [HttpGet("{language}/{articleBodyUrl}")]
        public async Task<string> Get(string language, string articleBodyUrl)
        {
            WikiScrapper ws = new WikiScrapper(language);
            return await ws.ScrapArticle(articleBodyUrl);
        }
    }
}
