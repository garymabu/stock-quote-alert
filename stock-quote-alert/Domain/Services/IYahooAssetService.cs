using Refit;
using stock_quote_alert.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Domain
{
    interface IYahooAssetService
    {
        [Get("/v7/finance/quote")]
        Task<YahooFetchAssetOutput> FetchAssetData(string symbols);
    }
}
