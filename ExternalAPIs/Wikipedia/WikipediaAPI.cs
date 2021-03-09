using System.Threading.Tasks;

namespace WikiMaster.ExternalAPIs.Wikipedia
{
    public class WikipediaAPI
    {
        private BaseWebRequest baseWebRequest;

        public WikipediaAPI(string language)
        {
            string url = "https://" + language + ".wikipedia.org/api/rest_v1/";
            baseWebRequest = new BaseWebRequest(url);
        }

        
        public async Task<string> RandomArticleSummaryRequest()
        {
            string responseData;

            string bodyAddress = "page/random/summary";
            responseData = await baseWebRequest.GetRequest(bodyAddress);
            return responseData;
        } 
    }
}
