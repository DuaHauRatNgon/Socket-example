using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {
    internal class Program {
        private static void Main(string[] args) {
            Console.Title = "Udp Server";
            Console.OutputEncoding = Encoding.UTF8;

            var localIp = IPAddress.Any;    // giá trị Any của IPAddress tương ứng với Ip của tất cả các giao diện mạng trên máy
            var localPort = 3456;           // tiến trình server sẽ sử dụng cổng 1308
            var localEndPoint = new IPEndPoint(localIp, localPort);     // biến này sẽ chứa "địa chỉ" của tiến trình server trên mạng

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(localEndPoint);
            Console.WriteLine($"Local socket được gắn với {localEndPoint}. Đang chờ đợi request tới ...");

            var size = 1024;
            var receiveBuffer = new byte[size];

            while (true) {
                // biến này về sau sẽ chứa địa chỉ của tiến trình client nào gửi gói tin tới
                EndPoint remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);
                // khi nhận được gói tin nào sẽ lưu lại địa chỉ của tiến trình client
                var length = socket.ReceiveFrom(receiveBuffer, ref remoteEndpoint);
                var text = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                Console.WriteLine($"Text nhận được từ {remoteEndpoint}: {text}");

                var result = text.ToUpper();
                var sendBuffer = Encoding.ASCII.GetBytes(result);
                socket.SendTo(sendBuffer, remoteEndpoint);
                Array.Clear(receiveBuffer, 0, size);
            }
        }
    }
}