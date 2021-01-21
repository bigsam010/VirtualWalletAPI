using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;

namespace VirtualWalletApi.Utilities
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, SignUpResponseModel>();
            CreateMap<Customer, LoginResponseModel>();
            CreateMap<Wallet, CreateWalletResponseModel>();
            CreateMap<WalletTransaction, FundWalletResponseModel>().AfterMap((src, dest) =>
            {
                dest.TransId = src.Id;

            });
            CreateMap<WalletTransaction, WithdrawFundResponseModel>().AfterMap((src, dest) =>
            {
                dest.TransId = src.Id;

            });
            CreateMap<WalletTransaction, FindTransactionResponseModel>().AfterMap((src, dest) =>
            {
                dest.TransId = src.Id;

            });

            CreateMap<List<WalletTransaction>, List<GetTransactionsResponseModel>>().AfterMap((src, dest) =>
            {
                src.ForEach(f =>
                {
                    dest.Add(new GetTransactionsResponseModel
                    {
                        Type = f.Type.ToString(),
                        AccountNumber = f.AccountNumber,
                        Amount = f.Amount,
                        BalanceAfter = f.BalanceAfter,
                        BalanceBefore = f.BalanceBefore,
                        Description = f.Description,
                        Status = f.Status.ToString(),
                        TransDate = f.TransDate,
                        TransId = f.Id,
                        TransReference = f.TransReferenceId
                    });

                });
            });

        }

    }
}
