using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.IO;

namespace Flights
{
    public partial class Form1 : Form
    {
        private TcpClient clientSocket;

        public Form1(TcpClient clientSocket)
        {
            InitializeComponent();
            this.clientSocket = clientSocket;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            try
            {
                StreamReader reader = new StreamReader(clientSocket.GetStream());
                StreamWriter writer = new StreamWriter(clientSocket.GetStream());
                writer.WriteLine("Login");
                writer.Flush();
                writer.WriteLine(textBox1.Text);
                writer.Flush();
                writer.WriteLine(textBox2.Text);
                writer.Flush();

                int count = Convert.ToInt32(reader.ReadLine());

                if (count >= 1)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    Form2 fm = new Form2(clientSocket);
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
