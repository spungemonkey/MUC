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
		
		//BitmapImage celticPark = new BitmapImage();


        // Constructor
        public MainPage()
        {
            //constructor
            InitializeComponent(); //Loads the page/view associated with the class (MainPage)
            ShowMyLocationOnTheMap(); //Calls method to find users location
            insertVenues(); //calls method to draw the venues and information on screen
        }

        private void displayVenueInfo(string vName, string vAddress, string vEvent) //dedicated method used to show Venue information. Allows code to pass in arguments
        {
            MessageBox.Show("Venue: " + vName + "\nAddress: " + vAddress + "\nEvent(s): " + vEvent + ".", "Information", MessageBoxButton.OK);
            //set up events and call this method
        }

        private void insertVenues()
        {
            //Button but = new Button();
            //but.Content = "CFC";
            //but.Height = 20;
            //but.Width = 20;
            //but.Opacity = 50;
            //MapOverlay butOverlay = new MapOverlay();
            //butOverlay.Content = but;
            //butOverlay.PositionOrigin = new Point(0.5, 0.5);
            //butOverlay.GeoCoordinate = new GeoCoordinate(55.849734, -4.205625);
            //MapLayer butLayer = new MapLayer();
            //butLayer.Add(butOverlay);
            //mainMap.Layers.Add(butLayer);
			
			Image[] venues = new Image[AppConstants.NumVenue];
			MapOverlay vOverlay = new MapOverlay();
			MapLayer vLayer = new MapLayer();
			
			//in App constants I need to put the venues and all of 
			//their coordiantes in an array like I have done in GP3.
			
			for (int i=0; i < AppConstants.NumVenue; i++)
			{
				venues[i].CreateOptions = BitmapCreateOptions.None;
				venues[i].UriSouce = new Uri(".\\Assets\\Venues\\ven" & i & ".png", UriKind.Relative);
				venues[i].ImageOpened += (a,b) =>
				{
				MainImage.Source = venues[i];
				}
				
				vOverlay.Content = venues[i];
				vOverlay.GeoCoordinate = VenCoord[i];
				vLayer.Add(venues[i]);
				mainMap.Layers.Add(vLayer);
				
				
			}
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
                    this.mainMap.Center = new GeoCoordinate(55.8580, -4.2590); //Lat/Long coordinates of Glasgow
                    this.mainMap.ZoomLevel = 12;

                }   

                catch (Exception ex)
                {
                    // Something else happened while acquiring the location. If this catch is thrown it centres the map on Glasgow City Centre
                    MessageBox.Show("Location is disabled in phone settings or capabilities are not checked.");
                    this.mainMap.Center = new GeoCoordinate(55.8580, -4.2590);
                    this.mainMap.ZoomLevel = 12;
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

*/