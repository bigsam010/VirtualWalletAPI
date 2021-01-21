using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.Enums;
using VirtualWalletApi.Data.Persistence;
using VirtualWalletApi.Data.RequestModels.CommandRequestModels;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;
using VirtualWalletApi.Utilities;

namespace VirtualWalletApi.Handlers.CommandHandlers
{
    public class WithdrawFundCommandHandler : IRequestHandler<WithdrawFundRequestModel, WithdrawFundResponseModel>
    {
        private readonly IRepository<Wallet> _walletRepo;
        private readonly IRepository<WalletTransaction> _walletTransRepo;
        private readonly IMapper _mapper;

        public WithdrawFundCommandHandler(IRepository<Wallet> walletRepo, IRepository<WalletTransaction> walletTransRepo, IMapper mapper)
        {
            _walletRepo = walletRepo;
            _walletTransRepo = walletTransRepo;
            _mapper = mapper;
        }
        public async Task<WithdrawFundResponseModel> Handle(WithdrawFundRequestModel request, CancellationToken cancellationToken)
        {
            if (request.Amount < 1)
            {
                throw new ArgumentException("Minimum allowed Withdrawal amount is 1");
            }
            var wallet = _walletRepo.SingleOrDefault(w => w.AccountNumber == request.AccountNumber);
            if (wallet == null)
            {
                throw new ArgumentException("Invalid wallet account number");
            }
            if (wallet.Balance < request.Amount)
            {
                throw new ArgumentException("Insufficient fund");
            }
            var trans = new WalletTransaction
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                BalanceAfter = wallet.Balance - request.Amount,
                BalanceBefore = wallet.Balance,
                CustomerId = WebHelper.CurrentCustomerId,
                Description = request.Description,
                Id = Guid.NewGuid(),
                Status = EWalletTransactionStatus.APPROVED,
                TransReferenceId = request.TransReference,
                Type = EWalletTransactionType.WITHDRAW
            };
            await _walletTransRepo.AddAsync(trans);
            wallet.Balance -= request.Amount;
            _walletRepo.Update(wallet);
            var response = _mapper.Map<WithdrawFundResponseModel>(trans);
            response.Label = wallet.Label;
            return response;
        }
    }
}
