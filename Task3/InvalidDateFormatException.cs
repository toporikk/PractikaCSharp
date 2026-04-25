namespace DateValidationApp
{
    public class InvalidDateFormatException : Exception
    {
        public InvalidDateFormatException() : base("Некорректный формат даты.") { }
        public InvalidDateFormatException(string message) : base(message) { }
        public InvalidDateFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
