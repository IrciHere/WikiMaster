using HtmlAgilityPack;
using System.Linq;
using System.Threading.Tasks;

namespace WikiMaster.WebsiteScrapper
{
    public class WikiScrapper
    {
        private string baseUrl;
        private string language;
        private BaseScrapper scrapper;

        public WikiScrapper(string lang = "en")
        {
            language = lang;
            baseUrl = "https://" + language +".wikipedia.org/";
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
            SetStylesheets(htmlDocument);
            RemoveLanguageModule(htmlDocument);
            RemoveContentOutsideArticle(htmlDocument);
            RemoveContentInsideArticle(htmlDocument);
            SetImagesUrls(htmlDocument);
            SetHrefs(htmlDocument);
            RemoveArticleLeftMargin(htmlDocument);

            string scrappedCode = htmlDocument.DocumentNode.WriteTo();
            return scrappedCode;
        }

        private void SetStylesheets(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "link"
                                                                        && n.Attributes["rel"] != null
                                                                        && n.Attributes["rel"].Value == "stylesheet")
                                                                .ToList();

            foreach (var node in nodes)
            {
                string newHref = "https://en.wikipedia.org/" + node.Attributes["href"].Value;
                node.Attributes["href"].Value = newHref;
            }
        }

        private void RemoveLanguageModule(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "script"
                                                                        && n.Attributes["src"] != null
                                                                        && n.Attributes["src"].Value.StartsWith("/w/load.php?lang"))
                                                                .ToList();

            foreach(var node in nodes)
            {
                node.Remove();
            }
        }

        private void RemoveContentOutsideArticle(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "div")
                                                                .ToList();

            string content = "";
            HtmlNode contentNode = null;
            foreach (var node in nodes)
            {
                if (node.Attributes["id"] != null && node.Attributes["id"].Value == "content")
                {
                    content = node.InnerHtml;
                    contentNode = node;
                }
                else
                {
                    node.Remove();
                }
            }

            if (content != "" && contentNode != null)
            {
                contentNode.InnerHtml = content;
            }

        }

        private void RemoveContentInsideArticle(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().ToList();

            bool afterReferences = false;

            foreach (var node in nodes)
            {
                if (!afterReferences && node.Attributes["id"] != null && node.Attributes["id"].Value == "References")
                {
                    afterReferences = true;
                }

                if (afterReferences && node.Name != "script")
                {
                    node.Remove();
                }
            }
        }

        private void SetImagesUrls(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "img")
                                                                .ToList();

            foreach (var node in nodes)
            {
                if (node.Attributes["src"] != null)
                {
                    node.Attributes["src"].Value = "https:" + node.Attributes["src"].Value;
                }
            }
        }

        private void SetHrefs(HtmlDocument htmlDocument)
        {
            var nodes = htmlDocument.DocumentNode.Descendants().Where(n => n.Name == "a")
                                                                .ToList();

            foreach (var node in nodes)
            {
                if (node.Attributes["href"] != null && node.Attributes["href"].Value[0] == '#')
                {
                    node.Attributes["href"].Value = "about:srcdoc" + node.Attributes["href"].Value;
                }

                
                else if (node.Attributes["href"] != null && node.Attributes["href"].Value.StartsWith("/wiki/"))
                {
                    string newValue = "/api/wiki/" + language + node.Attributes["href"].Value.Replace("wiki/", "wiki%2F");
                    node.Attributes["href"].Value = newValue;
                    node.Attributes.Add("onclick", $"parent.linkClicked(\"{newValue}\")");
                }
            }
        }


        private void RemoveArticleLeftMargin(HtmlDocument htmlDocument)
        {
            var node = htmlDocument.DocumentNode.SelectSingleNode("//head");
            
            HtmlNode newPara = HtmlNode.CreateNode("<style>.mw-body, #mw-head-base, #left-navigation, #mw-data-after-content, .mw-footer {margin-left: 0;}</style>");

            node.ChildNodes.Add(newPara);
        }
    }
}
