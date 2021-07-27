using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Server
    {
        private int _maxPlayers;
        private int _port;
        private TcpListener _tcpListener;
        private Dictionary<int, Client> _clients = new Dictionary<int, Client>();

        public Server(int maxPlayers, int port)
        {
            _maxPlayers = maxPlayers;
            _port = port;
        }

        public void Start()
        {
            Console.WriteLine($"Starting server...");

            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(TcpConnectCallback, null);

            Console.WriteLine($"Server started on {_port}.");
        }

        private void TcpConnectCallback(IAsyncResult ar)
        {
            TcpClient tcpClient = _tcpListener.EndAcceptTcpClient(ar);
            _tcpListener.BeginAcceptTcpClient(TcpConnectCallback, null);

            Console.WriteLine($"Incoming connection from {tcpClient.Client.RemoteEndPoint}...");

            if (ServerIsFull())
            {
                Console.WriteLine($"{tcpClient.Client.RemoteEndPoint} failed to connect: Server is full!");
                return;
            }

            int nextClientId = NextClientId();
            Client client = new(nextClientId);
            client.Tcp.Connect(tcpClient);
            _clients[nextClientId] = client;
        }

        private bool ServerIsFull()
        {
            return _clients.Count >= _maxPlayers;
        }

        private int NextClientId()
        {
            return _clients.Count + 1;
        }
    }
}