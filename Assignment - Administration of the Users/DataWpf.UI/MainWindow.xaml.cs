using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

using System.ComponentModel;

namespace WpfTrackingApplication
{
    /// <summary>
    /// Interaction logic for Mainwindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            PropertyChanged += MainWindow_PropertyChanged;
        }

        void MainWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Username = Username;
            Password = Password;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public string Username
        {
            get { return _Username; }

            set
            {
                if (_Username == value) return;
                _Username = value;
                RaisePropertyChanged("Username");
            }
        }
        private string _Username;

        public string Password
        {
            get { return _Password; }

            set
            {
                if (_Password == value) return;
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }
        private string _Password;

    }
}