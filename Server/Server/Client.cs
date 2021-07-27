namespace Server
{
    public class Client
    {

        private int _id;

        public TcpTransport TcpTransport { get; }

        public Client(int id)
        {
            TcpTransport = new TcpTransport(id);
            _id = id;
        }
    }
}