using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Congcong.ClientConsole
{
    public class IMClient
    {
        public IMClient(int clientId)
        {
            this.TcpClient = new TcpClient();
            this.TcpClient.Connect("192.168.1.10", 1234);
            this.ClientId = clientId;
            this.Login();
        }

        public int ClientId { set; get; }

        public TcpClient TcpClient { set; get; }

        public void Receive()
        {
            byte[] bytes = new Byte[256];
            string data = null;
            while (true)
            {
                int i;
                NetworkStream stream = this.TcpClient.GetStream();
                i = stream.Read(bytes, 0, bytes.Length);
                data = Encoding.UTF8.GetString(bytes, 0, i);
                Message msg = JsonConvert.DeserializeObject<Message>(data);
                Console.WriteLine("{0}: {1}", msg.senderId, msg.content);
            }
        }

        public void Login()
        {
            LoginModel model = new LoginModel { clientId = this.ClientId };
            NetworkStream stream = this.TcpClient.GetStream();
            string msgJson = JsonConvert.SerializeObject(model);
            byte[] msgBytes = Encoding.UTF8.GetBytes(msgJson);
            stream.Write(msgBytes, 0, msgBytes.Length);
        }

        public void Send(int receiverId, string content)
        {
            Message msg = new Message { receiverId = receiverId, senderId = this.ClientId, sendTime = DateTime.Now, content = content };
            NetworkStream stream = this.TcpClient.GetStream();
            string msgJson = JsonConvert.SerializeObject(msg);
            byte[] msgBytes = Encoding.UTF8.GetBytes(msgJson);
            stream.Write(msgBytes, 0, msgBytes.Length);
        }
    }
}
