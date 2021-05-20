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
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var filePath = Path.Combine(desktopPath, "Text1.txt");
                var text = File.ReadAllText(filePath);

                // Преобразуем текст в массив слов без знаков пунктуации
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
                var delimiters = new char[] { ' ', '\n', '\r' };
                var words = noPunctuationText.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Группируем слова по частоте встречаемости
                var wordGroups = words.GroupBy(w => w)
                                      .Select(w => new { Word = w.Key, Count = w.Count() })
                                      .OrderByDescending(w => w.Count);

                // Выводим топ-10 самых встречаемых слов
                foreach (var w in wordGroups.Take(10))
                {
                    Console.WriteLine($"Слово '{w.Word}' встречается {w.Count} раз(а)");
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
