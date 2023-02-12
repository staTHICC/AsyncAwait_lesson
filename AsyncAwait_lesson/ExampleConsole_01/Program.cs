using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ExampleConsole_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //SyncMath();
            //AsyncMath();
            //AsyncWriteBook();
            Task task = AsyncAdd();

            Console.WriteLine("AAAAAAAAAAAAA"); //сделать яичницу

            Console.ReadLine();
        }

        static void SyncMath()
        {
            Math();
        }

        static async Task AsyncMath()   //грядка
        {
            await Task.Run(Math);
            Console.WriteLine("Я всё сделал!");
        }

        static async Task AsyncWriteBook()
        {
            string text = "Начиналась гроза. " +
                " Я подошёл к окну и задёрнул шторы...";
            await Task.Run(() => WriteBook(text));

            Console.WriteLine("Я написал книгу!");
        }

        static async Task<int> AsyncAdd()
        {
            int a = 12;
            int b = 13;

            int res = await Task.Run(() => Add(a,b));

            //Console.WriteLine("RES: " + res);

            return res;
        }
        static int Add(int a, int b)
        {
            Console.WriteLine("Start Add");
            Thread.Sleep(5000);
            Console.WriteLine("Finish Add");
            return a + b;
        }

        static void WriteBook(string text)
        {
            Console.WriteLine("Start WriteBook");
            Thread.Sleep(5000);
            Console.WriteLine("Finish WriteBook");
            Console.WriteLine($"Text: {text}");
        }

        static void Math()
        {
            Console.WriteLine("Start Math");
            Thread.Sleep(5000);
            Console.WriteLine("Finish Math");
        }
    }
}
