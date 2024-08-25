namespace Situations.Core.Exceptions
{
    public class UnregisteredSituationException : Exception
    {
        public UnregisteredSituationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
