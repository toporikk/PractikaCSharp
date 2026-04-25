using System;

namespace DateValidationApp
{

    class Program
    {
        static void Main(string[] args)
        {
            DateValidator validator = new DateValidator();

            try
            {
                Console.Write("Введите дату (дд.мм.гггг): ");
                string input = Console.ReadLine();

                validator.ValidateDate(input);
            }
            catch (InvalidDateFormatException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
