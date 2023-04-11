using Microsoft.Win32;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Reflection.Metadata;
using Project.Model;
using System.Collections.ObjectModel;
using Project.Observer;
using Project.Service;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourGuideMainView.xaml
    /// </summary>
    public partial class TourGuideMainView : Window, INotifyPropertyChanged, IObserver
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly TourGuideController _tourGuideController;
        private readonly TourAppointmentsController _tourAppointmentsController;
        private readonly ImageController _imageController;
        private readonly TourPointController _tourPointController;
        private readonly TourPointsListController _tourPointsListController;
        private readonly LocationController _locationController;
        private readonly AppointmentController _appointmentController;

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;

        public Tour SelectedTour { get; set; }

        


        public ObservableCollection<Tour> Tours { get; set; }
        //public ObservableCollection<Tour> TourAppointments { get; set; }
        public ObservableCollection<TourPointsList> Points { get; set; }

        User User { get; set; }

        

        private string _imagesource;
        public string ImageSource
        {
            get => _imagesource;
            set
            {
                if (value != _imagesource)
                {
                    _imagesource = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourGuideMainView(User user)
        {
            InitializeComponent();
            DataContext =  this;

            User = user;
             
            _tourGuideController = new TourGuideController();
            _tourGuideController.Subscribe(this);

            _tourAppointmentsController = new TourAppointmentsController();
            _tourAppointmentsController.Subscribe(this);

            _imageController = new ImageController();
            _imageController.Subscribe(this);

            _tourPointController = new TourPointController();
            _tourPointController.Subscribe(this);

            _tourPointsListController = new TourPointsListController();
            _tourPointsListController.Subscribe(this);

            _locationController = new LocationController();
            _locationController.Subscribe(this);

            _appointmentController = new AppointmentController();
            _appointmentController.Subscribe(this);

            _tourService = new TourService();
            _tourService.Subscribe(this);

            _appointmentService = new AppointmentService();
            _appointmentService.Subscribe(this);

            ImageSource = "../../Resources/Data/images.csv";


            //Tours = new ObservableCollection<Tour>(_tourGuideController.GetAllTours());

            Tours = new ObservableCollection<Tour>(_tourService.GetAllTourAppointments());
            var t = Tours[1].TourAppointment.DateAndTimeOfAppointment.ToString();

        }


        public void Update()
        {
            UpdateTours();
            //UpdateToursServiceChanged();
            //Update2();
        }

        //public void UpdateTours()
        //{
        //    Tours.Clear();

        //    foreach (var tour in _tourGuideController.GetAllTours())
        //    {
        //        Tours.Add(tour);
        //    }
        //}

        public void UpdateToursServiceChanged()
        {
            Tours.Clear();

            foreach (var tour in _tourService.GetAll())
            {
                Tours.Add(tour);
            }
        }

        public void UpdateTours()
        {
            Tours.Clear();

            foreach(var tour in _tourService.GetAllTourAppointments())
            {
                Tours.Add(tour);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usernameLabel.Content = User.Username;
            cancelTour.IsEnabled = false;

        }


        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour addNewTour = new AddNewTour(_tourGuideController,_tourService, _imageController,_tourPointController,_tourPointsListController,_locationController, _appointmentController,_appointmentService);
            addNewTour.Show();
        }


        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedTour != null)
            {
                SingleTourOverview singleTour = new SingleTourOverview(SelectedTour);
                singleTour.Show();
            }
            
        }

        private void cancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                if (MessageBox.Show("Are you sure you want to cancel the tour?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //no
                }
                else
                {
                    //yes
                    _tourService.Cancel(SelectedTour.Id);
                }
            }
            
            
            
        }

        private void myTourDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedTour == null)
            {
                cancelTour.IsEnabled = false;
            }
            else
            {
                cancelTour.IsEnabled = true;
            }
            
        }
    }
}
