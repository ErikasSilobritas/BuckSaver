using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidAccountTypeException : Exception
    {
        public InvalidAccountTypeException() : base("The requested account type is invalid. You may choose either (Savings) or (Checking) account")
        {
            
        }
    }
}
