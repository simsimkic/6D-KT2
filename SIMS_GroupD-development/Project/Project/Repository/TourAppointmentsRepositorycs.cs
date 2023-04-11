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
    public class TourAppointmentsRepositorycs: ISubject
    {
        private const string FilePath = "../../../Resources/Data/tourappointments.csv";

        private readonly Serializer<TourAppointments> serializer;
        private readonly List<IObserver> _observers;

        private List<TourAppointments> tourAppointments;

        public TourAppointmentsRepositorycs()
        {
            serializer = new Serializer<TourAppointments>();
            _observers = new List<IObserver>(); 
            tourAppointments = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourAppointments);
        }

        private int GenerateId()
        {
            if (tourAppointments.Count == 0)
                return 0;

            return tourAppointments[tourAppointments.Count - 1].Id + 1;
        }

        public void Add(TourAppointments tourAppointment)
        {
            tourAppointment.Id = GenerateId();
            tourAppointments.Add(tourAppointment);
            SaveInFile();
            NotifyObservers();

        }

        public void Remove(int id)
        {
            TourAppointments tourAppointment = GetById(id);

            tourAppointments.Remove(tourAppointment);
            SaveInFile();

        }

        public TourAppointments GetById(int id)
        {
            return tourAppointments.Find(v => v.Id == id);
        }

        public List<TourAppointments> GetAll()
        {
            return tourAppointments;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
