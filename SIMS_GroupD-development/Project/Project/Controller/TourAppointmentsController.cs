using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class TourAppointmentsController
    {
        TourAppointmentsRepositorycs tourAppointmentsRepository { get; set; }

        public TourAppointmentsController()
        {
            tourAppointmentsRepository = new TourAppointmentsRepositorycs();
        }

        public void Subscribe(IObserver observer)
        {
            tourAppointmentsRepository.Subscribe(observer);
        }

        public void Create(int tourId, List<DateTime> tourDates)
        {

            TourAppointments tourAppointments = new TourAppointments(tourId,tourDates);
            tourAppointmentsRepository.Add(tourAppointments);

        }

        public List<DateTime> GetAllAppointmentsDatesByTourId(int id)
        {
            List<DateTime> todayAppointments = new List<DateTime>();

            TourAppointments tourAppointments = tourAppointmentsRepository.GetById(id);

            foreach (DateTime date in tourAppointments.TourDates)
            {
                if(date == DateTime.Today)
                {
                    todayAppointments.Add(date);
                }
            }

            return todayAppointments;
        }
    }
}
