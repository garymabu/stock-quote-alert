using stock_quote_alert.Configuration;
using stock_quote_alert.Domain;
using stock_quote_alert.Domain.Models;
using stock_quote_alert.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Monitoring
{
    // João Coimbra:
    // i would usually prefer to create a webhook that listens to every change in the asset, but that wont fit in the
    // runtime specification.
    // The monitoring works by fetching data from a asset every x seconds.
    class AssetMonitoring
    {
        private string assetToMonitor;
        private decimal sellPrice;
        private decimal referenceBuyPrice;
        private int intervalBetweenRequests;
        private int maxNumberOfTries;
        public AssetMonitoring(string _assetToMonitor, decimal _sellPrice, decimal _referenceBuyPrice)
        {
            assetToMonitor = _assetToMonitor;
            sellPrice = _sellPrice;
            referenceBuyPrice = _referenceBuyPrice;

            var configuration = SecretConfiguration.GetInstance();
            intervalBetweenRequests = Convert.ToInt32(configuration.GetSection("monitoringInterval").Value);
            maxNumberOfTries = Convert.ToInt32(configuration.GetSection("maxNumberOfTries").Value);
        }
        private MonitoringOutput? DecideMonitoringOutput(decimal assetValue, int numberOfTries)
        {
            if (assetValue >= sellPrice)
                return MonitoringOutput.AboveSellThreshold;
            else if (assetValue <= referenceBuyPrice)
                return MonitoringOutput.BellowBuyThreshold;
            else if (numberOfTries >= maxNumberOfTries)
                return MonitoringOutput.ReachedMaxNumberOfTries;
            else
                return null;
        }
        async public Task<MonitoringOutput> MonitorAsset()
        {
            var yahooService = new YahooAssetService();
            int numberOfTries = 0;
            MonitoringOutput? result = null;
            while(result == null)
            {
                // parses time in seconds to miliseconds and awaits a little bit in order not to
                // perform too many requests. If we dont do this our service can be interpreted as a DDOS.
                await Task.Delay(intervalBetweenRequests * 1000);
                var requestOutput = await yahooService.GetAssetData(assetToMonitor);

                result = DecideMonitoringOutput(requestOutput.regularMarketPrice, numberOfTries);
                numberOfTries++;
            }
            return (MonitoringOutput)result;
        }
    }
}
