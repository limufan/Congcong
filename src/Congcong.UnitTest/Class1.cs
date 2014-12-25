using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Congcong.UnitTest
{
    public class Class1
    {
        [Test]
        public void Test1()
        {
            byte[] bytes = File.ReadAllBytes(@"C:\Users\Administrator\Desktop\momo_234.apk");
            string json = JsonConvert.SerializeObject(bytes);
            File.WriteAllText(@"C:\Users\Administrator\Desktop\2014030214304636992.txt", json);
        }
    }
}
