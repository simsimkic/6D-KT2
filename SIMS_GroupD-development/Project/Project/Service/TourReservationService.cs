using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourReservationService
    {
        TourReservationRepository tourReservationRepository;

        public TourReservationService()
        {
            tourReservationRepository = new TourReservationRepository();
        }
    }
}
