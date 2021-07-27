using System;
using System.Net.Sockets;

namespace Server
{
    public class Tcp
    {
        private TcpClient _socket;
        private readonly int _id;
        private NetworkStream _stream;
        private byte[] _receiveBuffer;

        public Tcp(int id)
        {
            _id = id;
        }

        public void Connect(TcpClient socket)
        {
            _socket = socket;
            _socket.ReceiveBufferSize = Client.DataBufferSize;
            _socket.SendBufferSize = Client.DataBufferSize;
            _stream = _socket.GetStream();
            _receiveBuffer = new byte[Client.DataBufferSize];
            _stream.BeginRead(_receiveBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int byteLength = _stream.EndRead(ar);
                if (byteLength <= 0)
                {
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(_receiveBuffer, data, byteLength);

                _stream.BeginRead(_receiveBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}