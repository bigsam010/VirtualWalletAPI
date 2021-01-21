using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ResponseModels.QueryResponseModels
{
    public class FindTransactionResponseModel
    {
        public Guid TransId { set; get; }
        public string TransReference { set; get; }

        public string Type { set; get; }

        public string Description { set; get; }

        public string AccountNumber { set; get; }

        public decimal Amount { set; get; }
        public decimal BalanceBefore { set; get; }
        public decimal BalanceAfter { set; get; }

        public DateTime TransDate { set; get; }
        public string Status { set; get; }
    }
}
