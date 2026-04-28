namespace FileOrderSystem
{
    public class OrderFileReader
    {
        private readonly string _filePath;

        public OrderFileReader(string filePath) => _filePath = filePath;

        public bool VerifyFile()
        {
            if (!File.Exists(_filePath)) return false;

            Console.WriteLine("--- Проверка данных из файла ---");
            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 3 &&
                            int.TryParse(parts[0], out _) &&
                            int.TryParse(parts[2], out _))
                        {
                            Console.WriteLine($"Корректная строка: {line}");
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка в данных: {line}");
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении: {ex.Message}");
                return false;
            }
        }
    }
}
