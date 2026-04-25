namespace BatteryApp
{
    public class LowBatteryException : Exception
    {
        public LowBatteryException() : base("Уровень заряда критически низкий!") { }

        public LowBatteryException(string message) : base(message) { }

        public LowBatteryException(string message, Exception inner) : base(message, inner) { }
    }
}
