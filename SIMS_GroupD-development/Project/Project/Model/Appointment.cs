using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Appointment: ISerializable
    {
        public enum STATUS { NOTSTARTED, ACTIVE, COMPLETED }
        public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime DateAndTimeOfAppointment { get; set; }
        public STATUS Status { get; set; }
        public bool IsNotCanceled { get; set; }


        public Appointment()
        {
            Id = -1;
            TourId = -1;
            DateAndTimeOfAppointment = DateTime.MinValue;
            Status = STATUS.NOTSTARTED;
            IsNotCanceled = true;
        }

        public Appointment(int tourid,DateTime dateAndTime)
        {
            Id = -1;
            TourId = tourid;
            DateAndTimeOfAppointment = dateAndTime;
            Status = STATUS.NOTSTARTED;
            IsNotCanceled = true;

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                TourId.ToString(),
                DateAndTimeOfAppointment.ToString("dd/MM/yyyy hh:mm:ss tt"),
                Status.ToString(),
                IsNotCanceled.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            DateAndTimeOfAppointment = DateTime.Parse(values[2]);
            string status = values[3];
            switch (status)
            {
                case "ACTIVE":
                    Status = STATUS.ACTIVE;
                    break;
                case "COMPLETED":
                    Status = STATUS.COMPLETED;
                    break;
                default:
                    Status = STATUS.NOTSTARTED;
                    break;
            }
            IsNotCanceled = bool.Parse(values[4]);
        }
    }
}
