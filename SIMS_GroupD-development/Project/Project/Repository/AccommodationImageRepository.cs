using System;
using Project.Model;
using Project.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class AccommodationImageRepository
    {
        private const string FilePath = "../../../Resources/Data/accImages.csv";

        private readonly Serializer<AccommodationImage> serializer;

        private List<AccommodationImage> images;

        public AccommodationImageRepository()
        {
            serializer = new Serializer<AccommodationImage>();
            images = serializer.FromCSV(FilePath);
        }


        private void SaveInFile()
        {
            serializer.ToCSV(FilePath, images);
        }

        private int GenerateId()
        {
            if (images.Count == 0) return 0;
            return images[images.Count - 1].Id + 1;
        }

        public AccommodationImage Add(AccommodationImage image)
        {
            image.Id = GenerateId();
            images.Add(image);
            SaveInFile();
            return image;
        }

        public AccommodationImage Update(AccommodationImage image)
        {
            AccommodationImage oldImage = GetImageById(image.Id);
            if (oldImage == null) return null;

            oldImage.Url = image.Url;
            oldImage.EntityId = image.EntityId;


            SaveInFile();
            return oldImage;
        }

        public AccommodationImage Remove(int id)
        {
            AccommodationImage image = GetImageById(id);
            if (image == null) return null;

            images.Remove(image);
            SaveInFile();
            return image;
        }

        public AccommodationImage GetImageById(int id)
        {
            return images.Find(v => v.Id == id);
        }

        public List<AccommodationImage> GetAllImages()
        {
            return images;
        }

    }
}
