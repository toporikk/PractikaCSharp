namespace BatteryApp
{
    public class BatteryMonitor
    {
        public void CheckBatteryLevel(int level)
        {
            if (level < 5)
            {
                throw new LowBatteryException($"Внимание! Заряд всего {level}%. Срочно подключите зарядное устройство.");
            }
            Console.WriteLine($"Уровень заряда в норме: {level}%");
        }
    }
}
