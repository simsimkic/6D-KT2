using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourAppointments: ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public List<DateTime> TourDates { get; set; }

        public TourAppointments(int tourId,List<DateTime> tourDates)
        {
            Id = -1;
            TourId = tourId;
            TourDates = tourDates;

        }

        public TourAppointments()
        {
            Id = -1;
            TourId = -1;
            TourDates = new List<DateTime>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                GetConcatedDates(TourDates)
                
            };
            return csvValues;
        }

        public string GetConcatedDates(List<DateTime> dateList)
        {
            string concatenateString = "";
            foreach (DateTime date in dateList)
            {
                concatenateString +=  date.ToString() + "|";
            }

            return concatenateString;
        }



        public void FromCSV(string[] values)
        {
        

            Id = int.Parse(values[0]);
            TourId= int.Parse(values[1]);
            TourDates = new List<DateTime>();
            for(int i = 2; i < values.Length - 1; i++)
            {

                TourDates.Add(Convert.ToDateTime(values[i]).Date);
            }
        }
    }
}
