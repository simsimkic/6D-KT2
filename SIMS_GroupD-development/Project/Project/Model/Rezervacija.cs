using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public string Guest { get; set; }
        public bool IsArrived { get; set; }
        public int AppointmentId { get; set; }

        public Rezervacija()
        {
            Id = -1;
            Guest = "";
            IsArrived = false;
            AppointmentId = -1;
        }

        public Rezervacija(int id, string guest, bool isArrived, int appointmentId )
        {
            Id = id;
            Guest = guest;
            IsArrived = isArrived;
            AppointmentId = appointmentId;
        }
    }
}
