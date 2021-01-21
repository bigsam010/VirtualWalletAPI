using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Data.ControllerResponses
{
    public class PayloadedResponse<T>:Response
    {
        public T Data { set; get; }
    }
}
