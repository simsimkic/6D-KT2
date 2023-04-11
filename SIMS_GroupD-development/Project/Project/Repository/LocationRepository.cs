using System;
using Project.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;
using Project.Observer;
using System.IO;

namespace Project.Repository
{
    public class LocationRepository: ISubject
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> serializer;
        private readonly List<IObserver> _observers;

        private List<Location> locations;

        public LocationRepository()
        {
            serializer = new Serializer<Location>();
            _observers = new List<IObserver>();
            locations = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, locations);

        }

        private int GenerateId()
        {
            if (locations.Count == 0) return 0;
            return locations[locations.Count - 1].Id + 1;
        }

        public Location Add(Location location)
        {
            location.Id = GenerateId();
            locations.Add(location);
            SaveInFile();
            NotifyObservers();
            return location;
        }

        public Location Update(Location location)
        {
            Location oldLocation = GetLocationById(location.Id);
            if (oldLocation == null) return null;

            oldLocation.City = oldLocation.City;
            oldLocation.Country = location.Country;


            SaveInFile();
            return oldLocation;
        }

        public Location Remove(int id)
        {
            Location location = GetLocationById(id);
            if (location == null) return null;

            locations.Remove(location);
            SaveInFile();
            return location;
        }

        public Location GetLocationById(int id)
        {
            return locations.Find(v => v.Id == id);
        }

        public string GetCountryById(int id)
        { 
            Location location = GetLocationById(id);
            return location.Country;

        }

        public string GetCityById(int id)
        {
            Location location = GetLocationById(id);
            return location.City;
        }

        public string[] GetAllCountries()
        {
            StreamReader countrySource = new StreamReader(@"../../../Resources/Data/country.csv");
            string content = countrySource.ReadToEnd();
            string[] country = content.Split('|');

            return country;
        }

        public string[] GetAppropriateCities(string country)
        {
            string[] cities = { };
            StreamReader citySource = new StreamReader(@"../../../Resources/Data/city.csv");
            string line;

            while ((line = citySource.ReadLine()) != null)
            {

                string[] couple = line.Split('|');
                if (couple[0] == country)
                {
                    cities = couple[1].Split(';');
                }
            }

            return cities;
        }

        public List<Location> GetAllLocations()
        {
            return locations;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
