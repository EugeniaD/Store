using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StoreRepository.Common
{
    public static class SendRequest
    {
        public static async ValueTask<decimal> GetLocalCurrency(string path)
        {
            WebRequest request = WebRequest.CreateHttp($"https://www.cbr-xml-daily.ru/daily_json.js");
            Stream dataStream;
            WebResponse response = await request.GetResponseAsync();
            string result;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
            }
            response.Close();
            JObject obj = JObject.Parse(result);
            var exchangeRate = (decimal)obj.SelectToken($"$.Valute.{path}.Value");

            return exchangeRate;
        }
    }
}

