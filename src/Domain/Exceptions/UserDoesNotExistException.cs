namespace Domain.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public UserDoesNotExistException() : base("User by that ID does not exist")
        {

        }
    }
}
