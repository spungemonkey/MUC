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

        // Constructor
        public MainPage()
        {
            //constructor
            InitializeComponent(); //Loads the page/view associated with the class (MainPage)
            ShowMyLocationOnTheMap(); //Calls method to find users location
            setupArrays();
            insertVenues(); //calls method to draw the venues and information on screen
        }

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

    }
}



//myCirle.DoulbeTap = venueBox("passedName", "passedEvent", "passedAddress");
//private void venueBox(string name, string events, string address) { show (name event addres) }









/*
//Glasgow
    this.mainMap.Center = new GeoCoordinate(55.8580, -4.2590);

//New Delhi 
    this.mainMap.Center = new GeoCoordinate(28.633349, 77.226226);

//Melbourne
     this.mainMap.Center = new GeoCoordinate(-37.814361, 144.963091);

 //Manchester
    this.mainMap.Center = new GeoCoordinate(53.479329, -2.248486);

 //Kuala Lumpar
     this.mainMap.Center = new GeoCoordinate(3.138992, 101.686851);

 //Victoria
    this.mainMap.Center = new GeoCoordinate(48.428199, -123.365739);
    

 //Auckland (New Zealand)	
    this.mainMap.Center = new GeoCoordinate(-36.848487, 174.763286);


//Edinburgh (Scotland)	
    this.mainMap.Center = new GeoCoordinate(55.952372, -3.188764);


//Brisbane (Australia)	
    this.mainMap.Center = new GeoCoordinate(-27.470925, 153.023519);


//Edmonton (Canada)	
    this.mainMap.Center = new GeoCoordinate(53.544362, -113.490897);

//Christchurch (New Zealand)	
    this.mainMap.Center = new GeoCoordinate(-43.532006, 172.636268);
*/











/*
 this.mainMap.Center = new GeoCoordinate(56.49314,-2.74544);
 this.mainMap.Center = new GeoCoordinate(55.794273,-4.219052);
 this.mainMap.Center = new GeoCoordinate(55.849734,-4.205625);
 this.mainMap.Center = new GeoCoordinate(55.84694,-4.207384);
 this.mainMap.Center = new GeoCoordinate(55.845831,-4.23696);
 this.mainMap.Center = new GeoCoordinate(55.825515,-4.25197);
 this.mainMap.Center = new GeoCoordinate(55.853372,-4.309069);
 this.mainMap.Center = new GeoCoordinate(55.867895,-4.288194);
 this.mainMap.Center = new GeoCoordinate(55.939923,-3.173174);
 this.mainMap.Center = new GeoCoordinate(55.880583,-4.340808);
 this.mainMap.Center = new GeoCoordinate(55.860923,-4.287418);
 this.mainMap.Center = new GeoCoordinate(55.7971971,-4.0342997);
 this.mainMap.Center = new GeoCoordinate(55.845591,-4.175798);

Barry Buddons Shooting Centre
Cathkin Braes Mountain Bike Trails
Celtic Park
Emirates Arena including the Sir Chris Hoy Velfrome
Glasgow National Hockey Centre
Hampden Park
Ibrox Stadium
Kelvingrove Lawn Bowls Centre
Royal Commonwealth Pool
Scotson Sports Campus
SECC Precint
Strathclyde Country Park
Tollcross International Swimming Centre

            //MapLayer[] vlayer = new MapLayer[AppConstants.NumVenue];
            //MapOverlay[] vOverlay = new MapOverlay[AppConstants.NumVenue];
            //Polygon[] vPoly = new Polygon[AppConstants.NumVenue];
            //BitmapImage[] venues = new BitmapImage[AppConstants.NumVenue];
            //ImageBrush[] venBrush = new ImageBrush[AppConstants.NumVenue];
            //venues[0] = new BitmapImage(new Uri("/Icons/Venues/ven1.png", UriKind.Relative));
            //venues[1] = new BitmapImage(new Uri("/Icons/Venues/ven1.png", UriKind.Relative));
            //venues[2] = new BitmapImage(new Uri("/Icons/Venues/ven1.png", UriKind.Relative));
            //venues[3] = new BitmapImage(new Uri("/Icons/Venues/ven1.png", UriKind.Relative));
            //venues[4] = new BitmapImage(new Uri("/Icons/Venues/ven1.png", UriKind.Relative));



            //for (int i = 0; i < 5; i++)
            //{
            //    vPoly[i].Points.Add(new Point(0, 0));
            //    vPoly[i].Points.Add(new Point(76, 0));
            //    vPoly[i].Points.Add(new Point(38, 38));
            //    //event listener
            //    venues[i].UriSource = new Uri("/Icons/Venues/ven" + i + ".png", UriKind.Relative);
            //    venBrush[i].ImageSource = venues[i];
            //    vPoly[i].Fill = venBrush[i];
            //    vOverlay[i].Content = vPoly[i];
            //    vOverlay[i].GeoCoordinate = venCoords[i];
            //    vlayer[i].Add(vOverlay[i]);
            //    mainMap.Layers.Add(vlayer[i]);
            //}  
 
  
  
  
*/


