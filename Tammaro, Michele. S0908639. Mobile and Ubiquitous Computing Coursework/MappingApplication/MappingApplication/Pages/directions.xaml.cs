using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Device.Location;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Tasks;
using MappingApplication.Resources;
using Windows.Devices.Geolocation;
using System.Windows.Media.Imaging;

namespace MappingApplication.Pages
{
    public partial class directions : PhoneApplicationPage
    {

        GeoCoordinate[] venCoords = new GeoCoordinate[AppConstants.NumVenue];
        string[] venInfo = new string[AppConstants.NumVenue];


        public directions()
        {
            InitializeComponent();
            getDirections();
        }

        private void getDirections()
        {
            MapsDirectionsTask mapDirections = new MapsDirectionsTask();

            LabeledMapLocation start = new LabeledMapLocation("Label", AppConstants.ven1);
            LabeledMapLocation end = new LabeledMapLocation("Label 2", AppConstants.ven2);
           // mapDirections.Start = start;
           // mapDirections.End = end;
            mapDirections.Show();
        }


        private void appMap_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}