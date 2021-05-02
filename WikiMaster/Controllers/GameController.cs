using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WikiMaster.Models;
using System.Linq;
using System;
using WikiMaster.Services.GameHelpers;

namespace WikiMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private WikipediaHelper wikipediaHelper;
        public GameController()
        {
             wikipediaHelper = new WikipediaHelper();
        }

        // GET: api/<GameController>
        [HttpGet]
        public async Task<SingleGameItem> Get()
        {
            SingleGameItem item = await wikipediaHelper.CreateSingleGameItem();
            return item;
        }

        // GET api/<GameController>/en
        [HttpGet("{language}")]
        public async Task<SingleGameItem> Get(string language)
        {
            if (!wikipediaHelper.IsSupportedLanguage(language))
            {
                throw new ArgumentException($"Unsupported language: {language}");
            }

            SingleGameItem item = await wikipediaHelper.CreateSingleGameItem(language);
            return item;
        }
    }
}
