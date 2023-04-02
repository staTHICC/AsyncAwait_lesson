using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

                Thread thread = new Thread(WorkWithClient);
                thread.Start();

                //WorkWithClient(client);
            }
        }

        public static void WorkWithClient(object clientObj)
        {
            while (true)
            {
                Socket client = clientObj as Socket;

                byte[] bytes = new byte[1024];
                int count = client.Receive(bytes);

                if (count != 0)
                {
                    string response = Encoding.UTF8.GetString(bytes, index: 0, count: count);

                    // GET /index HTTP/1.1
                    string initClient = response.Split('\r')[0];

                    string initClientMethod = initClient.Split(' ')[0];
                    string initClientResource = initClient.Split(' ')[1];

                    initClientResource = initClientResource.Substring(1);

                    // /math?a=5&b=10
                    // /math
                    // /?a=5&b=10 -> x

                    if (initClientResource.Contains('?')) // /math?a=5&b=10
                    {
                        string[] mixRequests = initClientResource.Split('?');

                        initClientResource = mixRequests[0];

                        //a=&b=
                        string initClientParams = mixRequests[1]; // a=5&b=10

                        try
                        {
                            int a = int.Parse(initClientParams.Split('&')[0].Split('=')[1]);
                            int b = int.Parse(initClientParams.Split('&')[1].Split('=')[1]);

                            int sum = a + b;

                            //встраивание данных в шаблон
                            Page.Math = Page.MathTemplate.Replace("{sum}", sum.ToString()).Replace("{a}", a.ToString()).
                                Replace("{b}", b.ToString());
                        }
                        catch
                        {

                        }
                    }

                    Console.WriteLine("Сервер получил сообщение из браузера");
                    Console.WriteLine(response);

                    //отправляем ответ клиенту
                    string body = Page.Error;

                    if (initClientResource == "index")
                    {
                        body = Page.Index;
                    }
                    else if (initClientResource == "info")
                    {
                        body = Page.Info;
                    }
                    else if (initClientResource == "math")
                    {
                        body = Page.Math;
                    }
                    else if (initClientResource == "input")
                    {
                        body = Page.Input;
                    }

                    string init = "HTTP/1.1 200 OK\n";

                    string headers =
                        "Content-Type: text/html\n" +
                        "Content-Length: " + body.Length + "\n";

                    string httpMessage = init + headers + "\n" + body;

                    byte[] bytesHttpMessage = Encoding.UTF8.GetBytes(httpMessage);
                    client.Send(bytesHttpMessage);

                    Console.WriteLine("Отправлено сообщение клиенту!");
                }                
            }
        }
    }
}
