namespace Domain.Exceptions
{
    public class AccountNumberExceededException : Exception
    {
        public AccountNumberExceededException() : base("The number of accounts for current user exceeds the limit.")
        {

        }
    }
}
