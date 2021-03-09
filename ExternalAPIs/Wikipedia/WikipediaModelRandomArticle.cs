using System;

namespace WikiMaster.ExternalAPIs.Wikipedia
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Namespace
    {
        public int id { get; set; }
        public string text { get; set; }
    }

    public class Titles
    {
        public string canonical { get; set; }
        public string normalized { get; set; }
        public string display { get; set; }
    }

    public class Thumbnail
    {
        public string source { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Originalimage
    {
        public string source { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Coordinates
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Desktop
    {
        public string page { get; set; }
        public string revisions { get; set; }
        public string edit { get; set; }
        public string talk { get; set; }
    }

    public class Mobile
    {
        public string page { get; set; }
        public string revisions { get; set; }
        public string edit { get; set; }
        public string talk { get; set; }
    }

    public class ContentUrls
    {
        public Desktop desktop { get; set; }
        public Mobile mobile { get; set; }
    }

    public class RootWikipediaRandomArticle
    {
        public string type { get; set; }
        public string title { get; set; }
        public string displaytitle { get; set; }
        public Namespace @namespace { get; set; }
        public string wikibase_item { get; set; }
        public Titles titles { get; set; }
        public int pageid { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Originalimage originalimage { get; set; }
        public string lang { get; set; }
        public string dir { get; set; }
        public string revision { get; set; }
        public string tid { get; set; }
        public DateTime timestamp { get; set; }
        public string description { get; set; }
        public string description_source { get; set; }
        public Coordinates coordinates { get; set; }
        public ContentUrls content_urls { get; set; }
        public string extract { get; set; }
        public string extract_html { get; set; }
    }


}
