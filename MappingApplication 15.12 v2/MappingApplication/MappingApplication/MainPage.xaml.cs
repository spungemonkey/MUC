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
using MappingApplication.Resources;
using Windows.Devices.Geolocation;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using Windows.Phone.Speech.VoiceCommands;

namespace MappingApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
	//create constants class (like GP3) for location data

        //Geo properties needed to find user location
        Geolocator myGeolocator = new Geolocator(); //new instance of Geolocator. Provides access to current geographic location
        Geoposition myGeoposition = null; //new Geoposition. Represents a location that contains lat/long data
        Geocoordinate myGeocoordinate = null; //new Geocoordinate. contains information for identifying the geographic location
        GeoCoordinate myGeoCoordinate = null; //newGeoCordinate. Represents a location axpressed as a geographic coordinate
        GeoCoordinate defaultCoord = AppConstants.GlasgowCoord;
        GeoCoordinate[] venCoords = new GeoCoordinate[AppConstants.NumVenue];
        string[] venInfo = new string[AppConstants.NumVenue];

        double lastAngle;

        private RotationHelper rotationHelper;

        private int cartNo = 0;
        private int lightNo = 0;

        // Constructor
        public MainPage()
        {
            //constructor
            InitializeComponent(); //Loads the page/view associated with the class (MainPage)
            ShowMyLocationOnTheMap(); //Calls method to find users location
            setupArrays(); //calls method to assign constants to objects in the array
            insertVenues(); //calls method to draw the venues and information on screen

            rotationHelper = new RotationHelper(mainMap);
        }







        private void PhoneApplicationPage_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            lastAngle = this.mainMap.Heading;
        }

        void PhoneApplicationPage_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.PinchManipulation != null)
            {
                double angle = angleBetween2Lines(e.PinchManipulation.Original, e.PinchManipulation.Current);
                this.mainMap.SetView(mainMap.Center, mainMap.ZoomLevel, lastAngle + angle, MapAnimationKind.Parabolic);
            }
        }

        public static double angleBetween2Lines(PinchContactPoints line1, PinchContactPoints line2)
        {
            if (line1 != null && line2 != null)
            {
                double angle1 = Math.Atan2(line1.PrimaryContact.Y - line1.SecondaryContact.Y,
                                           line1.PrimaryContact.X - line1.SecondaryContact.X);
                double angle2 = Math.Atan2(line2.PrimaryContact.Y - line2.SecondaryContact.Y,
                                           line2.PrimaryContact.X - line2.SecondaryContact.X);
                return (angle1 - angle2) * 180 / Math.PI;
            }
            else { return Double.NaN; }
        }

      











#region myShit
        private void setupArrays()
        {
            venCoords[0] = AppConstants.ven0;
            venCoords[1] = AppConstants.ven1;
            venCoords[2] = AppConstants.ven2;
            venCoords[3] = AppConstants.ven3;
            venCoords[4] = AppConstants.ven4;
            venCoords[5] = AppConstants.ven5;
            venCoords[6] = AppConstants.ven6;
            venCoords[7] = AppConstants.ven7;
            venCoords[8] = AppConstants.ven8;
            venCoords[9] = AppConstants.ven9;
            venCoords[10] = AppConstants.ven10;
            venCoords[11] = AppConstants.ven11;
            venCoords[12] = AppConstants.ven12;

            venInfo[0] = AppConstants.ven0Info;
            venInfo[1] = AppConstants.ven1Info;
            venInfo[2] = AppConstants.ven2Info;
            venInfo[3] = AppConstants.ven3Info;
            venInfo[4] = AppConstants.ven4Info;
            venInfo[5] = AppConstants.ven5Info;
            venInfo[6] = AppConstants.ven6Info;
            venInfo[7] = AppConstants.ven7Info;
            venInfo[8] = AppConstants.ven8Info;
            venInfo[9] = AppConstants.ven9Info;
            venInfo[10] = AppConstants.ven10Info;
            venInfo[11] = AppConstants.ven11Info;
            venInfo[12] = AppConstants.ven12Info;

        }

        private void drawVen(BitmapImage venue, GeoCoordinate venuecoord, string venstring)
        {
            MapLayer vLayer = new MapLayer();
            MapOverlay vOverlay = new MapOverlay();
            Polygon vPoly = new Polygon();
            vPoly.Points.Add(new Point(0, 0));
            vPoly.Points.Add(new Point(76, 0));
            vPoly.Points.Add(new Point(38, 38));
            vPoly.Tap += (sender, e) => { vPoly_DoubleTap(sender, e, venstring); };

            ImageBrush venBrush = new ImageBrush();
            venBrush.ImageSource = venue;
            vPoly.Fill = venBrush;
            vOverlay.Content = vPoly;
            vOverlay.PositionOrigin = new Point(0.5, 0.5);
            vOverlay.GeoCoordinate = venuecoord;
            vLayer.Add(vOverlay);
            mainMap.Layers.Add(vLayer);
        }

        private void insertVenues()
        {
            BitmapImage[] venues = new BitmapImage[AppConstants.NumVenue];

            for (int i = 0; i < AppConstants.NumVenue; i++)
            {
                venues[i] = new BitmapImage(new Uri("/Icons/Venues/ven" + i + ".png", UriKind.Relative));
                drawVen(venues[i], venCoords[i], venInfo[i]);
            }
		}



        void vPoly_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e, string vInfo)
        {
              MessageBox.Show(vInfo, "Venue Information", MessageBoxButton.OK);
        }
      

	  
        private async void ShowMyLocationOnTheMap()
        {
            try
                {
                    //tries to find the users location using location capability on the phone
                    //creates geoposition
                    myGeoposition = await myGeolocator.GetGeopositionAsync();
                    myGeocoordinate = myGeoposition.Coordinate;
                    myGeoCoordinate = CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);

                    //Centres the map on the found location               
                    this.mainMap.Center = myGeoCoordinate;
                    this.mainMap.ZoomLevel = 13; //sets zoom level

                    //creates a circle on the users location 10x10 pixels and colours it blue
                    Ellipse myCircle = new Ellipse();
                    myCircle.Fill = new SolidColorBrush(Colors.Blue);
                    myCircle.Height = 10;
                    myCircle.Width = myCircle.Height;
                    myCircle.Opacity = 50; //semi transparant
                    
                    //overlays the map with the circle
                    MapOverlay myLocationOverlay = new MapOverlay();
                    myLocationOverlay.Content = myCircle; //sets overlay to circle
                    myLocationOverlay.PositionOrigin = new Point(0.5, 0.5); //sets origin as the centre of the circle
                    myLocationOverlay.GeoCoordinate = myGeoCoordinate; //Sets geocoordinate of the overlay to the found GeoCoordinatw
					
                    //creates a new layer and draws the circle
                    MapLayer myLocationLayer = new MapLayer();
                    myLocationLayer.Add(myLocationOverlay); //adds previously created overlay
                    mainMap.Layers.Add(myLocationLayer);//adds a new layer
                }  
            
                catch (UnauthorizedAccessException)
                {
                    //If location services are disabled then it centres the map on Glasgow City Centre
                    MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
                    this.mainMap.Center = AppConstants.GlasgowCoord; ; //Lat/Long coordinates of Glasgow
                    this.mainMap.ZoomLevel = 15;

                }   

                catch (Exception ex)
                {
                    // Something else happened while acquiring the location. If this catch is thrown it centres the map on Glasgow City Centre
                    MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
                    this.mainMap.Center = AppConstants.GlasgowCoord;
                    this.mainMap.ZoomLevel = 15;
                }
        }



        private void appDiary_Click(object sender, EventArgs e)
        {
            //event listener that changes view to the "diary" page
            NavigationService.Navigate(new Uri("/Pages/Diary.xaml", UriKind.Relative));
        }

        private void appAbout_Click(object sender, EventArgs e)
        {
            //event listener that changes view to the "about" page
            NavigationService.Navigate(new Uri("/Pages/about.xaml", UriKind.Relative));
        }

        private void prevHosts_Click(object sender, EventArgs e)
        {
			//event listener that changes view to the "previous hosts" page
            NavigationService.Navigate(new Uri("/Pages/hosts.xaml", UriKind.Relative));
        }

#endregion

        private void cartMode_Click(object sender, EventArgs e)
        {
            cartNo++;
            switch (cartNo)
            {
                case 0:
                    mainMap.CartographicMode = MapCartographicMode.Road;
                break;

                case 1:
                    mainMap.CartographicMode = MapCartographicMode.Aerial;
                break;

                case 2:
                    mainMap.CartographicMode = MapCartographicMode.Hybrid;
                break;
            
                case 3:
                    mainMap.CartographicMode = MapCartographicMode.Terrain;
                break;
            
                default:
                    cartNo = -1;
                break;
            }
        }

        private void lightMode_Click(object sender, EventArgs e)
        {
            lightNo++;

            switch (lightNo)
            {
                case 0:
                    mainMap.ColorMode = MapColorMode.Light;
                break;

                case 1:
                    mainMap.ColorMode = MapColorMode.Dark;
                break;

                default:
                    lightNo = -1;
                break;
            }

        }

    }
}



//video.ch9.ms/sessions/build/2012/3-050.pptx