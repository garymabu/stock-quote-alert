using stock_quote_alert.Configuration;
using stock_quote_alert.Monitoring;
using stock_quote_alert.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    class StockQuoteAlert
    {
        private AssetMonitoring monitoring;

        public StockQuoteAlert(string _assetToMonitor, decimal _sellPrice, decimal _referenceBuyPrice)
        {
            monitoring = new AssetMonitoring(_assetToMonitor, _sellPrice, _referenceBuyPrice);
        }

        async public Task MonitorAssetAndAlertResult()
        {
            var output = await monitoring.MonitorAsset();
            if (output == Domain.Models.MonitoringOutput.ReachedMaxNumberOfTries)
                throw new Exception(Constants.maxNumberOfAttemptsReachedExceptionMessage);
            var isToSell = output == Domain.Models.MonitoringOutput.AboveSellThreshold;
            var necessaryAction = isToSell ? Constants.monitoringSellAction : Constants.monitoringBuyAction;
            var emailSubject = string.Format(Constants.monitoringEmailSubject, necessaryAction);
            var emailBody = string.Format(Constants.monitoringTextEmailBody, necessaryAction);
            var emailBodyMarkup = string.Format(Constants.monitoringMarkupEmailBody, necessaryAction);

            var emailService = new EmailService();

            var sendEmailTo = SecretConfiguration.GetInstance().GetSection("alertDestinyEmail").Value;
            emailService.SendMail(sendEmailTo, emailSubject, emailBody, emailBodyMarkup);
        }
    }
}
