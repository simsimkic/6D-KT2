using Project.Observer;
using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Xml.Linq;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for Guest2View.xaml
    /// </summary>
    public partial class Guest2View : Window, IObserver
    {
        private Guest2Controller controller;
        private User user;
        public ObservableCollection<TourReservation> TourReservations { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Tour> FilteredTours { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedLanguage { get; set; }
        public Tour SelectedTour { get; set; }


        public Guest2View(User u)
        {
            InitializeComponent();
            DataContext = this;
            controller = new Guest2Controller(u);
            TourReservations = new ObservableCollection<TourReservation>(controller.GetTourReservations());
            Tours = new ObservableCollection<Tour>(controller.GetTours());
            FilteredTours = new ObservableCollection<Tour>(Tours);
            controller.SubscribeToReservationRepo(this);
            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();
            FillCountriesList();
            FillLanguagesList();
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryCities.Clear();
            foreach(var location in controller.GetTourLocations())
            {
                if(location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Languages.Clear();
            foreach(var language in controller.GetTourLanguages())
            {
                if(language.ToString() == SelectedLanguage)
                {
                    Languages.Add(language.ToString());
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Tour> temp = new List<Tour>();
            List<Tour> tempFiltered = new List<Tour>();
            bool hasEntered = false;

            temp.AddRange(Tours);

            // Location comboboxes

            if (!string.IsNullOrEmpty(SelectedCountry))
            {
                bool isCityChosen = false;
                hasEntered = true;
                if (!string.IsNullOrEmpty(SelectedCity))
                {
                    isCityChosen = true;
                }

                foreach (Tour tour in temp)
                {
                    if (tour.Location.Country == SelectedCountry)
                    {
                        if (isCityChosen)
                        {
                            if (tour.Location.City == SelectedCity)
                            {
                                tempFiltered.Add(tour);

                            }

                            continue;
                        }
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }

            // Number of guests

            if (!string.IsNullOrWhiteSpace(tbGuestNumber.Text))
            {
                if (!IsDigitsOnly(tbGuestNumber.Text))
                {
                    string sMessageBoxText = $"Number of guests field must contain only digits!";
                    string sCaption = "Input error - Number of guests";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int guestNum = Convert.ToInt32(tbGuestNumber.Text);

                foreach (Tour tour in temp)
                {
                    if (guestNum >= tour.MaxGuests)
                    {
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }


            // Duration of tour

            if (!string.IsNullOrWhiteSpace(tbHours.Text))
            {
                if (!IsDigitsOnly(tbHours.Text))
                {
                    string sMessageBoxText = $"Duration of tour field must contain only digits!";
                    string sCaption = "Input error - Number of days";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int durationInHours = Convert.ToInt32(tbHours.Text);

                foreach (Tour tour in temp)
                {
                    if (durationInHours >= tour.Duration)
                    {
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }

            FilteredTours.Clear();
            foreach(Tour t in temp)
            {
                FilteredTours.Add(t);
            }

            
        }

        private void FillCountriesList()
        {
            foreach(var location in controller.GetTourLocations())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void FillLanguagesList()
        {
            foreach(var language in controller.GetTourLanguages())
            {
                if (!Languages.Contains(language.ToString()))
                {
                    Languages.Add(language.ToString());
                }
            }
        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private void tbViewDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TourInfoView tourInfoView = new TourInfoView(controller, SelectedTour);
            tourInfoView.Show();
        }

        private void UpdateMyTourReservationsList()
        {
            TourReservations.Clear();
            foreach(var reservation in controller.GetTourReservations())
            {
                TourReservations.Add(reservation);
            }
        }

        public void Update()
        {
            UpdateMyTourReservationsList();
        }
    }
}
