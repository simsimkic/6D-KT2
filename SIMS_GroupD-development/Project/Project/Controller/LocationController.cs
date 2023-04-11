using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class LocationController
    {
        LocationRepository locationRepository { get; set; }

        public LocationController()
        {
            locationRepository = new LocationRepository();
        }

        public Location Create(string city, string country)
        {
            Location location = new Location(city,country);

            Location createdLocation = locationRepository.Add(location);
            return createdLocation;

        }

        public void Subscribe(IObserver observer)
        {
            locationRepository.Subscribe(observer);
        }

        public Location GetById(int id)
        {
            return locationRepository.GetLocationById(id);
        }

        public string GetCountryById(int id)
        {
           return locationRepository.GetCountryById(id);
        }

        public string GetCityById(int id)
        {
            return locationRepository.GetCityById(id);
        }

        public string[] GetAllCountries()
        {
            return locationRepository.GetAllCountries();
        }

        public string[] GetAppropriateCities(string country)
        {
            return locationRepository.GetAppropriateCities(country);
        }
    }
}
