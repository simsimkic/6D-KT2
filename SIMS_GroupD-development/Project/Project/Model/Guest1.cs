using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Guest1
    {
        public User User { get; set; }
        public List<AccommodationReservation> Reservations { get; set; }

        public Guest1()
        {
            Reservations = new List<AccommodationReservation>();
            User = new User();
        }

        public Guest1(User user)
        {
            User = user;
            Reservations = new List<AccommodationReservation>();
        }

    }
}
