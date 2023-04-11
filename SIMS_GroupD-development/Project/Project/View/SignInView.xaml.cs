using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using Project.Repository;
using Project.Model;
using Project.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.View.TourGuideView;
using Project.Controller;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        private readonly UserRepository _repository;
        private readonly TourGuideController _controller;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInView()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new UserRepository();
            _controller = new TourGuideController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _repository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case Role.OWNER:
                            OwnerView ownerView = new OwnerView(user);
                            ownerView.Show();
                            Close();
                            break;

                        case Role.GUIDE:
                            TourGuideMainView tourGuideMainView = new TourGuideMainView(user);
                            tourGuideMainView.Show();
                            Close();
                            break;

                        case Role.GUEST1:
                            Guest1View guest1View = new Guest1View(user);
                            guest1View.Show();
                            Close();
                            break;

                        default:
                            Guest2View guest2View = new Guest2View(user);
                            guest2View.Show();
                            Close();
                            break;


                    }
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

        }
    }
}
