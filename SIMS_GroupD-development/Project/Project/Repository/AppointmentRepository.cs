using Project.Model;
using Project.Observer;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class AppointmentRepository: ISubject
    {
        private const string FilePath = "../../../Resources/Data/appointment.csv";

        private readonly Serializer<Appointment> serializer;

        private readonly List<IObserver> _observers;

        private List<Appointment> appointments;

        public AppointmentRepository()
        {
            serializer = new Serializer<Appointment>();
            appointments = serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, appointments);
        }

        private int GenerateId()
        {
            if (appointments.Count == 0)
                return 0;

            return appointments[appointments.Count - 1].Id + 1;
        }

        public void Add(Appointment appointment)
        {
            appointment.Id = GenerateId();
            appointments.Add(appointment);
            SaveInFile();
            NotifyObservers();

        }

        public void Remove(int id)
        {
            Appointment appointment = GetById(id);

            appointments.Remove(appointment);
            SaveInFile();

        }

        public Appointment GetById(int id)
        {
            return appointments.Find(v => v.Id == id);
        }

        public int GetTourIdById(int id) 
        {
            int tourId = -1;
            foreach (Appointment appointment in GetAll()){
                if(appointment.Id == id)
                {
                    tourId = appointment.TourId;
                }
            }

            return tourId;
        }


        public Appointment GetByDateAndTour(DateTime date,int id)
        {
            return appointments.Find(v => v.TourId == id && v.DateAndTimeOfAppointment == date);
        }


        public List<Appointment> GetAll()
        {
            return appointments;
        }

        public void RefreshAppointments()
        {
            appointments = serializer.FromCSV(FilePath);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
