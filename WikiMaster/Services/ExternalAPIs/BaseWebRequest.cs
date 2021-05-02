using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WikiMaster.Services.ExternalAPIs.Wikipedia
{
    class BaseWebRequest
    {
        private string baseAddress;

        public BaseWebRequest(string baseAddress)
        {
            this.baseAddress = baseAddress;
        }


        private async Task<string> BaseRequest(string endpoint, string requestMethod)
        {
            var request = (HttpWebRequest)WebRequest.Create(baseAddress + endpoint);
            request.Method = requestMethod;

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var result = await reader.ReadToEndAsync();
                return result;
            }
        }


        public async Task<string> GetRequest(string endpoint)
        {
            var result = await BaseRequest(endpoint, "GET");
            return result;
        }
    }
}
