using System.Runtime.InteropServices;

namespace TaskExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";

            Console.WriteLine($"Поток {mainThread.Name}, ID: {mainThread.GetHashCode()}");

            Message message = new Message();
            await message.PrintMessageAsync();


            for (int i = 0; i < 5; i++)
            {
                Random r = new Random();

                Console.WriteLine($"Выполняется поток: {mainThread.Name}");
                Thread.Sleep(r.Next(500, 3000));

            }

            Console.WriteLine($"\nПоток: {mainThread.Name}, ID: {mainThread.GetHashCode()}. Конец работы");

        }

    }

    class Message
    {
        private Thread? secondThread;

        private void PrintMessage()
        {
            Random r;
            for (int i = 1; i <= 10; i++)
            {
                r = new Random();
                Console.WriteLine($"Сообщение #{i} {secondThread.Name} В классе Message");
                Thread.Sleep(r.Next(500, 3000));
            }
            Console.WriteLine($"\nПоток: {secondThread.Name}, ID: {secondThread.GetHashCode()}. Конец работы");

        }

        public async Task PrintMessageAsync()
        {
            await Task.Run(() =>
            {
                secondThread = new Thread(PrintMessage);
                secondThread.Name = "Поток Message1";
                secondThread.Start();
            });

        }
    }
}