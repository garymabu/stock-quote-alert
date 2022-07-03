using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    static class Constants
    {
        static public string assetToMonitorExceptionKey
        {
            get => "Asset to monitor";
        }
        static public string sellPriceExceptionKey
        {
            get => "Asset's sell price";
        }
        static public string referenceBuyPriceExceptionKey
        {
            get => "Asset's reference buy price";
        }
        static public string notFoundExceptionText
        {
            get => "The required asset was not found.";
        }
        static public string maxNumberOfAttemptsReachedExceptionMessage
        {
            get => "Max number of attempts reached.";
        }
        static public string monitoringSellAction
        {
            get => "sell your asset";
        }
        static public string monitoringBuyAction
        {
            get => "buy more assets";
        }
        static public string monitoringEmailSubject
        {
            get => "Monitoring results: you need to {0}!";
        }
        static public string monitoringMarkupEmailBody
        {
            get => @"
                <p>The parameters for the application have been met.</p>
                <p>That means that you need to <b>{0}</b></p>
            ";
        }
        static public string monitoringTextEmailBody
        {
            get => @"
                The parameters for the application have been met.
                That means that you need to {0}!
            ";
        }
    }
}
