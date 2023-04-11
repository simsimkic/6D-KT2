using Project.Model;
using Project.Observer;
using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class ImageRepository: ISubject
    {
        private const string FilePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> serializer;
        private readonly List<IObserver> _observers;

        private List<Image> images;

        public ImageRepository()
        {
            serializer = new Serializer<Image>();
            _observers = new List<IObserver>();
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

        public Image Add(Image image)
        {
            image.Id = GenerateId();
            images.Add(image);
            SaveInFile();
            return image;
        }

        public Image Update(Image image)
        {
            Image oldImage = GetImageById(image.Id);
            if (oldImage == null) return null;

            oldImage.Url = image.Url;
            oldImage.EntityId = image.EntityId;


            SaveInFile();
            return oldImage;
        }

        public Image Remove(int id)
        {
            Image image = GetImageById(id);
            if (image == null) return null;

            images.Remove(image);
            SaveInFile();
            return image;
        }

        public Image GetImageById(int id)
        {
            return images.Find(v => v.Id == id);
        }

        public List<Image> GetImagesByEntityIdandType(int id, PictureType type)
        {
            List<Image> images = new List<Image>();
            List<Image> filteredImages = new List<Image>();
            images = GetAllImages();

            List<int> imagesIds = new List<int>();

            foreach (Image image in images)
            {
                if(image.EntityId == id && image.Type == type)
                {
                    imagesIds.Add(image.Id);
                }
            }

            foreach(int identificator in imagesIds)
            {
                filteredImages.Add(images.Find(v => v.Id == identificator));
            }

            return filteredImages;
        }

        public List<Image> GetAllImages()
        {
            return images;
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
            foreach(var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
