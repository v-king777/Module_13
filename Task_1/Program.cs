using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Task_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // Читаем текстовый файл с рабочего стола
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var filePath = Path.Combine(desktopPath, "Text1.txt");
                var text = File.ReadAllText(filePath);

                // Преобразуем текст в массив слов без знаков пунктуации
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
                var delimiters = new char[] { ' ', '\n', '\r' };
                var words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Создаём простой список из массива
                var wordList = new List<string>(words);

                // Запускаем секундомер
                var stopwatch = Stopwatch.StartNew();

                // Вставляем строку в середину списка
                wordList.Insert(wordList.Count / 2, "НОВАЯ СТРОКА");

                // Выводим результат
                Console.WriteLine($"Время вставки строки в простой список {stopwatch.Elapsed.TotalMilliseconds} мс");
                Console.WriteLine("Нажмите <Enter>...");
                Console.ReadLine();

                // Создаём связный список из простого
                var linkedList = new LinkedList<string>(wordList);

                // находим в списке указанное значение
                var node = linkedList.Find("НОВАЯ СТРОКА");

                // Запускаем секундомер
                stopwatch = Stopwatch.StartNew();

                // Вставляем строку после указанного значения
                linkedList.AddAfter(node, "ЕЩЁ ОДНА НОВАЯ СТРОКА");

                // Выводим результат и видим, что вставка в связный список работает быстрее примерно в 167 раз (но это не точно)
                Console.WriteLine($"Время вставки строки в связный список {stopwatch.Elapsed.TotalMilliseconds} мс");
                Console.WriteLine("Нажмите <Enter>...");
                Console.ReadLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл 'Text1.txt' не найден!");
                Console.WriteLine("Скопируйте его на рабочий стол и перезапустите программу.");
                Console.WriteLine("Нажмите <Enter>...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка!");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Нажмите <Enter>...");
                Console.ReadLine();
            }
        }
    }
}
