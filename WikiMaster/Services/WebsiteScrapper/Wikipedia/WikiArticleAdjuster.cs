using HtmlAgilityPack;
using System.Linq;

namespace WikiMaster.Services.WebsiteScrapper.Wikipedia
{
    public class WikiArticleAdjuster
    {
        private HtmlDocument doc;
        private string language;

        public WikiArticleAdjuster(HtmlDocument _doc, string _language)
        {
            doc = _doc;
            language = _language;
        }

        public HtmlDocument AdjustArticle()
        {
            RemoveUnnecessaryContent();
            SetNecessaryContent();

            return doc;
        }


        private void RemoveUnnecessaryContent()
        {
            RemoveLanguageModule();
            RemoveArticleLeftMargin();
            RemoveContentOutsideArticle();
            RemoveContentInsideArticle();
        }


        private void SetNecessaryContent()
        {
            SetStylesheets();
            SetImagesUrls();
            SetHrefs();
        }


        #region Remove-Content Methods
        private void RemoveLanguageModule()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .Where(n => n.Name == "script"
                                                          && n.Attributes["src"] != null
                                                          && n.Attributes["src"].Value.StartsWith("/w/load.php?lang"))
                                                 .ToList();

            foreach (var node in nodes)
            {
                node.Remove();
            }
        }


        private void RemoveContentOutsideArticle()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .Where(n => n.Name == "div")
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


        private void RemoveContentInsideArticle()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .ToList();

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


        private void RemoveArticleLeftMargin()
        {
            var node = doc.DocumentNode.SelectSingleNode("//head");

            HtmlNode newParameter = HtmlNode.CreateNode("<style>.mw-body, #mw-head-base, #left-navigation, #mw-data-after-content, .mw-footer {margin-left: 0;}</style>");

            node.ChildNodes.Add(newParameter);
        }
        #endregion

        #region Set-Content Methods
        private void SetStylesheets()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .Where(n => n.Name == "link"
                                                          && n.Attributes["rel"] != null
                                                          && n.Attributes["rel"].Value == "stylesheet")
                                                 .ToList();

            foreach (var node in nodes)
            {
                string newHref = "https://en.wikipedia.org/" + node.Attributes["href"].Value;
                node.Attributes["href"].Value = newHref;
            }
        }


        private void SetImagesUrls()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .Where(n => n.Name == "img")
                                                 .ToList();

            foreach (var node in nodes)
            {
                if (node.Attributes["src"] != null)
                {
                    node.Attributes["src"].Value = "https:" + node.Attributes["src"].Value;
                }
            }
        }


        private void SetHrefs()
        {
            var nodes = doc.DocumentNode.Descendants()
                                                 .Where(n => n.Name == "a")
                                                 .ToList();

            foreach (var node in nodes)
            {
                if(node.Attributes["href"] == null)
                {
                    continue;
                }

                if (node.Attributes["href"].Value.StartsWith("/wiki/"))
                {
                    string newValue = "/api/wiki/" + language + node.Attributes["href"].Value.Replace("wiki/", "wiki%2F");
                    node.Attributes["href"].Value = newValue;
                    node.Attributes.Add("onclick", $"parent.linkClicked(\"{newValue}\")");
                }

                else
                {
                    if (node.Attributes["href"].Value[0] !='#' || node.Attributes["href"].Value.StartsWith("#cite_note"))
                    {
                        node.Attributes["href"].Value = "#firstHeading";
                    }
                    node.Attributes["href"].Value = "about:srcdoc" + node.Attributes["href"].Value;
                }
            }
        }
        #endregion
    }
}