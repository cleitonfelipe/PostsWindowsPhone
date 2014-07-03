using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using IsoFile.Resources;
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace IsoFile
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoChooserTask chooser;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            chooser = new PhotoChooserTask();
            chooser.Completed += new EventHandler<PhotoResult>(chooser_Completed);
        }

        void chooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage img = new BitmapImage();
                img.SetSource(e.ChosenPhoto);

                SalvarFoto(img);
                ExibirFoto();
            }
        }

        private void ExibirFoto()
        {
            BitmapImage img = null;
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(@"Imagens\foto.jpg"))
                {
                    using (IsolatedStorageFileStream stream = iso.OpenFile(@"Imagens\foto.jpg", FileMode.Open))
                    {
                        img = new BitmapImage();
                        img.SetSource(stream);
                    }
                }
            }

            image1.Source = img;
        }

        private void SalvarFoto(BitmapImage img)
        {
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                WriteableBitmap bmp = new WriteableBitmap(img);

                if (!iso.DirectoryExists("Imagens"))
                {
                    iso.CreateDirectory("Imagens");
                }

                using (IsolatedStorageFileStream stream = iso.OpenFile(@"Imagens\foto.jpg", FileMode.OpenOrCreate))
                {
                    Extensions.SaveJpeg(
                                            bmp,
                                            stream,
                                            bmp.PixelWidth,
                                            bmp.PixelHeight,
                                            0,
                                            100
                                        );
                }
            }
        }

        private void camera_Click(object sender, EventArgs e)
        {
            chooser.Show();
        }

        private void excluir_Click(object sender, EventArgs e)
        {
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                iso.Remove();
            }

            ExibirFoto();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ExibirFoto();
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}