using Project.Controller;
using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window, IObserver
    {
        private Guest1Controller _controller;

        public ObservableCollection<AccommodationReservation> MyReservations { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Accommodation SelectedAccommodation { get; set; }
        public Guest1View(User u)
        {
            InitializeComponent();
            DataContext = this;

            _controller = new Guest1Controller(u);
            MyReservations = new ObservableCollection<AccommodationReservation>(_controller.GetGuestReservations());
            Accommodations = new ObservableCollection<Accommodation>(_controller.GetAllAccommodations());
            FilteredAccommodations = new ObservableCollection<Accommodation>(Accommodations);
            _controller.SubscribeToReservationRepository(this);

            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            FillCountriesList();
            
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void FillCountriesList()
        {
            foreach (var location in _controller.GetAccommodationLocations())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryCities.Clear();
            foreach (var location in _controller.GetAccommodationLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Accommodation> temp = new List<Accommodation>();
            List<Accommodation> tempFiltered = new List<Accommodation>();
            bool hasEntered = false;

            temp.AddRange(Accommodations);

            // Name textbox
            if (!string.IsNullOrWhiteSpace(tbName.Text))
            {
                hasEntered = true;

                foreach (Accommodation accommodation in temp)
                {
                    if (accommodation.Name.Contains(tbName.Text))
                    {
                        tempFiltered.Add(accommodation);
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

            // Location comboboxes

            if (!string.IsNullOrEmpty(SelectedCountry))
            {
                bool isCityChosen = false;
                hasEntered = true;
                if (!string.IsNullOrEmpty(SelectedCity))
                {
                    isCityChosen = true;
                }

                foreach (Accommodation accommodation in temp)
                {
                    if (accommodation.Location.Country == SelectedCountry)
                    {
                        if (isCityChosen)
                        {
                            if (accommodation.Location.City == SelectedCity)
                            {
                                tempFiltered.Add(accommodation);
                                
                            }
                                                         
                            continue;
                        }
                        tempFiltered.Add(accommodation);
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

            if (!string.IsNullOrWhiteSpace(tbGuestNum.Text))
            {
                if (!IsDigitsOnly(tbGuestNum.Text))
                {
                    string sMessageBoxText = $"Number of guests field must contain only digits!";
                    string sCaption = "Input error: Number of guests";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int guestNum = Convert.ToInt32(tbGuestNum.Text);

                foreach (Accommodation accommodation in temp)
                {
                    if (guestNum <= accommodation.MaxGuests)
                    {
                        tempFiltered.Add(accommodation);
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


            // Number of days

            if (!string.IsNullOrWhiteSpace(tbDaysNum.Text))
            {
                if (!IsDigitsOnly(tbDaysNum.Text))
                {
                    string sMessageBoxText = $"Number of days field must contain only digits!";
                    string sCaption = "Input error: Number of days";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int daysNum = Convert.ToInt32(tbDaysNum.Text);

                foreach (Accommodation accommodation in temp)
                {
                    if (daysNum >= accommodation.MinReservationDays)
                    {
                        tempFiltered.Add(accommodation);
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

            // Accommodation type checkboxes

            if ((chbHouse.IsChecked == false) && (chbAppartment.IsChecked == false) && (chbCottage.IsChecked == false))
            {
                FilteredAccommodations.Clear();
                foreach (Accommodation a in temp)
                {
                    FilteredAccommodations.Add(a);
                }
                return;
            }

            foreach (Accommodation accommodation in temp)
            {
                if((accommodation.AccommodationType == AccommodationType.HOUSE && (bool)chbHouse.IsChecked) ||
                    (accommodation.AccommodationType == AccommodationType.APPARTMENT && (bool)chbAppartment.IsChecked) ||
                    (accommodation.AccommodationType == AccommodationType.COTTAGE && (bool)chbCottage.IsChecked))
                {
                    tempFiltered.Add(accommodation);
                }
            }

            FilteredAccommodations.Clear();
            foreach (Accommodation a in tempFiltered)
            {
                FilteredAccommodations.Add(a);
            }
            

        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }


        private void tbViewDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AccommodationInfoView accommodationInfoView = new AccommodationInfoView(_controller, SelectedAccommodation);
            accommodationInfoView.Show();
        }

        private void UpdateMyReservationsList()
        {
            MyReservations.Clear();
            foreach (var reservation in _controller.GetGuestReservations())
            {
                MyReservations.Add(reservation);
            }
        }

        public void Update()
        {
            UpdateMyReservationsList();
        }

        private void btMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                string sMessageBoxText = $"Choose an accommodation first!";
                string sCaption = "Reservation not chosen";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;


                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return;
            }

            ReserveView reserveView = new ReserveView(_controller, SelectedAccommodation);
            reserveView.Show();
        }
    }
}
