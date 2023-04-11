using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using Project.Observer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class AccommodationReservationRepository : ISubject
    {

        private const string FilePath = "../../../Resources/Data/accReservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accReservations;

        private List<IObserver> _observers;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accReservations = _serializer.FromCSV(FilePath);
            _observers = new List<IObserver>();
        }


        private void SaveInFile()
        {
            _serializer.ToCSV(FilePath, _accReservations);
        }

        private int GenerateId()
        {
            if (_accReservations.Count == 0) return 0;
            return _accReservations[_accReservations.Count - 1].Id + 1;
        }

        public AccommodationReservation Add(AccommodationReservation accReservation)
        {
            accReservation.Id = GenerateId();
            _accReservations.Add(accReservation);
            SaveInFile();
            NotifyObservers();
            return accReservation;
        }

        public AccommodationReservation Update(AccommodationReservation accReservation)
        {
            AccommodationReservation oldReservation = GetReservationById(accReservation.Id);
            if (oldReservation == null) return null;

            oldReservation.StartDate = accReservation.StartDate;
            oldReservation.EndDate = accReservation.EndDate;
            oldReservation.GuestId = accReservation.GuestId;
            oldReservation.AccommodationId = accReservation.AccommodationId;


            SaveInFile();
            NotifyObservers();
            return oldReservation;
        }

        public AccommodationReservation Remove(int id)
        {
            AccommodationReservation reservation = GetReservationById(id);
            if (reservation == null) return null;

            _accReservations.Remove(reservation);
            SaveInFile();
            NotifyObservers();
            return reservation;
        }

        public AccommodationReservation GetReservationById(int id)
        {
            return _accReservations.Find(v => v.Id == id);
        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return _accReservations;
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
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
