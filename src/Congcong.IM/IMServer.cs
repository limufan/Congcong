using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Congcong.IM
{
    public class IMServer
    {
        private Dictionary<int, IMClient> _imClientDic;

        public IMServer()
        {
            this._imClientDic = new Dictionary<int, IMClient>();
        }

        public void Start()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("192.168.1.10"), 1234);
            tcpListener.Start();
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                IMClient imClient = new IMClient(this, client);
                imClient.Signed += Client_Signed;
                Thread t = new Thread(imClient.Start);
                t.Start();
            }
        }

        void Client_Signed(IMClient imClient)
        {
            this._imClientDic.Add(imClient.Id, imClient);
        }

        public void SendMessage(Message msg)
        {
            if(this._imClientDic.ContainsKey(msg.receiverId))
            {
                IMClient imClient = this._imClientDic[msg.receiverId];
                NetworkStream stream = imClient.TcpClient.GetStream();
                string msgJson = JsonConvert.SerializeObject(msg);
                byte[] msgBytes = Encoding.UTF8.GetBytes(msgJson);
                stream.Write(msgBytes, 0, msgBytes.Length);
            }
        }

        public void Offline(IMClient imClient)
        {
            this._imClientDic.Remove(imClient.Id);
            Thread.CurrentThread.Abort();
        }
    }
}
