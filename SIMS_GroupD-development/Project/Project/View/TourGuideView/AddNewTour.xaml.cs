using Microsoft.Win32;
using Project.Controller;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for AddNewTour.xaml
    /// </summary>
    public partial class AddNewTour : Window,INotifyPropertyChanged
    {


        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        //private string _country;
        //public string Country
        //{
        //    get => _country;
        //    set
        //    {
        //        if (value != _country)
        //        {
        //            _country = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //private string _city;
        //public string City
        //{
        //    get => _city;
        //    set
        //    {
        //        if (value != _city)
        //        {
        //            _city = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private string _name;
        public string NameOfTour
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();

                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string LanguageOfTour
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _coverImageUrl;


        public string CoverImageUrl
        {
            get => _coverImageUrl;
            set
            {
                if (value != _coverImageUrl)
                {
                    _coverImageUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location = new Location();

        public Location LocationOfTour
        {
            get => _location;
            set
            {
                if(value != _location)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _todayDate;
        public DateTime TodayDate
        {
            get => _todayDate;
            set
            {
                if (value != _todayDate)
                {
                    _todayDate = value;
                    OnPropertyChanged();
                }
            }
        }

        //private readonly TourGuideController _tourGuideController;
        private readonly ImageController _imageController;
        private readonly TourPointController _tourPointController;
        private readonly TourPointsListController _tourPointsListController;
        private readonly LocationController _locationController;
        //private readonly AppointmentController _appointmentController;

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;

        List<DateTime> dates = new List<DateTime>();
        List<string> images = new List<string>();
        List<int> pointsIds = new List<int>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddNewTour(TourGuideController tourGuideController,TourService tourService,ImageController imageController,
                            TourPointController tourPointController,TourPointsListController tourPointsListController, LocationController locationController,
                            AppointmentController appointmentController, AppointmentService appointmentService)
        {
            InitializeComponent();
            DataContext = this;

            //_tourGuideController = tourGuideController;
            _imageController = imageController;
            _tourPointController = tourPointController;
            _tourPointsListController = tourPointsListController;
            _locationController = locationController;
            //_appointmentController = appointmentController;

            _tourService = tourService;
            _appointmentService = appointmentService;

            LocationOfTour = new Location();

            TodayDate = DateTime.Now;
            StartDate = DateTime.Now;


        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelHeight = 200;
                bitmap.EndInit();

                images.Add(openFileDialog.FileName);
                imageList.Items.Add(bitmap);
            }

        }

        private void AddPoints_Click(object sender, RoutedEventArgs e)
        {
            string anotherPoint = pointInput.Text;

            if (anotherPoint != "")
            {
                int anotherTourPointId = _tourPointController.Create(anotherPoint, false);
                pointsIds.Add(anotherTourPointId);
            }

            pointInput.Clear();
            pointsList.Items.Add(anotherPoint);

        }

        private void pointInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                AddPoints_Click(sender, e);
            }

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            //PRAVLJENJE LOCATION-a

            if (cityComboBox.SelectedItem != null && countryComboBox.SelectedItem != null)
            {
                
                _location = _locationController.Create(cityComboBox.SelectedItem.ToString(), countryComboBox.SelectedItem.ToString());
            }

            //KREIRANJE TOUR-a

            int tourId  = _tourService.Create(_location, NameOfTour, Description, LanguageOfTour, MaxGuests, Duration);

            //KREIRANJE APPOINTMENT-a

            foreach(DateTime date in dates)
            {
                _appointmentService.Create(tourId,date);
            }

            dates.Clear();

            //KREIRANJE POINTS-a 

            if (startPointTextBox.Text != "" && endPointTextBox.Text != "")
            {
                int startPointId = _tourPointController.Create(startPointTextBox.Text, false);
                int endPointId = _tourPointController.Create(endPointTextBox.Text, false);
                pointsIds.Insert(0, startPointId);
                pointsIds.Add(endPointId);
            }

            _tourPointsListController.Create(tourId, pointsIds);

            //SKLADISTENJE SLIKA

            foreach (string image in images)
            {
                _imageController.Create(image, tourId, Model.PictureType.TOUR);
            }
            images.Clear();


            Close();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateAndTime = _tourService.BuildDate(StartDate, time.Text);

            dates.Add(dateAndTime);
            time.Clear();
            dateTimeList.Items.Add(dateAndTime.ToString("dd/MM/yyyy HH:mm:ss"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string[] country = _locationController.GetAllCountries();
            foreach (string element in country)
            {
                countryComboBox.Items.Add(element);
            }

            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach(string element in language)
            {
                languageComboBox.Items.Add(element);
            }

            AddPoints.IsEnabled = false;


        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityComboBox.Items.Clear();
            string [] cities = _locationController.GetAppropriateCities(countryComboBox.SelectedItem.ToString());

            foreach (string city in cities)
            {
                cityComboBox.Items.Add(city);
            }
        }


        private void startPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (startPointTextBox.Text != "" && endPointTextBox.Text != "")
            {
                AddPoints.IsEnabled = true;
            }
            else
            {
                AddPoints.IsEnabled = false;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
