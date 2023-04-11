using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Owner
    {
        public User User { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public Owner()
        {
            Accommodations = new List<Accommodation>();
            User = new User();
        }

        public Owner(User user)
        {
            User = user;
            Accommodations = new List<Accommodation>();

        }


    }
}
