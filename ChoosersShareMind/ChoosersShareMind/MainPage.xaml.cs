using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ChoosersShareMind.Resources;
using Microsoft.Phone.Tasks;

namespace ChoosersShareMind
{
    public partial class MainPage : PhoneApplicationPage
    {
        //TIPO DO OBJETO QUE IREMOS UTILIZAR
        AddressChooserTask chooser;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            chooser = new AddressChooserTask();
            chooser.Completed += new EventHandler<AddressResult>(chooser_Completed);
            chooser.Show();
        }
        void chooser_Completed(object sender, AddressResult e)
        {
            MessageBox.Show("Nome: " + e.DisplayName + "\nEndereço: " + e.Address);
        }
    }
}