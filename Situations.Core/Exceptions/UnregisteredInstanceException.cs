namespace Situations.Core.Exceptions
{
    public class UnregisteredInstanceException : Exception
    {
        public UnregisteredInstanceException(string message) : base(message) { }
    }
}
