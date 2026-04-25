using System.Globalization;

namespace DateValidationApp
{
    public class DateValidator
    {
        public void ValidateDate(string date)
        {
            if (!DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                throw new InvalidDateFormatException($"Строка '{date}' не является датой в формате ДД.ММ.ГГГГ.");
            }

            Console.WriteLine("Дата успешно прошла проверку.");
        }
    }
}
