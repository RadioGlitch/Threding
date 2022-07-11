back:
Console.Write("Введите количество потоков: ");

int number_of_thread;

try {number_of_thread = Convert.ToInt32(Console.ReadLine()); }
catch { Console.WriteLine("Только целые числа"); goto back; }

Console.WriteLine();

if (number_of_thread <= 0) { Console.WriteLine("Количество потоков меньше 1. Завершение работы"); Environment.Exit(0); }

List<string> result = new List<string>();

if (File.Exists($"{Directory.GetCurrentDirectory()}\\output.txt") != true)
{
    FileStream filestream = new FileStream(@$"{Directory.GetCurrentDirectory()}\\output.txt", FileMode.Create);
    filestream.Close();
}

Thread[] Threads = new Thread[number_of_thread];

for (int i = 0; i < Threads.Length ; i++)
{
    Threads[i] = new(Proces);
    Threads[i].Name = $"{i + 1}";
    Threads[i].Start();
}

for (int i = 0; i < Threads.Length; i++)
{
    Threads[i].Join();
}

for (int i = 0; i < result.Count; i++)
{
    Console.WriteLine(result[i].ToString());
}
File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\output.txt", result);

Console.WriteLine($"Готово!\n Файл создат по следующему пути: {Directory.GetCurrentDirectory()}\\output.txt");
Console.ReadKey();

void Proces()
{
    for (int i = 1; i < Threads.Length + 1; i++)
    {
        result.Add($"{Thread.CurrentThread.Name}: {Convert.ToInt32(Thread.CurrentThread.Name) * Threads.Length * i}");
        Thread.Sleep(1);
    }
}