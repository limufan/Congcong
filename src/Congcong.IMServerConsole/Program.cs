using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Congcong.IM;

namespace Congcong.IMServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IMServer imServer = new IMServer();
            imServer.Start();
        }
    }
}
