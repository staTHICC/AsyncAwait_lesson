using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace HttpWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 7632;

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(ipEndPoint);

            socket.Listen(1);

            while (true)
            {
                Socket client = socket.Accept();

                Console.WriteLine("OK");

                byte[] bytes = new byte[1024];
                int count = client.Receive(bytes);
                string response = Encoding.UTF8.GetString(bytes, index: 0, count: count);

                Console.WriteLine("Сервер получил сообщение из браузера");
                Console.WriteLine(response);

                //отправляем ответ клиенту
                string body = "<html><h1>Hello<h1></html>";

                string init = "HTTP/1.1 200 OK\n";

                string headers =
                    "Content-Type: text/html\n" +
                    "Content-Length: " + body.Length + "\n";

                string httpMessage = init + headers + "\n" + body;

                byte[] bytesHttpMessage = Encoding.UTF8.GetBytes(httpMessage);
                client.Send(bytesHttpMessage);

                Console.WriteLine("Отправлено сообщение клиенту!");

                Console.ReadLine();
            }
        }
    }
}
