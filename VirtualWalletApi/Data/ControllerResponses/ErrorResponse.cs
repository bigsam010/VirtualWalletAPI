using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ControllerResponses
{
    public class ErrorResponse:Response
    {
        public List<object> Errors { set; get; } = new List<object>();
    }
}
