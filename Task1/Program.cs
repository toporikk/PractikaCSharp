using System;

namespace BatteryApp
{

    class Program
    {
        static void Main(string[] args)
        {
            BatteryMonitor monitor = new BatteryMonitor();

            try { 
            
                monitor.CheckBatteryLevel(3);
            }
            catch (LowBatteryException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
