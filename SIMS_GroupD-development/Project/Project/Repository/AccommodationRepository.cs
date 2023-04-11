using Project.Model;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> serializer;

        private List<Accommodation> accommodations;

        public AccommodationRepository()
        {
            serializer = new Serializer<Accommodation>();
            accommodations = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, accommodations);
        }

        private int GenerateId()
        {
            if (accommodations.Count == 0) return 0;
            return accommodations[accommodations.Count - 1].Id + 1;
        }

        public int GetLastId()
        {
            if (accommodations.Count == 0) return 0;
            return accommodations[accommodations.Count - 1].Id + 1;
        }

        public Accommodation Add(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            accommodations.Add(accommodation);
            SaveInFile();
            return accommodation;
        }

        public Accommodation Update(Accommodation accommodation)
        {
            Accommodation oldAccommodation = GetAccommodationById(accommodation.Id);
            if (oldAccommodation == null) return null;

            oldAccommodation.Name = accommodation.Name;
            oldAccommodation.OwnerId = accommodation.OwnerId;
            oldAccommodation.Location = accommodation.Location;
            oldAccommodation.MaxGuests = accommodation.MaxGuests;
            oldAccommodation.MinReservationDays = accommodation.MinReservationDays;
            oldAccommodation.CancellationPeriod = accommodation.CancellationPeriod;
            oldAccommodation.AccommodationType = accommodation.AccommodationType;

            SaveInFile();
            return oldAccommodation;
        }

        public Accommodation Remove(int id)
        {
            Accommodation accommodation = GetAccommodationById(id);
            if (accommodation == null) return null;

            accommodations.Remove(accommodation);
            SaveInFile();
            return accommodation;
        }

        public Accommodation GetAccommodationById(int id)
        {
            return accommodations.Find(v => v.Id == id);
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return accommodations;
        }

    }
}
