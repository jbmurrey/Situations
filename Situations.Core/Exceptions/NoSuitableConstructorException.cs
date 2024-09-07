namespace Situations.Core.Exceptions
{
    public class NoSuitableConstructorException : Exception
    {
        public NoSuitableConstructorException(string message) : base(message) { }
    }
}
