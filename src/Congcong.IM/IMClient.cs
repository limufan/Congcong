using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Congcong.IM
{
    public class IMClient
    {
        public TcpClient TcpClient { set; get; }
        IMServer _imServer;
        public IMClient(IMServer imServer, TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
            this._imServer = imServer;
        }

        public int Id { private set; get; }

        public void Start()
        {
            this.SignIn();

            byte[] bytes = new byte[256];
            string data = null;

            while (true)
            {
                try
                {
                    int i;
                    NetworkStream stream = this.TcpClient.GetStream();
                    i = stream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    Message msg = JsonConvert.DeserializeObject<Message>(data);
                    this.SendMessage(msg);
                }
                catch
                {
                    this._imServer.Offline(this);
                }
            }
        }

        public event TEventHandler<IMClient> Signed;

        public void SignIn()
        {
            NetworkStream stream = this.TcpClient.GetStream();
            byte[] bytes = new Byte[256];
            int i = stream.Read(bytes, 0, bytes.Length);
            string data = Encoding.UTF8.GetString(bytes, 0, i);
            LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(data);
            this.Id = loginModel.clientId;
            if (this.Signed != null)
            {
                this.Signed(this);
            }
        }

        public void Stop()
        {

        }

        public void SendMessage(Message msg)
        {
            this._imServer.SendMessage(msg);
        }
    }
}
