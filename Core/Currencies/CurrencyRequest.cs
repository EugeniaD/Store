using System;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Store.Core.Enums;

namespace Store.Core.Currencies
{
    public class CurrencyRequest
    {
        public void GetLocalCurrency(string path)
        {
            List<Currency> currencies = new List<Currency>();

            var client = new RestClient($"https://www.cbr-xml-daily.ru");
            var request = new RestRequest("daily_json.js", Method.GET);
            var response = client.Execute(request);
            JObject obj = JObject.Parse(response.Content);

            foreach (var item in Enum.GetValues(typeof(CurrencyEnum)))
            {
                decimal rate;
                DateTime date = new DateTime();
                if (obj.SelectToken($"$.Valute.{path}.Value") != null)
                {
                    rate = (decimal)obj.SelectToken($"$.Valute.{path}.Value");
                    date = (DateTime)obj.SelectToken($"$.Timestamp");

                }
                else
                {
                    rate = 1;
                }

                Currency currency = new Currency()
                {
                    Code = item.ToString(),
                    Rate = rate,
                    Date = date.Date
                };
                currencies.Add(currency);
            }
            CurrencyRates.ActualCurrencyRates = currencies;
        }
    }
}

