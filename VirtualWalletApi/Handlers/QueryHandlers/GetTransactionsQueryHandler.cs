using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Controllers;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.Persistence;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;
using VirtualWalletApi.Utilities;

namespace VirtualWalletApi.Handlers.QueryHandlers
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsRequestModel, List<GetTransactionsResponseModel>>
    {

        private readonly IRepository<WalletTransaction> _walletTransRepo;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IRepository<WalletTransaction> walletTransRepo, IMapper mapper)
        {
            _walletTransRepo = walletTransRepo;
            _mapper = mapper;

        }
        public async Task<List<GetTransactionsResponseModel>> Handle(GetTransactionsRequestModel request, CancellationToken cancellationToken)
        {

            if (request.StartDate != null && request.EndDate == null)
            {
                throw new ArgumentException("End date is required");
            }
            if (request.StartDate == null && request.EndDate != null)
            {
                throw new ArgumentException("Start date is required");
            }
            if (request.StartDate != null && request.EndDate != null && request.EndDate < request.StartDate)
            {
                throw new ArgumentException("End date cannot be less than start date");
            }
            if (request.PageNo < 1)
            {
                request.PageNo = 1;
            }
            if (request.PageSize < 1)
            {
                request.PageSize = 1;
            }
            int skip = (request.PageNo - 1) * request.PageSize;

            var trans = _walletTransRepo.QueryAll(t => t.CustomerId == WebHelper.CurrentCustomerId);
            trans = request.Type == null ? trans : trans.Where(t => t.Type == request.Type);
            trans = request.Status == null ? trans : trans.Where(t => t.Status == request.Status);
            trans = request.AccountNumber == null ? trans : trans.Where(t => t.AccountNumber == request.AccountNumber);
            trans = request.StartDate == null ? trans : trans.Where(t => t.TransDate >= request.StartDate && t.TransDate <= request.EndDate);
            WalletController.transactionCount = trans.Count();
            trans = trans.Skip(skip).Take(request.PageSize);
            trans = request.Sort.ToLower() == "asc" ? trans.OrderBy(t => t.TransDate) : trans.OrderByDescending(t => t.TransDate);
            return _mapper.Map<List<GetTransactionsResponseModel>>(trans.ToList());

        }
    }
}
