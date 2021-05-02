using System;
using Xunit;
using WikiMaster.Controllers;
using WikiMaster.Models;
using System.Threading.Tasks;

namespace WikiMaster.Tests
{
    public class GameControllerTests
    {
        GameController _controller;

        public GameControllerTests()
        {
            _controller = new GameController();
        }

        [Fact]
        public void Get_NoArgumentsPassed_ReturnsCorrectSingleGameItem()
        {
            var result = _controller.Get().Result;

            Assert.IsType<SingleGameItem>(result);
            Assert.NotEmpty(result.StartArticle.ArticleTitle);
            Assert.NotEmpty(result.StartArticle.ArticleUrl);
            Assert.NotEmpty(result.TargetArticle.ArticleTitle);
            Assert.NotEmpty(result.TargetArticle.ArticleUrl);
        }

        [Fact]
        public void Get_CorrectArgumentsPassed_ReturnsCorrectSingleGameItem()
        {
            var result = _controller.Get("pl").Result;

            Assert.IsType<SingleGameItem>(result);
            Assert.NotEmpty(result.StartArticle.ArticleTitle);
            Assert.NotEmpty(result.StartArticle.ArticleUrl);
            Assert.NotEmpty(result.TargetArticle.ArticleTitle);
            Assert.NotEmpty(result.TargetArticle.ArticleUrl);


            result = _controller.Get("en").Result;

            Assert.IsType<SingleGameItem>(result);
            Assert.NotEmpty(result.StartArticle.ArticleTitle);
            Assert.NotEmpty(result.StartArticle.ArticleUrl);
            Assert.NotEmpty(result.TargetArticle.ArticleTitle);
            Assert.NotEmpty(result.TargetArticle.ArticleUrl);
        }

        [Fact]
        public async Task Get_IncorrectArgumentsPassed_Throws()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("xd"));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get(""));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("\n"));
            await Assert.ThrowsAsync<ArgumentException>(() => _controller.Get("13"));
        }
    }
}
