using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Domain.Models
{   
    public class YahooQuoteError
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class YahooQuote
    {
        public decimal regularMarketPrice { get; set; }
    }
    public class YahooQuoteResponse
    {
        public IEnumerable<YahooQuote> result { get; set; }
        public YahooQuoteError error { get; set; }
    }
    public class YahooFetchAssetOutput
    {
        public YahooQuoteResponse quoteResponse { get; set; }
    }
}
