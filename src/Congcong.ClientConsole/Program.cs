using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Congcong.ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("192.168.1.10", 1234);

            IMClient imClient = InitClientFromConsole(tcpClient);

            while (true)
            {
                string[] message = Console.ReadLine().Split(' ');
                imClient.Send(int.Parse(message[0]), message[1]);
            }
        }

        private static IMClient Init1000Client(TcpClient tcpClient)
        {
            IMClient imClient = null;
            for (int clientId = 100; clientId < 1100; clientId++)
            {
                imClient = new IMClient(clientId);
                Thread t1 = new Thread(imClient.Receive);
                t1.Start();
            }
            return imClient;
        }

        private static IMClient InitClientFromConsole(TcpClient tcpClient)
        {
            int clientId = int.Parse(Console.ReadLine());

            IMClient imClient = new IMClient(clientId);
            Thread t1 = new Thread(imClient.Receive);
            t1.Start();
            return imClient;
        }
    }
}
