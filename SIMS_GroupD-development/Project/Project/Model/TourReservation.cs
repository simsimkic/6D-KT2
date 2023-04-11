
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GuestId { get; set; } 

        public Guest2 Guest { get; set; } 

        public int TourId { get; set; }

        public Tour Tour { get; set; }

        public TourReservation() { }

        public TourReservation(int id, DateTime start, DateTime end, int guestId, int tourId)
        {
            Id = id;
            StartDate = start;
            EndDate = end;
            GuestId = guestId;
            TourId = tourId;
        }

        public TourReservation(TourReservation tourReservation)
        {
            Id = tourReservation.Id;
            StartDate = tourReservation.StartDate;
            EndDate = tourReservation.EndDate;
            GuestId = tourReservation.GuestId;
            TourId = tourReservation.TourId;
            Guest = tourReservation.Guest;
            Tour = tourReservation.Tour;
        }

        public TourReservation(int id, List<DateTime>TourDates, int guestId, int tourId)
        {
            Id = id;
            StartDate = TourDates[0];
            EndDate = TourDates[1];
            GuestId = guestId;
            TourId = tourId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            GuestId = int.Parse(values[3]);
            TourId = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString(), EndDate.ToString(), GuestId.ToString(), TourId.ToString() };
            return csvValues;
        }
    }
}
