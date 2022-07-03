using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Domain.Models
{
    enum MonitoringOutput
    {
        AboveSellThreshold,
        BellowBuyThreshold,
        ReachedMaxNumberOfTries
    }
}
