using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;


namespace Project.Model
{
    public enum AccommodationType { HOUSE, APPARTMENT, COTTAGE }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Location Location { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public AccommodationType AccommodationType { get; set; }

        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationPeriod { get; set; }

        public List<AccommodationImage> Images { get; set; }

        // morao sam new location jer bi bilo null polje
        public Accommodation()
        {
            Images = new List<AccommodationImage>();
            Location = new Location();
        }

        public Accommodation(string name, int ownerId, AccommodationType at, Location location, int maxGuests, int minReservationDays, int cancellationPeriod = 1)
        {
            Name = name;
            OwnerId = ownerId;
            AccommodationType = at;
            Location = location;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationPeriod = cancellationPeriod;
            Images = new List<AccommodationImage>();

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),OwnerId.ToString(), AccommodationTypeToString(), Location.City, Location.Country, MaxGuests.ToString(),
                                    MinReservationDays.ToString(), CancellationPeriod.ToString(), Name };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            AccommodationType = StringToAccomodationType(values[2]);
            Location.City = values[3];
            Location.Country = values[4];
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancellationPeriod = Convert.ToInt32(values[7]);
            Name = values[8];
        }

        private string AccommodationTypeToString()
        {
            if (AccommodationType == AccommodationType.HOUSE)
            {
                return "HOUSE";
            }
            else if (AccommodationType == AccommodationType.APPARTMENT)
            {
                return "APPARTMENT";
            }
            else return "COTTAGE";
        }

        private AccommodationType StringToAccomodationType(string str)
        {
            if (str == "HOUSE")
            {
                return AccommodationType.HOUSE;
            }
            else if (str == "APPARTMENT")
            {
                return AccommodationType.APPARTMENT;
            }
            else
                return AccommodationType.COTTAGE;

        }

        public List<AccommodationImage> GetAccommodationImages()
        {
            return Images;
        }



    }
}
