namespace FileSystemApp
{
    public class FileInfoProvider
    {
        public void PrintFileInfo(string path)
        {
            FileInfo info = new FileInfo(path);
            if (info.Exists)
            {
                Console.WriteLine($"--- Информация о файле: {info.Name} ---");
                Console.WriteLine($"Размер: {info.Length} байт");
                Console.WriteLine($"Создан: {info.CreationTime}");
                Console.WriteLine($"Изменен: {info.LastWriteTime}");
            }
        }

        public void CompareFiles(string path1, string path2)
        {
            FileInfo f1 = new FileInfo(path1);
            FileInfo f2 = new FileInfo(path2);
            if (f1.Exists && f2.Exists)
            {
                string result = f1.Length > f2.Length ? "первый больше" : "второй больше (или равны)";
                Console.WriteLine($"Сравнение размеров: {result}");
            }
        }
    }
}
