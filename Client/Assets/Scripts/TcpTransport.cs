using System;
using System.Net;
using System.Net.Sockets;

public class TcpTransport
{
    public static int DataBufferSize = 4096;

    private TcpClient _socket;
    private NetworkStream _stream;
    private byte[] _receiveBuffer;
    private readonly string _ip;
    private readonly int _port;

    public TcpTransport(string ip, int port)
    {
        _ip = ip;
        _port = port;
    }

    public void Connect()
    {
        _socket = new TcpClient()
        {
            ReceiveBufferSize = DataBufferSize,
            SendBufferSize = DataBufferSize
        };

        _receiveBuffer = new byte[DataBufferSize];

        _socket.BeginConnect(_ip, _port, ConnectCallback, _socket);
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        _socket.EndConnect(ar);

        if (!_socket.Connected)
        {
            return;
        }

        _stream = _socket.GetStream();
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