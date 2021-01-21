using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualWalletApi.Data.ControllerResponses;
using VirtualWalletApi.Data.RequestModels.CommandRequestModels;
using VirtualWalletApi.Data.RequestModels.QueryRequestModels;
using VirtualWalletApi.Data.ResponseModels.CommandResponseModels;
using VirtualWalletApi.Data.ResponseModels.QueryResponseModels;

namespace VirtualWalletApi.Controllers
{
    [Route("api/v1/wallet")]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint to create wallet. The label field is optional 
        /// </summary>
        /// <param name="createWalletRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<CreateWalletResponseModel>), 201)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWalletRequestModel createWalletRequest)
        {

            try
            {
                var response = await _mediator.Send(createWalletRequest);
                return Created("api/wallet", new PayloadedResponse<CreateWalletResponseModel>
                {
                    Data = response,
                    Message = "Wallet created successfully",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Errors = { ex },
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        /// <summary>
        /// Endpoint to fund wallet. Only accountNumber and amount fields are required
        /// </summary>
        /// <param name="fundRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<FundWalletResponseModel>), 200)]
        [HttpPost("fund")]
        public async Task<IActionResult> Fund([FromBody] FundWalletRequestModel fundRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Validation error-accountNumber and amount fields are required, with a valid amount."
                });

            try
            {
                var response = await _mediator.Send(fundRequest);
                return Ok(new PayloadedResponse<FundWalletResponseModel>
                {
                    Data = response,
                    Message = "Wallet funded successfully",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Errors = { ex },
                    Success = false,
                    Message = ex.Message
                });
            }

        }


        /// <summary>
        /// Endpoint to fund wallet. Only accountNumber and amount fields are required
        /// </summary>
        /// <param name="withdrawRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<WithdrawFundResponseModel>), 200)]
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawFundRequestModel withdrawRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Validation error-accountNumber and amount fields are required, with a valid amount."
                });

            try
            {
                var response = await _mediator.Send(withdrawRequest);
                return Ok(new PayloadedResponse<WithdrawFundResponseModel>
                {
                    Data = response,
                    Message = "Fund withdrawn from wallet successfully",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Errors = { ex },
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        /// <summary>
        /// Endpoint to get wallet transaction's details by Id
        /// </summary>
        /// <param name="transId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<FindTransactionResponseModel>), 200)]
        [HttpGet("transaction/{transId}")]
        public async Task<IActionResult> FindTransaction([FromRoute] Guid transId)
        {


            try
            {
                var request = new FindTransactionRequestModel();
                request.TransId = transId;
                var response = await _mediator.Send(request);
                return Ok(new PayloadedResponse<FindTransactionResponseModel>
                {
                    Data = response,
                    Message = "Transaction details retrieved successfully",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return NotFound(new Response
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Errors = { ex },
                    Success = false,
                    Message = ex.Message
                });
            }

        }
        public static int transactionCount = 0;

        /// <summary>
        /// Endpoint to get all wallet transactions. Type Possible Values{ DEPOSIT = 1,WITHDRAW = 2}, Status Possible Values {APPROVED = 1,PENDING = 2, DECLINED = 3}
        /// Sort Possible Values{ ASC, DESC}
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(PagedResponse<GetTransactionsResponseModel>), 200)]
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsRequestModel request)
        {
            try
            {              
                var response = await _mediator.Send(request);
                return Ok(new PagedResponse<GetTransactionsResponseModel>(response,request.PageNo,request.PageSize,transactionCount)
                {
                  
                    Message = "Transactions detail retrieved successfully",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Errors = { ex },
                    Success = false,
                    Message = ex.Message
                });
            }

        }
    }
}
