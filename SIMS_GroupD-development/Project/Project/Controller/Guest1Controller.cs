using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Project.Model;
using Project.Observer;
using Project.Repository;


namespace Project.Controller
{
    public class Guest1Controller
    {
        public Guest1 Guest { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }
        public AccommodationReservationRepository AccReservationRepository { get; set; }

        public List<Location> AccommodationLocations { get; set; }
        public AccommodationImageRepository ImageRepository { get; set; }

        public Guest1Controller()
        {
            Guest = new Guest1();
            AccommodationRepository = new AccommodationRepository();
            AccReservationRepository = new AccommodationReservationRepository();
            ImageRepository = new AccommodationImageRepository();
            AccommodationLocations = new List<Location>();
            LinkAccommodationsAndImages();
            LinkGuestAndReservations();
            FillAccommodationLocationsList();
        }


        public Guest1Controller(User u)
        {
            Guest = new Guest1(u);
            AccommodationRepository = new AccommodationRepository();
            AccReservationRepository = new AccommodationReservationRepository();
            ImageRepository = new AccommodationImageRepository();
            AccommodationLocations = new List<Location>();
            LinkAccommodationsAndImages();
            LinkGuestAndReservations();
            FillAccommodationLocationsList();

        }

        private void LinkGuestAndReservations()
        {
            foreach (var reservation in AccReservationRepository.GetAllReservations())
            {
                if (reservation.GuestId == Guest.User.Id)
                {
                    Guest.Reservations.Add(reservation);
                }
            }
        }

        public List<AccommodationReservation> GetGuestReservations()
        {
            return Guest.Reservations;
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return AccReservationRepository.GetAllReservations();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return AccommodationRepository.GetAllAccommodations();
        }

        public List<Location> GetAccommodationLocations()
        {
            return AccommodationLocations;
        }

        private void LinkAccommodationsAndImages()
        {
            foreach (var image in ImageRepository.GetAllImages())
            {
                Accommodation accommodation = AccommodationRepository.GetAllAccommodations().Find(a => a.Id == image.EntityId);
                if (accommodation == null)
                {
                    continue;
                }
                if (accommodation.Images.Exists(i => i.Id == image.Id))
                {
                    continue;
                }

                accommodation.Images.Add(image);
            }
        }

        private void FillAccommodationLocationsList()
        {
            foreach(var accommodation in AccommodationRepository.GetAllAccommodations())
            {
                Location location = 
                    AccommodationLocations.Find(l => (l.City == accommodation.Location.City) && (l.Country == accommodation.Location.Country));

                if (location == null)
                {
                    AccommodationLocations.Add(accommodation.Location);
                }
            }

        }

        public void SubscribeToReservationRepository(IObserver observer)
        {
            AccReservationRepository.Subscribe(observer);
        }

        public void AddReservation(AccommodationReservation reservation)
        {
            Guest.Reservations.Add(reservation);
            AccReservationRepository.Add(reservation);
            
        }


    }
}
