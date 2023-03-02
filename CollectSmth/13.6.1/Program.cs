using System.Diagnostics;
namespace CollectSmth._13._6._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new();
            Stopwatch stopwatch1 = new();
            // не разобрался пока, как работать с классом Path
            string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Downloads\Text1.txt";

            List<string> list = new();
            LinkedList<string> linkedList = new();

            // кол-во раз, которое будет выполняться вставка
            int times = 10;

            // время, затраченное на вставку в мс
            double addToListResult, addToLinkedListResult;

            if (File.Exists(path))
            {
                // вставка в конец списка
                AddToLinkedList(stopwatch1, path, linkedList, times, out addToLinkedListResult);
                AddToList(stopwatch, path, list, times, out addToListResult);

                if (addToListResult < addToLinkedListResult)
                {
                    Console.WriteLine($"Вставка в List в среднем быстрее вставки LinkedList на {addToLinkedListResult - addToListResult:.####}мс");
                }
                if (addToListResult > addToLinkedListResult)
                {
                    Console.WriteLine($"Вставка в LinkedList в среднем быстрее вставки List на {addToListResult - addToLinkedListResult:.####}мс");
                }
                if (addToListResult == addToLinkedListResult)
                {
                    Console.WriteLine("Вставки выполняются в среднем за одно и тоже время");
                }

                Console.WriteLine();

                // вставка в середину списка
                AddToListMiddle(stopwatch, path, list, times, out addToListResult);
                AddToLinkedListMiddle(stopwatch1, path, linkedList, out addToLinkedListResult);

                if (addToListResult < addToLinkedListResult)
                {
                    Console.WriteLine($"Вставка в середину List в среднем быстрее вставки в середину LinkedList на {addToLinkedListResult - addToListResult:.####}мс");
                }
                if (addToListResult > addToLinkedListResult)
                {
                    Console.WriteLine($"Вставка в середину LinkedList в среднем быстрее вставки в середину List на {addToListResult - addToLinkedListResult:.####}мс");
                }
                if (addToListResult == addToLinkedListResult)
                {
                    Console.WriteLine("Вставки выполняются в среднем за одно и тоже время");
                }
            }
            else
            {
                Console.WriteLine("Неверно задан путь!");
            }
        }

        private static void AddToList(Stopwatch stopwatch, string path, List<string> list, int times, out double a)
        {
            Console.WriteLine("Вставка в List:\n");
            double averageTime = default;
            int t = times;
            while (t > 0)
            {
                stopwatch.Start();
                foreach (string s in File.ReadAllLines(path))
                {
                    list.Add(s);
                }
                stopwatch.Stop();

                Console.WriteLine($"{times - t} попытка - {stopwatch.Elapsed.TotalMilliseconds}мс");
                averageTime += stopwatch.Elapsed.TotalMilliseconds;
                stopwatch.Reset();
                list.Clear();
                t--;
            }
            a = averageTime / times;
            Console.WriteLine($"\nСреднее время выполнения вставки в List - {a:.####}мс\n");
        }

        private static void AddToLinkedList(Stopwatch stopwatch1, string path, LinkedList<string> linkedList, int times, out double b)
        {
            Console.WriteLine("Вставка в LinkedList:\n");
            double averageTime = default;
            int t = times;
            while (t > 0)
            {
                stopwatch1.Start();
                foreach (string s in File.ReadAllLines(path))
                {
                    linkedList.AddLast(s);
                }
                stopwatch1.Stop();

                Console.WriteLine($"{times - t} попытка - {stopwatch1.Elapsed.TotalMilliseconds}мс");
                averageTime += stopwatch1.Elapsed.TotalMilliseconds;
                stopwatch1.Reset();
                linkedList.Clear();
                t--;
            }

            b = averageTime / times;

            Console.WriteLine($"\nСреднее время выполнения вставки в LinkedList - {b:.####}мс\n");
        }
        /// <summary>
        /// пока получается очень долго, вставка в середину List в несколько раз быстрее.
        /// И вставка в конец в LinkedList в среднем медленнее, чем в List
        /// </summary>
        /// <param name="stopwatch1"></param>
        /// <param name="path"></param>
        /// <param name="linkedList"></param>
        /// <param name="b"></param>
        /// <param name="times"></param>
        private static void AddToLinkedListMiddle(Stopwatch stopwatch1, string path, LinkedList<string> linkedList, out double b, int times = 3)
        {
            Console.WriteLine("Вставка в середину LinkedList:\n");
            double averageTime = default;
            int t = times;
            while (t > 0)
            {
                stopwatch1.Start();
                foreach (string s in File.ReadAllLines(path))
                {
                    if (linkedList.Count > 1)
                    {
                        LinkedListNode<string> mid = linkedList.Find(File.ReadAllLines(path)[linkedList.Count / 2 - 1]);
                        if (mid != null)
                        {
                            linkedList.AddAfter(mid, s);
                        }
                    }
                    else
                    {
                        linkedList.AddLast(s);
                    }
                }
                stopwatch1.Stop();

                Console.WriteLine($"{times - t} попытка - {stopwatch1.Elapsed.TotalMilliseconds}мс");
                averageTime += stopwatch1.Elapsed.TotalMilliseconds;
                stopwatch1.Reset();
                linkedList.Clear();
                t--;
            }

            b = averageTime / times;

            Console.WriteLine($"\nСреднее время выполнения вставки в середину LinkedList - {b:.####}мс\n");
        }
        private static void AddToListMiddle(Stopwatch stopwatch, string path, List<string> list, int times, out double a)
        {
            Console.WriteLine("Вставка в середину List:\n");
            double averageTime = default;
            int t = times;
            while (t > 0)
            {
                stopwatch.Start();
                foreach (string s in File.ReadAllLines(path))
                {
                    if (list.Count > 1)
                    {
                        list.Insert(list.Count / 2 - 1, s);
                    }
                    else
                    {
                        list.Add(s);
                    }
                }
                stopwatch.Stop();

                Console.WriteLine($"{times - t} попытка - {stopwatch.Elapsed.TotalMilliseconds}мс");
                averageTime += stopwatch.Elapsed.TotalMilliseconds;
                stopwatch.Reset();
                list.Clear();
                t--;
            }
            a = averageTime / times;
            Console.WriteLine($"\nСреднее время выполнения вставки в середину List - {a:.####}мс\n");
        }
    }
}