using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server {
    internal class Program {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 1234;

        static ASCIIEncoding encoding = new ASCIIEncoding();

        public static void Main() {
            Console.OutputEncoding = Encoding.UTF8;

            IPAddress address = IPAddress.Parse("127.0.0.1");

            TcpListener listener = new TcpListener(address, PORT_NUMBER);

            // 1. listen
            listener.Start();

            Console.WriteLine("Server bắt đầu kết nối tới IEP là:  " + listener.LocalEndpoint);
            Console.WriteLine("Đang chờ kết nối ...");

            Socket socket = listener.AcceptSocket();
            Console.WriteLine("Kết nối nhận được từ " + socket.RemoteEndPoint);

            // 2. receive
            byte[] data = new byte[BUFFER_SIZE];
            socket.Receive(data);

            string str = encoding.GetString(data);

            // 3. send
            socket.Send(encoding.GetBytes(str.ToUpper()));


            // 4. close
            socket.Close();
            listener.Stop();


            Console.ReadKey();
        }
    }
}