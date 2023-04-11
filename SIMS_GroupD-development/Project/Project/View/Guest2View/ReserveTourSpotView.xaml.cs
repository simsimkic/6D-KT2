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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for ReserveTourSpotView.xaml
    /// </summary>
    public partial class ReserveTourSpotView : Window
    {
        public Guest2Controller Controller { get; set; }
        public Tour Tour { get; set; }

        public ObservableCollection<TourAppointments> TourSpots { get; set; }

        public Tour SelectedTour { get; set; }

        public ReserveTourSpotView(Guest2Controller controller, Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            Controller = controller;
            Tour = tour;
            TourSpots = new ObservableCollection<TourAppointments>();
        }

        private void btSearchAvailableTours_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckConditions()) return;

            double numberOfGuests = Convert.ToDouble(tbGuests.Text);

            List<TourReservation> reservationsInSameLocation = new List<TourReservation>(GetToursInSameLocation());
        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private bool IsGuestsEmpty()
        {
            if (string.IsNullOrWhiteSpace(tbGuests.Text))
            {
                string sMessageBoxText = $"Please enter number of guests.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                string sCaption = "Missing input";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return true;
            }
            return false;
        }

        private bool IsGuestsDigits()
        {
            if (!IsDigitsOnly(tbGuests.Text))
            {
                string sMessageBoxText = $"Number of guests field must contain only digits.";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Input error - Number of guests";
                MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                return false;
            }
            return true;
        }

        private bool CheckMaxGuestsLimit(int numberOfGuests)
        {
            if(numberOfGuests > Tour.MaxGuests)
            {
                string sMessageBoxText = $"Maximum number of guests for this tour is {Tour.MaxGuests}";

                MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                string sCaption = "Exceeded number of guests";
                MessageBox.Show(sMessageBoxText,sCaption, btnMessageBox, icnMessageBox);
                return false;

            }
            return true;
        }

        private bool CheckConditions()
        {
            if (IsGuestsEmpty()) return false;
            if(!IsGuestsDigits()) return false;

            int numberOfGuests = Convert.ToInt32(tbGuests.Text);
            if(!CheckMaxGuestsLimit(numberOfGuests)) return false;

            return true;
        }

        private List<TourReservation> GetToursInSameLocation()
        {
            List<TourReservation> reservations = new List<TourReservation>(Controller.GetTourReservations());
            List<TourReservation> toursInSameLocation = new List<TourReservation>();

            foreach(var reservation in reservations)
            {
                if(reservation.TourId == Tour.Id)
                {
                    if ((reservation.Tour.City == Tour.City) && (reservation.Tour.MaxGuests <= Tour.MaxGuests))
                        continue;
                    toursInSameLocation.Add(reservation);
                }

            }

            return toursInSameLocation;
        }

        private void btReserve_Click(object sender, RoutedEventArgs e)
        {
            TourAppointments tourAppointments = new TourAppointments();
            tourAppointments.TourId = Tour.Id;
            
            TourReservation tourReservation = new TourReservation(Tour.Id, new DateTime(), new DateTime(), 4, Tour.Id);
            Controller.AddReservation(tourReservation);

        }
    }
}
