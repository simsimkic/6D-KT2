using Project.Model;
using Project.Repository;
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

namespace Project.View.TourGuideView
{


    public partial class AddPresentGuests : Window
    {

        public ObservableCollection<User> Reservations { get; set; }

        public TourReservationRepository reservationRepository;
        public UserRepository userRepository;
        public PresentGuestsRepository presentGuestsRepository;
        public AppointmentRepository appointmentRepository;

        public int tourId { get; set; }
        public int tourPointId { get; set; }
        public int appointmentId { get; set; }
        public User SelectedUser { get; set; }

        public List<PresentGuests> presentGuests { get; set; }

        public AddPresentGuests(int id , int tourpointid, int appointmentid, PresentGuestsRepository pGrepository)
        {
            InitializeComponent();
            DataContext = this;

            tourId = id;
            tourPointId = tourpointid;
            appointmentId = appointmentid;

            reservationRepository = new TourReservationRepository();
            userRepository = new UserRepository();
            presentGuestsRepository = pGrepository;
            
            presentGuests = new List<PresentGuests>();

            presentGuests = presentGuestsRepository.GetByAppointmentId(appointmentId);

            Reservations = new ObservableCollection<User>(GetNotPresentGuests());
        }


        public List<User> GetNotPresentGuests()
        {
            List<User> notPresent = new List<User>();

            foreach(User guest in GetApproprietReservations())
            {
                if (!presentGuestsRepository.GetAllGuestIds().Contains(guest.Id))
                {
                    notPresent.Add(guest);
                }
            }

            return notPresent;
        }


        public List<User> GetApproprietReservations()
        {
            List<TourReservation> tourReservations = reservationRepository.GetAllTourReservations();
            List<User> approprietReservations = new List<User>();

            foreach (TourReservation reservation in tourReservations)
            {
                if (reservation.TourId == tourId)
                {
                    approprietReservations.Add(userRepository.GetById(reservation.GuestId));
                }
            }

            return approprietReservations;
        }

        private void Dismiss_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSelectedGuest_Click(object sender, RoutedEventArgs e)
        {
            PresentGuests presentGuest = new PresentGuests();
            presentGuest.GuestId = SelectedUser.Id;
            presentGuest.TourId = tourId;
            presentGuest.AppointmentId = appointmentId;
            presentGuest.TourPointId = tourPointId;


            presentGuestsRepository.Add(presentGuest);

            Close();

        }
    }
}
