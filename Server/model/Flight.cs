using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.model
{
    public class Flight
    {
        private int id;
        private string destination;
        private string airport;
        private int freeseat;
        private string datehour;

        public Flight(int idf,string dest,string air,int free,string dateh)
        {
            this.id = idf;
            this.destination = dest;
            this.airport = air;
            this.freeseat = free;
            this.datehour = dateh;
        }
        public int Id { get => id; set => id = value; }
        public string Destination { get => destination; set => destination = value; }
        public string Airport { get => airport; set => airport = value; }
        public int Freeseat { get => freeseat; set => freeseat = value; }
        public string Datehour { get => datehour; set => datehour = value; }

        public override string ToString()
        {
            return id.ToString() + " " + destination.ToString() + " " + airport.ToString() + " " + freeseat.ToString() + " " + Datehour.ToString();
        }
    }
}
