using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Location : ISerializable
    {

        public int Id { get; set; }
        public string City { get; set; }

        public string Country { get; set; }
        public Location() 
        {
            City = "";
            Country = "";
        }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), City, Country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }

    }
}
