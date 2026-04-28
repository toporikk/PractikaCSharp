namespace FileSystemApp
{
    public class FileManager
    {
        public void CreateAndWrite(string path, string content)
        {
            File.WriteAllText(path, content);
            Console.WriteLine($"Файл создан: {path}");
        }

        public void ReadAndPrint(string path)
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                Console.WriteLine($"Содержимое файла: {content}");
            }
        }

        public void Copy(string source, string dest)
        {
            File.Copy(source, dest, true);
            Console.WriteLine($"Файл скопирован в: {dest}. Существует: {File.Exists(dest)}");
        }

        public void Move(string source, string dest)
        {
            if (File.Exists(source))
            {
                File.Move(source, dest);
                Console.WriteLine($"Файл перемещен в: {dest}");
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine($"Файл {path} удален.");
                }
                else
                {
                    throw new FileNotFoundException("Файл не найден для удаления.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
            }
        }

        public void DeleteByPattern(string directory, string pattern)
        {
            string[] files = Directory.GetFiles(directory, pattern);
            foreach (var file in files)
            {
                File.Delete(file);
                Console.WriteLine($"Удален файл по шаблону: {Path.GetFileName(file)}");
            }
        }
    }
}
