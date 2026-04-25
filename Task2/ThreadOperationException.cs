namespace ThreadErrorApp
{
    public class ThreadOperationException : Exception
    {
        public ThreadOperationException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
