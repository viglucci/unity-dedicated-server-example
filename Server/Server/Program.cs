using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";

            Server server = new Server(50, 26950);
            server.Start();

            Console.ReadKey();
        }
    }
}