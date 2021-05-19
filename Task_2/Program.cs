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
                var words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                // Группируем слова по частоте встречаемости
                var wordGroups = words.GroupBy(i => i)
                                      .Select(i => new { Word = i.Key, Count = i.Count() })
                                      .OrderByDescending(i => i.Count);

                // Выводим топ-10 самых встречаемых слов
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Слово '{wordGroups.ElementAt(i).Word}' " +
                        $"встречается {wordGroups.ElementAt(i).Count} раз(а)");
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
