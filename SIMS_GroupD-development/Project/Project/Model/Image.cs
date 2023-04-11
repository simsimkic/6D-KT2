using System;
using Project.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public enum PictureType { ACCOMMODATION, TOUR }
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int EntityId { get; set; }
        public PictureType Type { get; set; }

        public Image() { }

        public Image(string url, int entityId, PictureType type)
        {
            Url = url;
            EntityId = entityId;
            Type = type;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Url, EntityId.ToString(), PictureTypeToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Url = values[1];
            EntityId = Convert.ToInt32(values[2]);
            Type = StringToPictureType(values[3]);
        }

        private string PictureTypeToString()
        {
            if (Type == PictureType.ACCOMMODATION)
            {
                return "ACCOMMODATION";
            }
            else
                return "TOUR";

        }

        private PictureType StringToPictureType(string type)
        {
            if (type == "ACCOMMODATION")
            {
                return PictureType.ACCOMMODATION;
            }
            else
                return PictureType.TOUR;
        }
    }
}