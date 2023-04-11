using Project.Controller;
using Project.Model;
using Project.Serializer;
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
using static System.Net.Mime.MediaTypeNames;


namespace Project.View
{
    /// <summary>
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private User user;
        
        private OwnerController controller;


        private List<AccommodationImage> tempImages;

        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public ObservableCollection<string> Countries { get; set; }

        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }

        public string SelectedCity { get; set; }





        public OwnerView(User u)
        {
            InitializeComponent();
            DataContext = this;
            controller = new OwnerController(u);

            Accommodations = new ObservableCollection<Accommodation>(controller.Owner.Accommodations);

            Countries = new ObservableCollection<string>();

            tempImages = new List<AccommodationImage>();

            CountryCities = new ObservableCollection<string>();
            FillCountriesList();

            /*
            Location location = new Location("Sombor", "Serbia");
             
            Accommodation acc = new Accommodation("primer",2,AccommodationType.COTTAGE,location,10,30,15);

            acc = controller.AccommodationRepository.Add(acc);
            Accommodations.Add(acc);
            */

        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }
        private void FillCountriesList()
        {
            foreach (var location in controller.GetLocations())
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
            foreach (var location in controller.GetLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

     

        private void btAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddLink.Text))
            {
                MessageBox.Show("Link is not specified");
                return;
            }
            try
            {
                AccommodationImage image = new AccommodationImage(0,tbAddLink.Text, controller.AccommodationRepository.GetLastId());
                tempImages.Add(image);
                MessageBox.Show("Image added successfully!");
            }
            catch
            {
                MessageBox.Show("Error occurred while adding image");
                return;
            }

        }

        private void btAddAccommodation_click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            int cancellationPeriod, guestNumber, advanceReservation;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is not entered properly");
                return;
            }

            try
            {
                guestNumber = Convert.ToInt32(tbMaximumGuests.Text);
            }
            catch
            {
                MessageBox.Show("Field is not a number.");
                return;
            }
            

            AccommodationType type = AccommodationType.COTTAGE;
            if (rbApartment.IsChecked == true)
            {
                type = AccommodationType.APPARTMENT;
            }
            else if (rbHouse.IsChecked == true)
            {
                type = AccommodationType.HOUSE;
            }


            try
            {
                cancellationPeriod = Convert.ToInt32(tbCancellationPeriod.Text);
            }
            catch
            {
                MessageBox.Show("Field is not a number.");
                return;
            }
            try
            {
                advanceReservation = Convert.ToInt32(tbAdvanceReservation.Text);
            }
            catch
            {
                MessageBox.Show("Field is not a number.");
                return;
            }

            Location location = new Location(SelectedCity, SelectedCountry);
            Accommodation accommodation = new Accommodation(name, controller.Owner.User.Id, type, location, guestNumber, advanceReservation, cancellationPeriod);
            foreach(var image in tempImages)
            {
                controller.AccommodationImageRepository.Add(image);
                accommodation.Images.Add(image);
            }
            
            accommodation = controller.AccommodationRepository.Add(accommodation);
            Accommodations.Add(accommodation);
            MessageBox.Show("You've successfully added accommodation to your account.");
            tempImages.Clear();
            /*
                        Location location = new Location(SelectedCity, SelectedCountry);
                        //public Accommodation(string name, int ownerId, AccommodationType at, Location location, int maxGuests, int minReservationDays, int cancellationPeriod = 1)
                        Accommodation acc = new Accommodation(name, 2, AccommodationType.COTTAGE, location, 10, 30, 15);

                        acc = controller.AccommodationRepository.Add(acc);
                        Accommodations.Add(acc);
            */
        }
    }
}
