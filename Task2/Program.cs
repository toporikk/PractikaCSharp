using System;

namespace ThreadErrorApp
{

    class Program
    {
        static void Main(string[] args)
        {
            ThreadManager manager = new ThreadManager();

            try
            {
                manager.ExecuteWork();
            }
            catch (ThreadOperationException ex)
            {
                Console.WriteLine("ЛОГ ОШИБКИ");
                Console.WriteLine($"Сообщение: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                }

                Console.WriteLine("\nСТЕК ВЫЗОВОВ");
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadKey();
        }
    }
}
