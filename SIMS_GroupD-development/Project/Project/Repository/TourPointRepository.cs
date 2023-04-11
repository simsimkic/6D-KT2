using Project.Model;
using Project.Observer;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Repository
{
    public class TourPointRepository: ISubject
    {
        private const string FilePath = "../../../Resources/Data/tourpoint.csv";

        private readonly Serializer<TourPoint> serializer;
        private readonly List<IObserver> _observers;

        private List<TourPoint> tourPoints;

        public TourPointRepository()
        {
            serializer = new Serializer<TourPoint>();
            _observers = new List<IObserver>();
            tourPoints = serializer.FromCSV(FilePath);
        }

        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, tourPoints);
        }

        private int GenerateId()
        {
            if (tourPoints.Count == 0)
                return 0;

            return tourPoints[tourPoints.Count - 1].Id + 1;
        }

        public int Add(TourPoint tourPoint)
        {
            tourPoint.Id = GenerateId();
            tourPoints.Add(tourPoint);
            SaveInFile();
            NotifyObservers();
            return tourPoint.Id;


        }

        public void Update(int id,bool action)
        {
            TourPoint tourPoint = GetById(id);
            tourPoint.Action = action;
            SaveInFile();
            NotifyObservers();
        }

        public void Remove(int id)
        {
            TourPoint tourPoint = GetById(id);

            tourPoints.Remove(tourPoint);
            SaveInFile();

        }

        public TourPoint GetById(int id)
        {
            return tourPoints.Find(v => v.Id == id);
        }

        public List<TourPoint> GetAll()
        {
            return tourPoints;
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
