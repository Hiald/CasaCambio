using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CambioTotalED
{
    public class api
    {
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string id { get; set; }
        public string currencyId { get; set; }
        public string exchangeCurrencyId { get; set; }
        public string savingsRateId { get; set; }
        public string amountSale { get; set; }
        public string amountBuy { get; set; }
        public bool active { get; set; }
        public currency currency;
        public exchangeCurrency exchangeCurrency;

    }
}
