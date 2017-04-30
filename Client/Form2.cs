using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using Flights.model;
using System.Diagnostics;
using System.Reflection;

namespace Flights
{
    public partial class Form2 : Form
    {
        Boolean logoutwasclicked;
        List<Flight> flight;
        StreamReader reader;
        StreamWriter writer;
        private TcpClient clientSocket;
        DataTable tableFlight;

        public Form2(TcpClient clientSocket)
        {
            InitializeComponent();
            this.clientSocket = clientSocket;
            this.reader = new StreamReader(clientSocket.GetStream());
            this.writer = new StreamWriter(clientSocket.GetStream());
            this.flight = new List<Flight>();
            this.writer.AutoFlush = true;
            this.tableFlight = new DataTable();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
               .SetValue(this.dataGridView1, true, null);
            getAll();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.dataGridView1.SuspendLayout();
            tableRefresh();
            this.dataGridView1.ResumeLayout(true);
        }

        private void getAll()
        {
            try
            {
                String linie = "Show";
                writer.WriteLine(linie);
                string retur = reader.ReadLine();
                if (!string.IsNullOrEmpty(retur))
                {
                    string[] output = retur.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    flight.Clear();
                    foreach (string s in output)
                    {
                        string[] flyelem = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        int idf = Convert.ToInt32(flyelem[0]);
                        string destf = flyelem[1];
                        string airf = flyelem[2];
                        int frees = Convert.ToInt32(flyelem[3]);
                        string datef = flyelem[4];
                        Flight b = new Flight(idf, destf, airf, frees, datef);
                        flight.Add(b);
                    }

                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                    dataGridView1.DataSource = flight;

                    dataGridView3.DataSource = null;
                    dataGridView3.Refresh();
                    dataGridView3.DataSource = flight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tableRefresh()
        {
            try
            {
                String linie = "Show";
                writer.WriteLine(linie);
                string retur = reader.ReadLine();
                if (!string.IsNullOrEmpty(retur))
                {
                    string[] output = retur.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    flight.Clear();
                    foreach (string s in output)
                    {
                        string[] flyelem = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        int idf = Convert.ToInt32(flyelem[0]);
                        string destf = flyelem[1];
                        string airf = flyelem[2];
                        int frees = Convert.ToInt32(flyelem[3]);
                        string datef = flyelem[4];
                        Flight b = new Flight(idf, destf, airf, frees, datef);
                        flight.Add(b);
                    }

                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                    dataGridView1.DataSource = flight;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            writer.WriteLine("Final");
            clientSocket.Close();
            logoutwasclicked = true;
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logoutwasclicked == false)
            {
                writer.WriteLine("Final");
                clientSocket.Close();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            String destin = textBox1.Text;
            String depar = textBox2.Text;
            if (string.IsNullOrEmpty(destin) && string.IsNullOrEmpty(depar))
            {
                MessageBox.Show("Fill one of the fields with the necessary information");
            }
            else if (string.IsNullOrEmpty(destin) && !string.IsNullOrEmpty(depar))
            {
                try
                {
                    writer.WriteLine("SearchDep");
                    writer.WriteLine(depar);
                    string retur = reader.ReadLine();
                    if (!string.IsNullOrEmpty(retur))
                    {
                        List<Flight> zbor = new List<Flight>();
                        string[] output = retur.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in output)
                        {
                            string[] flyelem = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            int idf = Convert.ToInt32(flyelem[0]);
                            string destf = flyelem[1];
                            string airf = flyelem[2];
                            int frees = Convert.ToInt32(flyelem[3]);
                            string datef = flyelem[4];
                            Flight b = new Flight(idf, destf, airf, frees, datef);
                            zbor.Add(b);
                        }

                        dataGridView3.DataSource = null;
                        dataGridView3.Refresh();
                        dataGridView3.DataSource = zbor;

                        dataGridView2.DataSource = null;
                        dataGridView2.Refresh();
                        dataGridView2.DataSource = zbor;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (!string.IsNullOrEmpty(destin) && string.IsNullOrEmpty(depar))
            {
                try
                {
                    writer.WriteLine("SearchDest");
                    writer.WriteLine(destin);
                    string retur = reader.ReadLine();
                    if (!string.IsNullOrEmpty(retur))
                    {
                        List<Flight> zbor = new List<Flight>();

                        string[] output = retur.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in output)
                        {
                            string[] flyelem = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            int idf = Convert.ToInt32(flyelem[0]);
                            string destf = flyelem[1];
                            string airf = flyelem[2];
                            int frees = Convert.ToInt32(flyelem[3]);
                            string datef = flyelem[4];
                            Flight b = new Flight(idf, destf, airf, frees, datef);
                            zbor.Add(b);
                        }

                        dataGridView3.DataSource = null;
                        dataGridView3.Refresh();
                        dataGridView3.DataSource = zbor;

                        dataGridView2.DataSource = null;
                        dataGridView2.Refresh();
                        dataGridView2.DataSource = zbor;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            else if (!string.IsNullOrEmpty(destin) && !string.IsNullOrEmpty(depar))
            {
                try
                {
                    writer.WriteLine("SearchAll");
                    writer.WriteLine(destin);
                    writer.WriteLine(depar);
                    string retur = reader.ReadLine();
                    if (!string.IsNullOrEmpty(retur))
                    {
                        List<Flight> zbor = new List<Flight>();

                        string[] output = retur.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in output)
                        {
                            string[] flyelem = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            int idf = Convert.ToInt32(flyelem[0]);
                            string destf = flyelem[1];
                            string airf = flyelem[2];
                            int frees = Convert.ToInt32(flyelem[3]);
                            string datef = flyelem[4];
                            Flight b = new Flight(idf, destf, airf, frees, datef);
                            zbor.Add(b);
                        }

                        dataGridView3.DataSource = null;
                        dataGridView3.Refresh();
                        dataGridView3.DataSource = zbor;

                        dataGridView2.DataSource = null;
                        dataGridView2.Refresh();
                        dataGridView2.DataSource = zbor;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BuyTicket_Click(object sender, EventArgs e)
        {
            String client = textBox3.Text;
            String nrticket = textBox4.Text;
            String address = textBox5.Text;
            Flight fly = new Flight(Convert.ToInt32(this.dataGridView3.CurrentRow.Cells[0].Value), this.dataGridView3.CurrentRow.Cells[1].Value.ToString(), this.dataGridView3.CurrentRow.Cells[2].Value.ToString(), Convert.ToInt32(this.dataGridView3.CurrentRow.Cells[3].Value), this.dataGridView3.CurrentRow.Cells[4].Value.ToString());
            String id = fly.Id.ToString();
            if (string.IsNullOrEmpty(client) || string.IsNullOrEmpty(nrticket) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Fill all the fields with the necessary information");
            }
            else
                try
                {
                    writer.WriteLine("Buy");
                    writer.WriteLine(id);
                    writer.WriteLine(client);
                    writer.WriteLine(nrticket);
                    writer.WriteLine(address);
                    if (reader.ReadLine().Equals("Primit"))
                    {
                        MessageBox.Show("The ticket was bought with success");
                        getAll();
                    }
                    else
                    {
                        MessageBox.Show("The ticket couldn't be bought");
                    }
                }
                catch (IOException io)
                {
                    MessageBox.Show(io.ToString());
                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            getAll();
        }


    }
}
