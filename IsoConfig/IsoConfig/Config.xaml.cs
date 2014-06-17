using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace IsoConfig
{
    public partial class Config : PhoneApplicationPage
    {
        IsolatedStorageSettings isoStore = IsolatedStorageSettings.ApplicationSettings;
        public Config()
        {
            InitializeComponent();
        }

        private void SetSettingValue(string key, string value)
        {
            if (!isoStore.Contains(key))
            {
                isoStore.Add(key, value);
            }
            else
            {
                isoStore[key] = value;
            }

            isoStore.Save();
        }

        private void RemoveSettingValue(string key)
        {
            if (isoStore.Contains(key))
            {
                isoStore.Remove(key);
            }
            isoStore.Save();
        }

        private void btnGravar_Click(object sender, RoutedEventArgs e)
        {
            this.SetSettingValue("email", txtemail.Text);
            this.SetSettingValue("twitter", txttwitter.Text);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string email, twitter;

            if (isoStore.TryGetValue<string>("email", out email))
            {
                txtemail.Text = email;
            }

            if (isoStore.TryGetValue<string>("twitter", out twitter))
            {
                txttwitter.Text = twitter;
            }
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveSettingValue("email");
            this.RemoveSettingValue("twitter");

            txtemail.Text = string.Empty;
            txttwitter.Text = string.Empty;
        }
    }
}