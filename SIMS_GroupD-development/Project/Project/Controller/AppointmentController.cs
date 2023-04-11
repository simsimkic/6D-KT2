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
    public class AppointmentController
    {
        AppointmentRepository appointmentRepository { get; set; }

        public AppointmentController()
        {
            appointmentRepository = new AppointmentRepository();
        }

        public void Subscribe(IObserver observer)
        {
            appointmentRepository.Subscribe(observer);
        }

        public void Create(int tourId,DateTime date)
        {

            Appointment appointment = new Appointment(tourId,date);
            appointmentRepository.Add(appointment);

        }

        public List<Appointment> GetByTourId(int id)
        {
            List<Appointment> allAppointments = appointmentRepository.GetAll();
            List<Appointment> appointments = new List<Appointment>();

            foreach(Appointment appoint in allAppointments)
            {
                if(appoint.TourId == id)
                {
                    appointments.Add(appoint);
                }
            }

            return appointments;

        }
        
        public Appointment GetByDateAndTourId(DateTime date,int tourId)
        {
            return appointmentRepository.GetByDateAndTour(date, tourId);
        }

        public List<DateTime> GetAppointmentsDatesByTourId(int id)
        {
            List<DateTime> dates = new List<DateTime>();

            List<Appointment> appointments = appointmentRepository.GetAll();

            foreach (Appointment appointment in appointments)
            {
                if(appointment.TourId == id)
                {
                    if (appointment.DateAndTimeOfAppointment.ToShortDateString() == DateTime.Today.ToShortDateString())
                    {
                        dates.Add(appointment.DateAndTimeOfAppointment);
                    }
                        
                }
            }
            return dates;
        }
    }
}
