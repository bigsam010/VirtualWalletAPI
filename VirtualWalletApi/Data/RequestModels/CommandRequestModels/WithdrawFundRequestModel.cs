using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;

namespace VirtualWalletApi.Data.RequestModels.CommandRequestModels
{
    public class WithdrawFundRequestModel : IRequest<WithdrawFundResponseModel>
    {
        [Required]
        public string AccountNumber { set; get; }
        [Required]
        public decimal Amount { set; get; }
        public string TransReference { set; get; }
        public string Description { set; get; }
    }
}
