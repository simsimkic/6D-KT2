using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class PresentGuests: ISerializable
    {
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int AppointmentId { get; set; }
        public int TourPointId { get; set; }

        public PresentGuests()
        {
            GuestId = -1;
            TourId = -1;
            AppointmentId = -1;
            TourPointId = -1;
        }

        public PresentGuests(int guestId,int tourid, int appointmentId, int tourPointId)
        {
            GuestId = guestId;
            TourId = tourid;
            AppointmentId = appointmentId;
            TourPointId = tourPointId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                GuestId.ToString(),
                TourId.ToString(),
                AppointmentId.ToString(),
                TourPointId.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestId = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            AppointmentId = int.Parse(values[2]);
            TourPointId = int.Parse(values[3]);
        }
    }
}

