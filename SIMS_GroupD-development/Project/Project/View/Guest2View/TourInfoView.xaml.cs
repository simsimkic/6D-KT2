using Project.Model;
using Project.Controller;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TourInfoView.xaml
    /// </summary>
    public partial class TourInfoView : Window
    {

        public Guest2Controller Controller { get; set; }
        public Tour ChosenTour { get; set; }

        

        public TourInfoView(Guest2Controller guest2Controller, Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            Controller = guest2Controller;
            ChosenTour = tour;
        }

        private void btMakeReserv_Click(object sender, RoutedEventArgs e)
        {
            ReserveTourSpotView reserveSpot = new ReserveTourSpotView(Controller, ChosenTour);
            reserveSpot.Show();
        }
    }
}
