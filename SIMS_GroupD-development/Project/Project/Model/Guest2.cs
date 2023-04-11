using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Guest2
    {
        public User User { get; set; }

        public List<TourReservation> Reservations { get; set; } 

        public Guest2()
        {
            Reservations = new List<TourReservation>();
            User = new User();
        }

        public Guest2(User user)
        {
            User = user;
            Reservations = new List<TourReservation>();
        }
    }
}
