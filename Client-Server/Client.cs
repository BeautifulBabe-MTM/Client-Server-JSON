using Microsoft.Win32;
using System;
using System.Linq;
using System.Text.Json;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            Client1 client = new Client1();
            try
            {
                client.Connect();
                UserInfo info = new UserInfo();
                string json = JsonSerializer.Serialize<UserInfo>(info);
                client.SendMsg(json);
                Console.WriteLine(client.GetMsg());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}