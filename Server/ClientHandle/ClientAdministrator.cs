using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using ConsoleApp1.repo;
using ConsoleApp1.services;
using System.IO;
using ConsoleApp1.model;

namespace ConsoleApp1.ClientHandle
{
    public class ClientAdministrator
    {
        StreamReader reader;
        StreamWriter writer;

        TcpClient clientSocket;
        string clNo;
        Thread ctThread;
        FlightService serv;
        
        public void startClient(TcpClient inClientSocket, string clineNo, FlightService serv)
        {
            this.serv = serv;
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.reader = new StreamReader(clientSocket.GetStream());
            this.writer = new StreamWriter(clientSocket.GetStream());
            this.ctThread = new Thread(Listen);
            this.writer.AutoFlush = true;
            ctThread.Start();
        }

        private void Listen()
        {
            try
            {
                String dataFromClient = null;
                while ((true))
                {
                    dataFromClient = reader.ReadLine();
                    if (dataFromClient == "Final") break;
                    if (dataFromClient.CompareTo("Login") == 0)
                    {
                        String user = reader.ReadLine();
                        String pass = reader.ReadLine();
                        int count = serv.LoginCheck(user, pass);
                        writer.WriteLine(count);

                    }
                    if (dataFromClient.CompareTo("Show") == 0)
                    {
                        List<Flight> lista = new List<Flight>();
                        String bigString = "";
                        lista.Clear();
                        lista = serv.getAllFlights();

                        foreach (Flight fb in lista)
                            bigString = bigString + "-" + fb.Id + "," + fb.Destination + "," + fb.Airport + "," + fb.Freeseat + "," + fb.Datehour;

                        writer.WriteLine(bigString);
                    }
                    if (dataFromClient.CompareTo("SearchAll") == 0)
                    {
                        String destin = reader.ReadLine();
                        String depart = reader.ReadLine();
                        List<Flight> rez;

                        rez = serv.findByDestinationAndDate(destin, depart);

                        String bigString = "";
                        foreach (Flight i in rez)
                            bigString = bigString + "-" + i.Id + "," + i.Destination + "," + i.Airport + "," + i.Freeseat + "," + i.Datehour;
                        writer.WriteLine(bigString);
                    }
                    if (dataFromClient.CompareTo("SearchDep") == 0)
                    {
                        String depart = reader.ReadLine();
                        List<Flight> rez;

                        rez = serv.findByDate(depart);

                        String bigString = "";
                        foreach (Flight i in rez)
                            bigString = bigString + "-" + i.Id + "," + i.Destination + "," + i.Airport + "," + i.Freeseat + "," + i.Datehour;
                        writer.WriteLine(bigString);
                    }
                    if (dataFromClient.CompareTo("SearchDest") == 0)
                    {
                        String dest = reader.ReadLine();
                        List<Flight> rez;

                        rez = serv.findByDestination(dest);

                        String bigString = "";
                        foreach (Flight i in rez)
                            bigString = bigString + "-" + i.Id + "," + i.Destination + "," + i.Airport + "," + i.Freeseat + "," + i.Datehour;
                        writer.WriteLine(bigString);
                    }
                    if (dataFromClient.CompareTo("Buy") == 0)
                    {
                        String id = reader.ReadLine();
                        String clientname = reader.ReadLine();
                        String nrtickets = reader.ReadLine();
                        String address = reader.ReadLine();
                        int ok = serv.buyFlight(Convert.ToInt32(id), clientname, Convert.ToInt32(nrtickets), address);
                        if (ok == 1)
                        {
                            writer.WriteLine("Primit");
                        }
                        else
                        {
                            writer.WriteLine("Invalid");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Un client s-a delogat");
                clientSocket.Close();
            }
        }
    }
}
