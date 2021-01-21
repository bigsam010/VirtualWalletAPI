using MediatR;
using Microsoft.AspNetCore.Http;
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
    [Route("api/v1/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;

        }

        /// <summary>
        /// Endpoint to signup a customer. 
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<SignUpResponseModel>), 201)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SignUpRequestModel signUpRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Validation error-all fields are required, with a valid email format"
                });

            try
            {
                var response = await _mediator.Send(signUpRequest);
                return Created("api/customer", new PayloadedResponse<SignUpResponseModel>
                {
                    Data = response,
                    Message = "Signup completed successfully",
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
        /// Endpoint to authenticate a customer. 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PayloadedResponse<LoginResponseModel>), 200)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestModel loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "Validation error-all fields are required, with a valid email format"
                });

            try
            {
                var response = await _mediator.Send(loginRequest);
                return Ok(new PayloadedResponse<SignUpResponseModel>
                {
                    Data = response,
                    Message = "Authentication was successful",
                    Success = true

                });

            }
            catch (ArgumentException ex)
            {
                return Unauthorized(new Response
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
