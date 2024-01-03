using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AccountNumberExceededException : Exception
    {
        public AccountNumberExceededException() : base("The number of accounts for current user exceeds the limit.")
        {
            
        }
    }
}
