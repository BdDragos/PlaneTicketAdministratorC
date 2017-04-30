using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ConsoleApp1.model;
using System.Data;

namespace ConsoleApp1.repo
{
    public class FlightRepo
    {
        //conexiunea la baza de date
        static string connectmysql = "datasource=localhost;port=3306;username=root;password=;database=flights";
        public MySqlConnection conexiune = new MySqlConnection(connectmysql);

        //lista de zboruri
        private List<Flight> flights = new List<Flight>();

        public FlightRepo()
        {
            conexiune.Open();
            flights.Clear();
            MySqlCommand comanda = new MySqlCommand("select * from routes", conexiune);
            MySqlDataReader reader = comanda.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string destination = reader.GetString(1);
                string airport = reader.GetString(2);
                int freeseats = reader.GetInt32(3);
                string datehour = reader.GetString(4);

                Flight flight = new Flight(id, destination, airport, freeseats, datehour);
                flights.Add(flight);
            }
            reader.Close();
            conexiune.Close();
        }

        public List<Flight> getAll()
        {
            return flights;
        }

        public void updateFlight(Flight flight, String client, int noticket, String address)
        {
            conexiune.Open();
            foreach (Flight f in flights)
                if (f.Id == flight.Id)
                    try
                    {
                        int id = f.Id;
                        MySqlCommand comandaUpdate = new MySqlCommand("update routes SET FreeSeats = FreeSeats-@noticket where Id=@id", conexiune);
                        comandaUpdate.Parameters.AddWithValue("@noticket", noticket);
                        comandaUpdate.Parameters.AddWithValue("@id", id);
                        comandaUpdate.ExecuteNonQuery();
                        var obj = flights.FirstOrDefault(x => x.Id == f.Id);
                        if (obj != null)
                            obj.Freeseat = obj.Freeseat - noticket;

                        MySqlCommand comandaInsert = new MySqlCommand("insert into clients (Name,Address,NoTickets,Flight)" + " values (@Name, @Address, @NoTickets, @Flight)",conexiune);
                        comandaInsert.Parameters.AddWithValue("@Name", client);
                        comandaInsert.Parameters.AddWithValue("@Address", address);
                        comandaInsert.Parameters.AddWithValue("@NoTickets", noticket);
                        comandaInsert.Parameters.AddWithValue("@Flight", flight);
                        comandaInsert.ExecuteNonQuery();
                        conexiune.Close();

                    }
                    catch (Exception ex)
                    {
                        conexiune.Close();
                        Console.WriteLine(ex.ToString());
                    }
        }

        public List<Flight> findByDestinationAndDate(string destination, string datehour)
        {
            List<Flight> flig = new List<Flight>();
            conexiune.Open();
            MySqlCommand comanda = new MySqlCommand();
            comanda.Connection = conexiune;
            comanda.CommandType = System.Data.CommandType.Text;
            comanda.CommandText = "select * from routes where Destination = @destination and Date = @datehour";
            comanda.Parameters.AddWithValue("@destination", destination);
            comanda.Parameters.AddWithValue("@datehour", datehour);
            MySqlDataReader reader;
            try
            {
                reader = comanda.ExecuteReader();
                Flight flight;
                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string airport = reader.GetString(2);
                    int freeseat = reader.GetInt32(3);

                    flight = new Flight(id, destination, airport, freeseat, datehour);
                    flig.Add(flight);
                }
                reader.Close();
                conexiune.Close();
                return flig;
            }

            catch (Exception ex)
            {
                conexiune.Close();
                Console.WriteLine(ex.ToString());
            }
            conexiune.Close();
            return null;

        }

        public List<Flight> findByDate(string datehour)
        {
            List<Flight> flig = new List<Flight>();
            conexiune.Open();
            MySqlCommand comanda = new MySqlCommand();
            comanda.Connection = conexiune;
            comanda.CommandType = System.Data.CommandType.Text;
            comanda.CommandText = "select * from routes where Date = @datehour";
            comanda.Parameters.AddWithValue("@datehour", datehour);
            MySqlDataReader reader;
            try
            {
                reader = comanda.ExecuteReader();
                Flight flight;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string destination = reader.GetString(1);
                    string airport = reader.GetString(2);
                    int freeseat = reader.GetInt32(3);

                    flight = new Flight(id, destination, airport, freeseat, datehour);
                    flig.Add(flight);
                }
                reader.Close();
                conexiune.Close();
                return flig;

            }
            catch (Exception ex)
            {
                conexiune.Close();
                Console.WriteLine(ex.ToString());
            }
            conexiune.Close();
            return null;

        }

        public List<Flight> findByDestination(string destination)
        {
            List<Flight> flig = new List<Flight>();
            conexiune.Open();
            MySqlCommand comanda = new MySqlCommand();
            comanda.Connection = conexiune;
            comanda.CommandType = System.Data.CommandType.Text;
            comanda.CommandText = "select * from routes where Destination = @destination";
            comanda.Parameters.AddWithValue("@destination", destination);
            MySqlDataReader reader;
            try
            {
                reader = comanda.ExecuteReader();
                Flight flight;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string airport = reader.GetString(2);
                    int freeseat = reader.GetInt32(3);
                    string datehour = reader.GetString(4);
                    flight = new Flight(id, destination, airport, freeseat, datehour);
                    flig.Add(flight);
                }
                reader.Close();
                conexiune.Close();
                
                return flig;

            }
            catch (Exception ex)
            {
                conexiune.Close();
                Console.WriteLine(ex.ToString());
            }
            conexiune.Close();
            return null;

        }

        public void deleteFlight(int Id)
        {
            conexiune.Open();
            MySqlCommand comanda = new MySqlCommand();
            comanda.Connection = conexiune;
            comanda.CommandType = System.Data.CommandType.Text;
            comanda.CommandText = "delete from routes where Id = @Id";
            comanda.Parameters.AddWithValue("@Id", Id);
            try
            {
                comanda.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conexiune.Close();
                Console.WriteLine(ex.ToString());
            }
            conexiune.Close();
        }

        public Flight findById(int id)
        {
            foreach (Flight f in flights)
                if (f.Id == id)
                    return f;
            return null;
        }

        public int LoginCheck(String usern,String passw)
        {
            MySqlCommand cmd = new MySqlCommand("Select * from users where User=@username and Password=@password", conexiune);
            cmd.Parameters.AddWithValue("@username", usern);
            cmd.Parameters.AddWithValue("@password", passw);
            conexiune.Open();
            MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            conexiune.Close();
            int count = ds.Tables[0].Rows.Count;
            return count;
        }

    }
}

