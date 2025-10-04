using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace task2
{
    internal class Program
    {
        static List<FileInfo> findFile(string name, DirectoryInfo directory)
        {
            List<FileInfo> resultFiles = new List<FileInfo>();

            if (directory.Exists)
            {
                FileInfo[] files = directory.GetFiles();

                var file = from f in files
                           where f.Name == name
                           select f;

                foreach (var f in file) resultFiles.Add(f);

                DirectoryInfo[] dirs = directory.GetDirectories();

                if (dirs.Length != 0)
                {
                    foreach (DirectoryInfo dir in dirs)
                    {
                        List<FileInfo> Subdirfiles = findFile(name, dir);
                        foreach (var subdirfile in Subdirfiles) resultFiles.Add(subdirfile);
                    }
                }

                return resultFiles;
            }
            else throw new Exception("Не существующая директория");
        }

        static async void Print(string path)
        {
            Console.WriteLine();
            using (FileStream fstream = File.OpenRead(path))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);
                Console.WriteLine($"Текст из файла {path}:\n{textFromFile}");
            }
        }
        
        static void Compres(List<FileInfo> files)
        {
            Console.WriteLine();

            string dir = "C:\\Бауманка";
            string source_dir = "temp";
            DirectoryInfo directory = new DirectoryInfo(dir);
            directory.CreateSubdirectory(source_dir);


            foreach (FileInfo file in files)
            {
                string temp = dir + "\\" + source_dir + "\\" + file.Name;
                file.CopyTo(temp, true);
            }

            string zipFile = "C:\\Бауманка\\FindedFiles.zip";

            if (File.Exists(zipFile))//Удаляем, если есть уже созданный архив
            {
                File.Delete(zipFile);
            }

            ZipFile.CreateFromDirectory(dir+"\\"+source_dir, zipFile);
            Console.WriteLine($"Папка {dir} архивирована в файл {zipFile}");
        }
        static async Task DecompressAsync(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate);
            // поток для записи восстановленного файла
            using FileStream targetStream = File.Create(targetFile);
            // поток разархивации
            using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
            await decompressionStream.CopyToAsync(targetStream);
            Console.WriteLine($"Восстановлен файл: {targetFile}");
        }
        static async Task CompressAsync(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate);
            // поток для записи сжатого файла
            using FileStream targetStream = File.Create(compressedFile);

            // поток архивации
            using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
            await sourceStream.CopyToAsync(compressionStream); // копируем байты из одного потока в другой

            Console.WriteLine($"\nСжатие файла {sourceFile} завершено.");
        }
        async static Task Main(string[] args)
        {
            ///////////////////////////////////////////////////////////////////////         НЕ ЗАБЫТЬ ПРОВЕРИТЬ ФАЙЛЫ ПЕРЕД ЗАПУСКОМ
            Console.WriteLine("Введите название искомого файла c расширением, например: Я.jpg");
            //string? name = Convert.ToString(Console.ReadLine());
            string? name = "Я.jpg";
            //FileInfo file = new FileInfo(name);
            //Console.WriteLine(file.Name == name);

            Console.WriteLine("Введите название директории, в которой будет производится поиск нужного файла");
            //string? path = Convert.ToString(Console.ReadLine());
            string? path = "C:\\Данные";

            DirectoryInfo directory = new DirectoryInfo(path);

            List<FileInfo> resFiles = findFile(name, directory);//Получаем список найденных файлов

            Console.WriteLine();
            foreach (FileInfo file in resFiles) Console.WriteLine($"Пути найденных файлов с именем {name}: {file.FullName}");

            //Print("C:\\Мусор\\Коды стим.txt");// вывод содержимого файла
            Print(@"C:\Бауманка\ИУ8\Семестр 2\Компьютерная Графика\Компьютерная Графика.txt");

            string zipFile = "C:\\Бауманка\\FindedFiles.gz";
            if (resFiles.Count != 0) await CompressAsync(resFiles.First().FullName, zipFile);
            //Compres(resFiles);//Через Zip

            //await DecompressAsync(zipFile, "C:\\Бауманка\\z.jpg");
        }
    }
}
