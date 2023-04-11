using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Model
{
    public class TourPoint: ISerializable
    {
        public int Id { get; set; }
        public string PointName { get; set; }


        public bool Action { get; set; }

        public TourPoint()
        {
            Id = -1;
            PointName = "";

            Action = false;

        }

        public TourPoint(string name,bool action)
        {
            Id = -1;
            PointName = name;

            Action = action;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                PointName,

                Action.ToString()
               
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            PointName = values[1];

            Action = bool.Parse(values[2]);

        }
    }

}
