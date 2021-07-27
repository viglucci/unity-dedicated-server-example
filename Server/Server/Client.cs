using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Client
    {
        public static int DataBufferSize = 4096;
        
        private int _id;

        public Tcp Tcp { get; }

        public Client(int id)
        {
            Tcp = new Tcp(id);
            _id = id;
        }
    }
}