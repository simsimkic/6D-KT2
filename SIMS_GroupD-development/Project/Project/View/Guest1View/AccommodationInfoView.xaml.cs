using Project.Controller;
using Project.Model;
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
    /// Interaction logic for AccommodationInfoView.xaml
    /// </summary>
    public partial class AccommodationInfoView : Window
    {
        public Guest1Controller Controller { get; set; }
        public Accommodation ChosenAccommodation { get; set; }

        public List<AccommodationImage> Images { get; set; }

        int i = 0;

        public AccommodationInfoView(Guest1Controller guest1Controller, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            Controller = guest1Controller;
            ChosenAccommodation = accommodation;
            Images = new List<AccommodationImage>(ChosenAccommodation.GetAccommodationImages());
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            i--;

            if (i < 0)
            {
                i = Images.Count -1;
            }

            picHolder.Source = new BitmapImage(new Uri(Images[i].Url, UriKind.RelativeOrAbsolute));
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            i++;

            if (i > Images.Count-1)
            {
                i = 0;
            }

            picHolder.Source = new BitmapImage(new Uri(Images[i].Url, UriKind.RelativeOrAbsolute));
        }

        private void btMakeReserv_Click(object sender, RoutedEventArgs e)
        {
            ReserveView reserveView = new ReserveView(Controller, ChosenAccommodation);
            reserveView.Show();
        }

    }
}
