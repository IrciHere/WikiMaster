using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WikiMaster.Controllers;
using Xunit;

namespace WikiMaster.Tests
{
    public class WikiControllerTests
    {
        WikiController _controller;

        public WikiControllerTests()
        {
            _controller = new WikiController();
        }

        [Fact]
        public void Get_CorrectArgumentsPassed_ReturnsScrappedArticle()
        {
            List<string> results = new List<string>();
            results.Add(_controller.Get("pl", "wiki/Polska").Result);
            results.Add(_controller.Get("pl", "wiki/Velika_Ivanča").Result);
            results.Add(_controller.Get("pl", "wiki/Ramat_ha-Chajjal").Result);
            results.Add(_controller.Get("pl", "wiki/Gąsin").Result);
            results.Add(_controller.Get("pl", "wiki/Jun’ya_Ōsaki").Result);

            results.Add(_controller.Get("en", "wiki/Qasemabad,_Sepidan").Result);
            results.Add(_controller.Get("en", "wiki/Socket_AM2").Result);
            results.Add(_controller.Get("en", "wiki/Łagów,_Świebodzin_County").Result);
            results.Add(_controller.Get("en", "wiki/Rolls-Royce_Olympus").Result);
            results.Add(_controller.Get("en", "wiki/Church_of_Nativité-de-la-Sainte-Vierge-d%27Hochelaga").Result);
            results.Add(_controller.Get("en", "wiki/UTC-04:00").Result);
            results.Add(_controller.Get("en", "wiki/UTC+04:00").Result);

            foreach (var result in results)
            {
                Assert.NotEmpty(result);
            }
        }

        [Fact]
        public async Task Get_WrongLanguagePassed_Throws()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("xd", "wiki/Velika_Ivanča"));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("\n", "wiki/UTC+04:00"));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("", ""));
        }

        [Fact]
        public async Task Get_CorrectLanguagePassed_NoArticlePassed_Throws()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("en", ""));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("pl", ""));
        }
    }
}
