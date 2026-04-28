using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace FileSystemApp
{
    class Program
    {
        static void Main()
        {
            string lastName = "petrov.pp"; 
            string newName = "petrov.po"; 
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDir, lastName);
            string copyPath = filePath + ".copy";
            string moveDir = Path.Combine(currentDir, "NewFolder");

            FileManager fm = new FileManager();
            FileInfoProvider info = new FileInfoProvider();

            fm.CreateAndWrite(filePath, "Тестовая запись для задания.");
            fm.ReadAndPrint(filePath);

            info.PrintFileInfo(filePath);

            fm.Copy(filePath, copyPath);

            if (!Directory.Exists(moveDir)) Directory.CreateDirectory(moveDir);
            string movedPath = Path.Combine(moveDir, lastName);
            fm.Move(copyPath, movedPath);

            string renamedPath = Path.Combine(currentDir, newName);
            if (File.Exists(renamedPath)) File.Delete(renamedPath);
            File.Move(filePath, renamedPath);
            Console.WriteLine($"Файл переименован в: {newName}");

            fm.CreateAndWrite(filePath, "Короткий текст");
            info.CompareFiles(filePath, renamedPath);

            Console.WriteLine("\nСписок всех файлов в директории:");
            string[] allFiles = Directory.GetFiles(currentDir);
            foreach (var f in allFiles) Console.WriteLine(Path.GetFileName(f));

            File.SetAttributes(filePath, FileAttributes.ReadOnly);
            Console.WriteLine($"\nАтрибуты файла {Path.GetFileName(filePath)}: {File.GetAttributes(filePath)}");
            try
            {
                File.WriteAllText(filePath, "Попытка записи");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Запись запрещена (ошибка обработана).");
            }
            File.SetAttributes(filePath, FileAttributes.Normal); 

            Console.WriteLine("\nОчистка...");
            fm.DeleteByPattern(currentDir, "*.ii");
            fm.DeleteFile("non_existent_file.txt");

            Console.WriteLine("\nРабота завершена.");
            Console.ReadKey();
        }
    }
}
