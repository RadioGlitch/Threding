object loker = new();
string FilePath = $"{Directory.GetCurrentDirectory()}\\output.txt";
int number_of_thread;

back:
Console.Write("Введите количество потоков: ");

try {number_of_thread = Convert.ToInt32(Console.ReadLine()); }
catch { Console.WriteLine("Только целые числа"); goto back; }

Console.WriteLine();

if (number_of_thread <= 0) { Console.WriteLine("Количество потоков меньше 1. Завершение работы"); Environment.Exit(0); }

if (File.Exists($"{FilePath}") != true)
{
    FileStream filestream = new FileStream($"{FilePath}", FileMode.Create);
    filestream.Close();
}
else
{
    File.WriteAllText($"{FilePath}", null);
}

Thread[] Threads = new Thread[number_of_thread];

for (int i = 0; i < Threads.Length ; i++)
{
    Threads[i] = new(Proces);
    Threads[i].Name = $"{i + 1}";
    Threads[i].Start();
}

Console.WriteLine();
Console.WriteLine($"Готово!\nФайл создат по следующему пути: {FilePath}");

void Proces()
{
    for (int i = 1; i < Threads.Length + 1; i++)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name}: {Convert.ToInt32(Thread.CurrentThread.Name) * Threads.Length * i}");
        string output = $"{Thread.CurrentThread.Name}: {Convert.ToInt32(Thread.CurrentThread.Name) * Threads.Length * i}";
        lock (loker)
        {
            File.AppendAllText(FilePath, output + Environment.NewLine);
        }
        
    }
}

