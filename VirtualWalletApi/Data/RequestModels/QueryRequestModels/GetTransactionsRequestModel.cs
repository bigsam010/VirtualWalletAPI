using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Enums;

namespace VirtualWalletApi.Data.ResponseModels.QueryResponseModels
{
    public class GetTransactionsRequestModel : IRequest<List<GetTransactionsResponseModel>>
    {
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }

        public EWalletTransactionType? Type { set; get; }
        public string AccountNumber { set; get; }
        public EWalletTransactionStatus? Status { set; get; }
        public int PageSize { set; get; } = 10;
        public int PageNo = 1;

        public string Sort { set; get; } = "desc";
    }
}
