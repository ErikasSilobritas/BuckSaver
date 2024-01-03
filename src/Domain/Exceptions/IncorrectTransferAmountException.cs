namespace Domain.Exceptions
{
    public class IncorrectTransferAmountException : Exception
    {
        public IncorrectTransferAmountException() : base("The amount must be at least 0.01")
        {

        }
    }
}
