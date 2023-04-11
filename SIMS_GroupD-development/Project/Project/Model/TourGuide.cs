using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourGuide
    {
        public User User { get; set; }
        public List<Tour> Tours  {get; set;}

        public TourGuide()
        {
            Tours = new List<Tour>();
            User = new User();
        }

        public TourGuide(User user)
        {
            User = user;
            Tours = new List<Tour>();
        }   
    }
}
