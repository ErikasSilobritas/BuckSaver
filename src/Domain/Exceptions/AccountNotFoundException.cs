namespace Domain.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("A user by that id does not have any accounts")
        {

        }
    }
}
