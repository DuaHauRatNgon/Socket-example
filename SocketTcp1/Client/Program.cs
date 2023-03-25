using System.Net.Sockets;
using System.Text;

namespace Client {
    internal class Program {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 1234;

        static ASCIIEncoding encoding = new ASCIIEncoding();

        public static void Main() {

            Console.OutputEncoding = Encoding.UTF8;

            TcpClient client = new TcpClient();

            // 1. connect
            client.Connect("127.0.0.1", PORT_NUMBER);
            Stream stream = client.GetStream();

            Console.WriteLine("Kết nối tới server");
            Console.Write("Nhập vào: ");

            string str = Console.ReadLine();

            // 2. send
            byte[] data = encoding.GetBytes(str);

            stream.Write(data, 0, data.Length);

            // 3. receive
            data = new byte[BUFFER_SIZE];
            stream.Read(data, 0, BUFFER_SIZE);

            Console.WriteLine(encoding.GetString(data));

            // 4. Close
            stream.Close();
            client.Close();

            Console.ReadKey();
        }
    }
}