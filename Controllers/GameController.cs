using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WikiMaster.ExternalAPIs.Wikipedia;
using WikiMaster.Models;

namespace WikiMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET: api/<GameController>
        [HttpGet]
        public async Task<SingleGameItem> Get()
        {
            WikipediaData wikipediaData = new WikipediaData();
            RootWikipediaRandomArticle startArticle = await wikipediaData.GetRandomArticle();
            RootWikipediaRandomArticle targetArticle = await wikipediaData.GetRandomArticle();
            SingleGameItem item = new SingleGameItem(startArticle, targetArticle);
            return item;
        }

        // GET api/<GameController>/en
        [HttpGet("{language}")]
        public async Task<SingleGameItem> Get(string language)
        {
            WikipediaData wikipediaData = new WikipediaData(language);
            RootWikipediaRandomArticle startArticle = await wikipediaData.GetRandomArticle();
            RootWikipediaRandomArticle targetArticle = await wikipediaData.GetRandomArticle();
            SingleGameItem item = new SingleGameItem(startArticle, targetArticle);
            return item;
        }
    }
}
