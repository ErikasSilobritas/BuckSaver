using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base ("A user by that id does not have any accounts")
        {
            
        }
    }
}
