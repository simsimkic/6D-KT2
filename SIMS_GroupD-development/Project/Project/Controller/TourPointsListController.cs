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
    public class TourPointsListController
    {
        TourPointsListRepository tourPointsListRepository { get; set; }
        TourPointController tourPointController { get; set; }

        public TourPointsListController()
        {
            tourPointsListRepository = new TourPointsListRepository();
            tourPointController = new TourPointController();
        }

        public void Subscribe(IObserver observer)
        {
            tourPointsListRepository.Subscribe(observer);
        }

        public void Create(int tourId, List<int> pointsId)
        {
            TourPointsList tourPointsList = new TourPointsList(tourId,pointsId);
            tourPointsListRepository.Add(tourPointsList);
        
        }

        public TourPointsList GetByTourId(int id) {
            List<TourPointsList> tourPointsLists = GetAll();
            TourPointsList tourPoints = new TourPointsList();

            foreach(TourPointsList tourPointsList in tourPointsLists)
            {
                if(tourPointsList.TourId == id)
                {
                    tourPoints = tourPointsList;
                }

            }

            return tourPoints;
        
        }

        public List<TourPointsList> GetAll()
        {
            return tourPointsListRepository.GetAll();
        }

        public List<TourPoint> GetPointsByTourId(int id)
        {
            List<TourPoint> points = new List<TourPoint>();

            TourPointsList tourPointsList = GetByTourId(id);

            foreach (int tourPointId in tourPointsList.PointsId)
            {
                points.Add(tourPointController.GetById(tourPointId));
            }

            return points;
        }
    }
}
