using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class IncorrectTransferAmountException : Exception
    {
        public IncorrectTransferAmountException() : base ("The amount must be at least 0.01")
        {
            
        }
    }
}
