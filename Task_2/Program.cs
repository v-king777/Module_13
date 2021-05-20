using System;
using System.IO;
using System.Linq;

namespace Task_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // Читаем текстовый файл с рабочего стола
                var desktopPath = Environment
                    .GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var filePath = Path.Combine(desktopPath, "Text1.txt");
                var text = File.ReadAllText(filePath);

                // Преобразуем текст в массив слов без знаков пунктуации
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
                var delimiters = new char[] { ' ', '\n', '\r' };
                var words = noPunctuationText
                    .ToLower()
                    .Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Группируем слова по частоте встречаемости (операторы LINQ)
                var wordGroups1 = from a in words
                                  group a by a into b
                                  select new { b.Key, Count = b.Count() } into c
                                  orderby c.Count descending
                                  select c;

                // Группируем слова по частоте встречаемости (методы расширения LINQ)
                var wordGroups2 = words
                    .GroupBy(a => a)
                    .Select(b => new { b.Key, Count = b.Count() })
                    .OrderByDescending(c => c.Count);

                // Выводим топ-10 самых встречаемых слов
                foreach (var word in wordGroups1.Take(10))
                {
                    Console.WriteLine($"Слово '{word.Key}' встречается {word.Count} раз(а)");
                }

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
