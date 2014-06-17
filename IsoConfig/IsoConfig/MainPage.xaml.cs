using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IsoConfig.Resources;
using System.IO.IsolatedStorage;

namespace IsoConfig
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings isoStore = IsolatedStorageSettings.ApplicationSettings;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string twitter;
            string email;

            if (isoStore.TryGetValue<string>("twitter", out twitter) && isoStore.TryGetValue<string>("email", out email))
            {
                valorSetting.Text = twitter;
                valorSetting2.Text = email;
            }
            else 
            {
                valorSetting.Text = String.Empty;
                valorSetting2.Text = String.Empty;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Config.xaml", UriKind.Relative));
        }
    }
}