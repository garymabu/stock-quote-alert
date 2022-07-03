using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using stock_quote_alert.Domain;
using stock_quote_alert.Configuration;
using stock_quote_alert.Domain.Models;

namespace stock_quote_alert.Services
{
    class YahooAssetService
    {
        private IYahooAssetService yahooServiceInstance;
        public YahooAssetService()
        {
            yahooServiceInstance = RestService.For<IYahooAssetService>(
                SecretConfiguration.GetInstance().GetSection("yahooAssetEndpoint").Value
            );
        }

        async public Task<YahooQuote> GetAssetData(string assetName)
        {
            var response = await yahooServiceInstance.FetchAssetData(assetName);
            if (response.quoteResponse.error != null)
                throw new Exception(response.quoteResponse.error.description);
            if (response.quoteResponse.result.Count() <= 0)
                throw new Exception(Constants.notFoundExceptionText);
            return response.quoteResponse.result.First();
        }
    }
}
