using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using VirtualWalletApi.Controllers;
using VirtualWalletApi.Data.Entities;
using VirtualWalletApi.Data.RequestModels.CommandRequestModels;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;
using Xunit;

namespace VirtualWallet.Testing
{
    public class WalletControllerTest
    {

        private Mock<IMediator> Mediator;
        public WalletControllerTest()
        {
            Mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Ensure_Create_Wallet_Success_Result()
        {
            //Arrange
            var createWallet = new CreateWalletRequestModel();
            Mediator.Setup(x => x.Send(It.IsAny<CreateWalletRequestModel>(), new CancellationToken())).
                ReturnsAsync(new CreateWalletResponseModel { Label = "New Wallet", Id = Guid.NewGuid() });
            var walletController = new WalletController(Mediator.Object);

            //Action
            var result = await walletController.Post(createWallet);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Fund_Wallet_Should_Return_Bad_Request_Response_When_Amount_Is_Less_Than_One()
        {
            //Arrange
            var fundWallet = new FundWalletRequestModel { Amount = 0 };
            Mediator.Setup(x => x.Send(It.IsAny<FundWalletRequestModel>(), new CancellationToken())).
                Throws(new ArgumentException());
            var walletController = new WalletController(Mediator.Object);

            //Action
            var result = await walletController.Fund(fundWallet);

            //Assert

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Withdraw_Wallet_Fund_Should_Return_Bad_Request_Response_When_Amount_Is_Less_Than_One()
        {


            //Arrange
            var withdrawWallet = new WithdrawFundRequestModel { Amount = 0 };
            Mediator.Setup(x => x.Send(It.IsAny<WithdrawFundRequestModel>(), new CancellationToken())).
                Throws(new ArgumentException());
            var walletController = new WalletController(Mediator.Object);

            //Action
            var result = await walletController.Withdraw(withdrawWallet);

            //Assert

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Withdraw_Wallet_Fund_Should_Return_Bad_Request_Response_When_Amount_Is_More_Than_Balance()
        {


            //Arrange
            Wallet wallet = new Wallet { AccountNumber = "0123456789", Balance = 100 };
            var withdrawWallet = new WithdrawFundRequestModel { Amount = 200, AccountNumber = wallet.AccountNumber };
            Mediator.Setup(x => x.Send(It.IsAny<WithdrawFundRequestModel>(), new CancellationToken())).
                Throws(new ArgumentException());
            var walletController = new WalletController(Mediator.Object);

            //Action
            var result = await walletController.Withdraw(withdrawWallet);

            //Assert

            Assert.IsType<BadRequestObjectResult>(result);
        }


    }
}
