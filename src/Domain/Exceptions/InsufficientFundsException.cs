namespace Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("You ain't got enough dough bruh")
        {

        }
    }
}
