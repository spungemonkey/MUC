using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media.Imaging;

namespace MappingApplication.Pages
{
    public partial class hosts : PhoneApplicationPage
    {

        GeoCoordinate[] hostCoord = new GeoCoordinate[AppConstants.NumHosts - 1];       //sets up an array of coordinates. Size is set to one less than total to exclude Glasgow
        string[] hostInfo = new string[AppConstants.NumHosts - 1];                      //sets up array of strings to contain information. 


        public hosts()
        {
		//constructor
            InitializeComponent();          //method that sets up data on the view
            setupArrays();                  //calls method to assign constants to indexes in the array
            insertHosts();                  //calls method to draw the venues and information on screen
        }


        private void setupArrays()
        {
            //sets up each coordinate in the array to one of the constants in App Constants
            hostCoord[0] = AppConstants.NewDelhiCoord;
            hostCoord[1] = AppConstants.MelbourneCoord;
            hostCoord[2] = AppConstants.ManchesterCoord;
            hostCoord[3] = AppConstants.KualaLumparCoord;
            hostCoord[4] = AppConstants.VictoriaCoord;
            hostCoord[5] = AppConstants.AucklandCoord;
            hostCoord[6] = AppConstants.EdinburghCoord;
            hostCoord[7] = AppConstants.BrisbaneCoord;
            hostCoord[8] = AppConstants.EdmontonCoord;
            hostCoord[9] = AppConstants.ChristChurchCoord;

            //sets up each string in the array to one of the constants in App Constants
            hostInfo[0] = AppConstants.NewDelhiInfo;
            hostInfo[1] = AppConstants.MelbourneInfo;
            hostInfo[2] = AppConstants.ManchesterInfo;
            hostInfo[3] = AppConstants.KualaLumparInfo;
            hostInfo[4] = AppConstants.VictoriaInfo;
            hostInfo[5] = AppConstants.AucklandInfo;
            hostInfo[6] = AppConstants.EdinburghInfo;
            hostInfo[7] = AppConstants.BrisbaneInfo;
            hostInfo[8] = AppConstants.EdmontonInfo;
            hostInfo[9] = AppConstants.ChristChurchInfo;

        }


        private void insertHosts()
        {
            BitmapImage[] hosts = new BitmapImage[AppConstants.NumHosts-1];                             //creates array of images to use as overlays

            for (int i = 0; i < AppConstants.NumHosts - 1; i++)                                         //loops through images
            {
                hosts[i] = new BitmapImage(new Uri("/Icons/Venues/hostPin.png", UriKind.Relative));     //uses the same image for each pin
                drawHosts(hosts[i], hostCoord[i], hostInfo[i]);                                         //calls method. Passes in the host image, host coordinates
                                                                                                        // and host information, indexed to same position int he 3 arrays
            }
        }

        private void drawHosts(BitmapImage host, GeoCoordinate hostcoord, string hoststring)
        {
            MapLayer hLayer = new MapLayer();                                                           //creates new layer
            MapOverlay hOverlay = new MapOverlay();                                                     //creates new overlay
            Polygon hPoly = new Polygon();                                                              //creates new polygon (blank canvas)
            hPoly.Points.Add(new Point(0, 0));                                                          //Sets first point in triangle
            hPoly.Points.Add(new Point(38, 0));                                                         //sets second point
            hPoly.Points.Add(new Point(19, 19));                                                        //sets third point
            hPoly.Tap += (sender, e) => { vPoly_DoubleTap(sender, e, hoststring); };                     //calls attached event handler when the poly is clicked, passing in venstring

            ImageBrush venBrush = new ImageBrush();                                                     //used to paint an area (or polygon)
            venBrush.ImageSource = host;                                                                //sets source to passed in image
            hPoly.Fill = venBrush;                                                                      //fills the polygon wth the brush
            hOverlay.Content = hPoly;                                                                   //sets the content of the overlay to the filled poly
            hOverlay.PositionOrigin = new Point(0.5, 0.5);                                              //sets origin
            hOverlay.GeoCoordinate = hostcoord;                                                         //sets coordinates to passed coordinates
            hLayer.Add(hOverlay);                                                                       //adds overlay to map
            hostMap.Layers.Add(hLayer);                                                                 //adds layer to map
        }


        void vPoly_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e, string vInfo)      //single event attached to polygons
        {
            MessageBox.Show(vInfo, "Host Information", MessageBoxButton.OK);                            //displays a message box with information pertaining to the games
        }




        private void appDiary_Click(object sender, EventArgs e)
        {
            //changes view to the "diary" page
            NavigationService.Navigate(new Uri("/Pages/Diary.xaml", UriKind.Relative));
        }

        private void map_Click(object sender, EventArgs e)
        {
            //changes view to the "map" page
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

#region EventListenersForCities

        private void newDelhi_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to New Delhi. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.NewDelhiCoord;           //Sets centre of map to New Delhi
            this.hostMap.ZoomLevel = 15;                                //Zoom Level

        }

        private void melbourne_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Melbourne. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.MelbourneCoord;          //Sets centre of map to Melbourne
            this.hostMap.ZoomLevel = 15;
        }

        private void manchester_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Manchester. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.ManchesterCoord;         //Sets centre of map to Manchester
            this.hostMap.ZoomLevel = 15;
        }

        private void kualaLumpur_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Kuala Lumpur. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.KualaLumparCoord;        //Sets centre of map to Kuala Lumpar
            this.hostMap.ZoomLevel = 15;
        }

        private void victoria_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Victoria. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.VictoriaCoord;           //Sets centre of map to Victoria
            this.hostMap.ZoomLevel = 15;
        }

        private void auckNZ_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Auckland. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.AucklandCoord;           //Sets centre of map to Auckland
            this.hostMap.ZoomLevel = 15;
        }

        private void edinburgh_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Edinburgh. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.EdinburghCoord;          //Sets centre of map to Edinburgh
            this.hostMap.ZoomLevel = 15;
        }

        private void brisbane_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Brisbane. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.BrisbaneCoord;           //Sets centre of map to New Delhi
            this.hostMap.ZoomLevel = 15;
        }

        private void edmontom_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Edmontom. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.EdmontonCoord;           //Sets centre of map to Edmonton
            this.hostMap.ZoomLevel = 15;
        }

        private void christChurch_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Christ Church. Displays
		//associated information about the games hosted by them.
            this.hostMap.Center = AppConstants.ChristChurchCoord;       //Sets centre of map to New Delhi
            this.hostMap.ZoomLevel = 15;
        }

#endregion

    }
}
