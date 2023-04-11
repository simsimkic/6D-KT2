using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class OwnerController
    {
        public Owner Owner { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public LocationRepository LocationRepository { get; set; }

        public List<Location> Locations { get; set; }
        public AccommodationImageRepository AccommodationImageRepository { get; set; }


        public OwnerController()
        {
            Owner = new Owner();
            AccommodationRepository = new AccommodationRepository();
            LocationRepository = new LocationRepository();
            Locations = new List<Location>();
            AccommodationImageRepository = new AccommodationImageRepository();
            LinkOwnerAccommodation();
            FillLocationsList();
        }
        public OwnerController(User u)
        {
            Owner = new Owner(u);
            AccommodationRepository = new AccommodationRepository();
            LocationRepository = new LocationRepository();
            Locations = new List<Location>();
            AccommodationImageRepository = new AccommodationImageRepository();
            LinkOwnerAccommodation();
            FillLocationsList();
        }

        public List<Location> GetLocations()
        {
            return Locations;
        }
        private void LinkOwnerAccommodation()
        {
            foreach(Accommodation accommodation in AccommodationRepository.GetAllAccommodations())
            {
                if(accommodation.OwnerId == Owner.User.Id)
                {
                    Owner.Accommodations.Add(accommodation);
                }
            }
        }

        private void FillLocationsList()
        {
            foreach (var location in LocationRepository.GetAllLocations())
            {
                Locations.Add(location);
            }

        }

    }
}