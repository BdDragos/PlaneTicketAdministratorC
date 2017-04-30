using ConsoleApp1.model;
using ConsoleApp1.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.services
{
    public class FlightService
    {
        private FlightRepo repo;

        public FlightService(FlightRepo frep)
        {
            this.repo = frep;
        }

        public FlightService()
        {

        }
        public List<Flight> getAllFlights()
        {
            List<Flight> rez = repo.getAll();
            return rez;

        }

        public int buyFlight(int idf, String client, int noticket, String address)
    {
            Flight cop = repo.findById(idf);
            int nrorig = cop.Freeseat;
            if (nrorig < noticket)
            {
                return 0;
            }
            repo.updateFlight(cop, client, noticket, address);

            cop = repo.findById(idf);
            if (cop.Freeseat == nrorig - noticket)
            {
                return 1;
            }
            else
                return 0;
        }

        public void deleteFlight(Flight c)
        {
            repo.deleteFlight(c.Id);
        }
        public List<Flight> findByDestinationAndDate(String dest, string dat)
        {
            List<Flight> ret = repo.findByDestinationAndDate(dest, dat);
            return ret;
        }

        public List<Flight> findByDestination(String dest)
        {
            List<Flight> ret = repo.findByDestination(dest);
            return ret;
        }

        public List<Flight> findByDate(string dat)
        {
            List<Flight> ret = repo.findByDate(dat);
            return ret;
        }

        public int LoginCheck(String usern, String passw)
        {
            int count = repo.LoginCheck(usern, passw);
            return count;
        }

    }
}
