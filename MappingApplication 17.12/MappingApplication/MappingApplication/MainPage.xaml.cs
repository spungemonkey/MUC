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
using Microsoft.Phone.Tasks;

namespace MappingApplication
{
    public partial class MainPage : PhoneApplicationPage
    {

        //Geo properties needed to find user location
        Geolocator gLocator = new Geolocator();                                         //new instance of Geolocator. Provides access to current geographic location
        Geoposition gPosition = null;                                                   //new Geoposition. Represents a location that contains lat/long data
        Geocoordinate gcCoords = null;                                                  //new Geocoordinate. contains information for identifying the geographic location
        GeoCoordinate gCCoordinate = null;                                              //newGeoCordinate. Represents a location axpressed as a geographic coordinate
        GeoCoordinate defaultCoord = AppConstants.GlasgowCoord;                         //default coordinates (Glasgow City Centre) are used if location services are off
        GeoCoordinate[] venCoords = new GeoCoordinate[AppConstants.NumVenue];           //setup an array to store the coordinates of all venues
        string[] venInfo = new string[AppConstants.NumVenue];                           //set up an array to store the venue information


        private int cartNo = 0;                         //int used to change between cartographic modes
        private bool lightsOn = true;                   //last one out switch the lights out

        // Constructor
        public MainPage()
        {
            //constructor
            InitializeComponent();                  //Loads the page/view associated with the class (MainPage)
            this.mainMap.Center = AppConstants.GlasgowCoord;                                                                                //Coordinates of Glasgow set to the map centre
            this.mainMap.ZoomLevel = 10;     
            ShowUserLocation();               //Calls method to find users location
            setupArrays();                          //calls method to assign constants to indexes in the array
            insertVenues();                         //calls method to draw the venues and information on screen
        }


        private void setupArrays()
        {
            //sets up each coordinate in the array to one of the constants in App Constants
            venCoords[0] = AppConstants.barBud;
            venCoords[1] = AppConstants.cathBrae;
            venCoords[2] = AppConstants.celtPark;
            venCoords[3] = AppConstants.chVel;
            venCoords[4] = AppConstants.hockCen;
            venCoords[5] = AppConstants.hampPark;
            venCoords[6] = AppConstants.ibrox;
            venCoords[7] = AppConstants.kelvinBowls;
            venCoords[8] = AppConstants.royalPool;
            venCoords[9] = AppConstants.scotCampus;
            venCoords[10] = AppConstants.seccPrecinct;
            venCoords[11] = AppConstants.strathclydePark;
            venCoords[12] = AppConstants.tollcrossSwim;

            //sets up each string in the array to one of the constants in App Constants
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

        private async void ShowUserLocation()
        {
            try
            {
                //tries to find the users location using location capability on the phone
                //creates geoposition
                gPosition = await gLocator.GetGeopositionAsync();
                gcCoords = gPosition.Coordinate;                                        //converts Geocoordinate to GeoCoordinate
                gCCoordinate = CoordinateConverter.ConvertGeocoordinate(gcCoords);      //Sets GeoCoordinate to the locational coordinates of the user

                //Centres the map on the found location               
                this.mainMap.Center = gCCoordinate;                                     //centres the map with the location coordinates
                this.mainMap.ZoomLevel = 13;                                            //sets zoom level


                Ellipse markers = new Ellipse();                                        //creates an ellipses to mark user location
                markers.Fill = new SolidColorBrush(Colors.Blue);                        //sets colour to blue
                markers.Height = 10;                                                    //sets height to 10 pixels
                markers.Width = markers.Height;                                         //equates the height to the width to create a circle
                markers.Opacity = 50;                                                   //semi transparant

                //overlays the map with the circle
                MapOverlay locOverlay = new MapOverlay();                               //creates a new overlay                          
                locOverlay.Content = markers;                                           //sets overlay to the marker
                locOverlay.PositionOrigin = new Point(0.5, 0.5);                        //sets origin as the centre of the circle
                locOverlay.GeoCoordinate = gCCoordinate;                                //Sets geocoordinate of the overlay to the found GeoCoordinatw

                //creates a new layer and draws the circle
                MapLayer myLocationLayer = new MapLayer();                              //creates new MapLayer
                myLocationLayer.Add(locOverlay);                                        //adds overlay
                mainMap.Layers.Add(myLocationLayer);                                    //adds a new layer with the overlay
            }

            catch (UnauthorizedAccessException)
            {
                //If location services are disabled then it centres the map on Glasgow City Centre
                MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.", "Warning", MessageBoxButton.OK);     //Message box shown to the user
                this.mainMap.Center = AppConstants.GlasgowCoord; ;                                                                              //Lat/Long coordinates of Glasgow
                this.mainMap.ZoomLevel = 15;                                                                                                    //Zoom level

            }

            catch (Exception ex)
            {
                // Something else happened while acquiring the location. If this catch is thrown it centres the map on Glasgow City Centre
                MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.", "Warning", MessageBoxButton.OK);     //warning message
                this.mainMap.Center = AppConstants.GlasgowCoord;                                                                                //Coordinates of Glasgow set to the map centre
                this.mainMap.ZoomLevel = 15;                                                                                                    //zoom level
            }
        }

        private void insertVenues()
        {
            BitmapImage[] venues = new BitmapImage[AppConstants.NumVenue];                                  //creates array of images to use as overlays

            for (int i = 0; i < AppConstants.NumVenue; i++)                                                 //loops through through array of images
            {
                venues[i] = new BitmapImage(new Uri("/Icons/Venues/ven" + i + ".png", UriKind.Relative));   //there are 13 images used to represent the venues. We assign a numbered resource
                                                                                                            //to each index in the array by using the arrays index.
                drawVen(venues[i], venCoords[i], venInfo[i]);                                               //call method. Pass in each ven, its coordinates and info using the array index.                                  
            }
        }

        private void drawVen(BitmapImage venue, GeoCoordinate venuecoord, string venstring)             //method
        {
            //code taken from stackoverflow.com/questions/14797456/how-to-create-auto-scalling-image-lying-on-wp8-maps?rq=1
            //changes made as BitmapImage i passed as a parameter from an array
            //Overlay coordinates also passed 
            //Attached an event to the polygon also

            MapLayer vLayer = new MapLayer();                                                           //creates new layer
            MapOverlay vOverlay = new MapOverlay();                                                     //creates new overlay
            Polygon vPoly = new Polygon();                                                              //creates new polygon (blank canvas)
            vPoly.Points.Add(new Point(0, 0));                                                          //Sets first point in triangle
            vPoly.Points.Add(new Point(76, 0));                                                         //sets second point
            vPoly.Points.Add(new Point(38, 38));                                                        //sets third point
            vPoly.Tap += (sender, e) => { vPoly_DoubleTap(sender, e, venstring); };                     //calls attached event handler when the poly is clicked, passing in venstring

            ImageBrush venBrush = new ImageBrush();                                                     //used to paint an area (or polygon)
            venBrush.ImageSource = venue;                                                               //sets source to passed in image
            vPoly.Fill = venBrush;                                                                      //fills the polygon wth the brush
            vOverlay.Content = vPoly;                                                                   //sets the content of the overlay to the filled poly
            vOverlay.PositionOrigin = new Point(0.5, 0.5);                                              //sets origin
            vOverlay.GeoCoordinate = venuecoord;                                                        //sets coordinates to passed coordinates
            vLayer.Add(vOverlay);                                                                       //adds overlay to map
            mainMap.Layers.Add(vLayer);                                                                 //adds layer to map
        }

        void vPoly_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e, string vInfo)      //single event attached to the polygons
        {
              MessageBox.Show(vInfo, "Venue Information", MessageBoxButton.OK);                         //displays a message box. Displays the information from the venue
        }

        #region Event Handlers
        private void directions_Click(object sender, EventArgs e)
        {
            //Code taken from msdn.microsoft.com/en-us/library/windowsphone/develop/jj207044(v=vs.105).aspx
            //No code was copied, however implemented using this as a guide
            MapsDirectionsTask mapDirections = new MapsDirectionsTask();                            //creates new instance of MapsDirectionTask. Displays driving directions

            LabeledMapLocation defStart = new LabeledMapLocation("<Enter Start Point>", null);      //initialises and nulls the start point to allow user to specify start point
            LabeledMapLocation defEnd = new LabeledMapLocation("<Enter End Point>", null);          //initialises and nulls end point to allow user to specify

            mapDirections.Start = defStart;                                                         //sets Start point
            mapDirections.End = defEnd;                                                             //Sets end point
            mapDirections.Show();                                                                   //Shows the mapsDirection. This NEEDS to be called otherwise the Maps direction task CANNOT run
        }

        private void prevHosts_Click(object sender, EventArgs e)
        {
			//event listener that changes view to the "previous hosts" page
            NavigationService.Navigate(new Uri("/Pages/hosts.xaml", UriKind.Relative));
        }

        private void cartMode_Click(object sender, EventArgs e)             //handles request to change the Map Cartographic type
        {
            cartNo++;                                                               //increments the counter
            switch (cartNo)                                                         //case statement start. determines what mode is enabled depending on what number is on the counter
            {
                case 0:
                    mainMap.CartographicMode = MapCartographicMode.Road;            //default road
                break;

                case 1:
                    mainMap.CartographicMode = MapCartographicMode.Aerial;          //Aerial (satellite) view
                break;

                case 2:
                    mainMap.CartographicMode = MapCartographicMode.Hybrid;          //Hybride, overlays Aerial with road maps
                break;
            
                case 3:
                    mainMap.CartographicMode = MapCartographicMode.Terrain;         //shows changes in terrain
                break;
            
                default:
                    cartNo = -1;                                                    //else resets the value to -1. This is change to -1 to stop the road view from not appearing
                break;
            }
        }

        private void lightMode_Click(object sender, EventArgs e)            //handles requst to change colour mode
        {
            lightsOn = !lightsOn;                                           //changes valye to opposite value

            if (lightsOn)                                                   //checks if value is true, sets color mode to light
            {
                mainMap.ColorMode = MapColorMode.Light;
            }
            else                                                            //else sets it to dark
            {
                mainMap.ColorMode = MapColorMode.Dark;
            }
        }

        private void appDiary_Click(object sender, EventArgs e)
        {
            //event listener that changes view to the "diary" page
            NavigationService.Navigate(new Uri("/Pages/Diary.xaml", UriKind.Relative));
        }
        #endregion

    }


    //NOTE:
    //A class (RotationHelper) was included in this project, but has now been excluded
    //This class was copied from a tutorial I found at developer.nokia.com/Community/Wiki/Real-time_rotation_of_the_Windows_Phone_8_Map_Control
    //one line of code was altered for debugging purposes, however the code is as it was
    //in that tutorial. I have excluded from this project
}