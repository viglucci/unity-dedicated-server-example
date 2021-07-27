using System;
using System.Net.Sockets;

namespace Server
{
    public class TcpTransport
    {
        private static int DataBufferSize = 4096;
        
        private readonly int _id;
        private TcpClient _socket;
        private NetworkStream _stream;
        private byte[] _receiveBuffer;

        public TcpTransport(int id)
        {
            _id = id;
        }

        public void Connect(TcpClient socket)
        {
            _socket = socket;
            _socket.ReceiveBufferSize = DataBufferSize;
            _socket.SendBufferSize = DataBufferSize;
            _stream = _socket.GetStream();
            _receiveBuffer = new byte[DataBufferSize];
            _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
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

                _stream.BeginRead(_receiveBuffer, 0, DataBufferSize, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}