using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using Client;
using System.Diagnostics;
using System.Text.Json;

namespace Server
{
    class Server1
    {
        public string ipAddr;
        private int port;
        private IPEndPoint ipPoint;
        public Socket socket;
        public Socket socketclient;
        public List<Client1> clients;
        UserInfo info = new UserInfo();
        public Server1()
        {
            this.ipAddr = "127.0.0.1";
            this.port = 8000;
            this.ipPoint = new IPEndPoint(IPAddress.Parse(ipAddr), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.clients = new List<Client1>();
        }
        public void StartServer()
        {
            try
            {
                this.socket.Bind(ipPoint);
                this.socket.Listen(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void Connects()
        {
            while (true)
            {
                clients.Add(new Client1(this.socket.Accept()));
                SendMsg1("Connect", clients.Count - 1);
            }
        }
        public string GetMsg()
        {
            lock (this.clients)
            {
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];
                for (int i = 0; i < clients.Count; i++)
                    do
                    {
                        bytes = clients[i].socket.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (clients[i].socket.Available > 0);
                UserInfo tmp = JsonSerializer.Deserialize<UserInfo>(builder.ToString());
                return builder.ToString();
            }
        }
        public void SendMsg1(string message, int index)
        {
            clients[index].socket.Send(Encoding.Unicode.GetBytes(message));
        }
        public void SendMsg(string message)
        {
            lock (this.clients)
            {
                byte[] data = new byte[256];
                if (clients.Count != 0)
                    for (int i = 0; i < clients.Count; i++)
                        clients[i].socket.Send(Encoding.Unicode.GetBytes(message));
            }
        }
    }
}