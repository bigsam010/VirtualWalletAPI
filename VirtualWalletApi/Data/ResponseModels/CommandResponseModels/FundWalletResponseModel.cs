using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ResponseModels.CommandResponseModels
{
    public class FundWalletResponseModel
    {
        public Guid TransId { set; get; }
        public string AccountNumber { set; get; }
        public string Label { set; get; }
        public string Description { set; get; }
        public decimal Amount { set; get; }
        public decimal BalanceBefore { set; get; }
        public decimal BalanceAfter { set; get; }
    }
}
