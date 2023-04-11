using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Model
{
    public class TourPointsList:ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; } 
        public List<int> PointsId { get; set; }

        public TourPointsList()
        {
            Id = -1;
            TourId = -1;
            PointsId = new List<int>();
        }

        public TourPointsList(int tourId,List<int> pointsId)
        {
            TourId=tourId;
            PointsId = pointsId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                TourId.ToString(),
                GetConcatedIds(PointsId)

            };
            return csvValues;
        }

        public string GetConcatedIds(List<int> ids)
        {
            string concatenateString = "";
            foreach (int id in ids)
            {
                concatenateString += id.ToString() + "|";
            }

            return concatenateString;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            for (int i = 2; i < values.Length - 1; i++)
            {

                PointsId.Add(int.Parse(values[i]));
            }
        }
    }
}
