using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using ConsoleApp1.ClientHandle;
using ConsoleApp1.repo;
using ConsoleApp1.services;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        private static FlightRepo repo;
        private static FlightService serv;

        static void Main(string[] args)
        {
            IPAddress adresaLocala = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(adresaLocala,6500);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            repo = new FlightRepo();
            serv = new FlightService(repo);

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                ClientAdministrator client = new ClientAdministrator();
                client.startClient(clientSocket, Convert.ToString(counter),serv);
            }
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }
    }
}
