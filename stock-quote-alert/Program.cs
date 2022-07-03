using System;
using stock_quote_alert.Util;

namespace stock_quote_alert
{
    class Program
    {
        static void Main(string[] args)
        {
            var argReader = new ArgReader(args);
            string assetToMonitor = argReader.SafeGetPositionalArgValue(0, Constants.assetToMonitorExceptionKey);
            string sellPrice = argReader.SafeGetPositionalArgValue(1, Constants.sellPriceExceptionKey);
            string referenceBuyPrice = argReader.SafeGetPositionalArgValue(2, Constants.referenceBuyPriceExceptionKey);

            var stockQuoteAlert = new StockQuoteAlert(
                assetToMonitor,
                decimal.Parse(sellPrice),
                decimal.Parse(referenceBuyPrice)
            );
            stockQuoteAlert.MonitorAssetAndAlertResult().Wait();
        }
    }
}
