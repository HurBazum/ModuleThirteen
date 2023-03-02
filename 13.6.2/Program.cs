using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Downloads\Text1.txt";

        Dictionary<string, int> repeatWords = new();

        if(File.Exists(path))
        {
            var noPunctuationText = new string(File.ReadAllText(path).Where(c => !char.IsPunctuation(c)).ToArray());

            var textsWords = noPunctuationText.Split();

            foreach(var text in textsWords) 
            {
                if(repeatWords.ContainsKey(text))
                {
                    repeatWords[text]++;
                }
                if(!repeatWords.ContainsKey(text) && !string.IsNullOrEmpty(text))
                {
                    repeatWords.Add(text, 1);
                }
            }

            // сортировка словаря по значению в порядке убывания
            repeatWords = repeatWords.OrderByDescending(x => x.Value).ToDictionary(a => a.Key, b => b.Value);

            int count = 10;
            Console.WriteLine($"{count} самых часто используемых слов в данном тексте:");
            foreach (var txt in repeatWords)
            {
                if (count > 0)
                {
                    Console.WriteLine($"Слово \"{txt.Key}\" использовано {txt.Value} раз;");
                    count--;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Неверно задан путь к файлу!");
        }
    }
}