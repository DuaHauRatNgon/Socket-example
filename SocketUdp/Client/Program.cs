using System;
using System.Net; 
using System.Net.Sockets;
using System.Text; 

namespace Client {
    internal class Program {
        private static void Main(string[] args) {
            Console.Title = "Udp Client";
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Server IP address: ");
            var serverIp = IPAddress.Parse(Console.ReadLine());

            Console.Write("Server port: ");
            var serverPort = int.Parse(Console.ReadLine());

            var serverEndpoint = new IPEndPoint(serverIp, serverPort);

            var size = 1024; 
            var receiveBuffer = new byte[size];          

            while (true) {
                var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);

                Console.Write("Nhap text: ");
                var text = Console.ReadLine();
                var sendBuffer = Encoding.ASCII.GetBytes(text);

                socket.SendTo(sendBuffer, serverEndpoint);
                // endpoint này chỉ dùng khi nhận dữ liệu
                EndPoint dummyEndpoint = new IPEndPoint(IPAddress.Any, 0);
                // nhận mảng byte từ dịch vụ Udp và lưu vào bộ đệm
                // biến dummyEndpoint có nhiệm vụ lưu lại địa chỉ của tiến trình nguồn
                // tuy nhiên, ở đây chúng ta đã biết tiến trình nguồn là Server
                // do đó dummyEndpoint không có giá trị sử dụng 

                var length = socket.ReceiveFrom(receiveBuffer, ref dummyEndpoint);
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Ket qua: {result}");

                // xóa bộ đệm 
                Array.Clear(receiveBuffer, 0, size);
                // đóng socket và giải phóng tài nguyên
                socket.Close();
            }
        }
    }
}