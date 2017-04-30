using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
namespace Flights
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TcpClient clientSocket = new TcpClient();

            clientSocket.Connect("127.0.0.1", 6500);
            MessageBox.Show("Client Socket Program - Server Connected ...");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(clientSocket));

        }
    }
}
