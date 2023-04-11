using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AppointmentService
    {
        AppointmentRepository appointmentRepository { get; set; }
        public AppointmentService()
        {
            appointmentRepository = new AppointmentRepository();
        }


        public void Subscribe(IObserver observer)
        {
            appointmentRepository.Subscribe(observer);
        }

        public void Create(int tourId, DateTime date)
        {

            Appointment appointment = new Appointment(tourId, date);
            appointmentRepository.Add(appointment);

        }

        public void RefreshAppointments()
        {
            appointmentRepository.RefreshAppointments();
        }

        public List<Appointment> GetByTourId(int id)
        {
            List<Appointment> allAppointments = appointmentRepository.GetAll();
            List<Appointment> appointments = new List<Appointment>();

            foreach (Appointment appoint in allAppointments)
            {
                if (appoint.TourId == id)
                {
                    appointments.Add(appoint);
                }
            }

            return appointments;

        }



    }
}
