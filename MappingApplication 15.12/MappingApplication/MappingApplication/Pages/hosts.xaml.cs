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

namespace MappingApplication.Pages
{
    public partial class hosts : PhoneApplicationPage
    {
        GeoCoordinate tempCoord = new GeoCoordinate(); //LAt/Long coordinates of the cities
        Ellipse hostCircle = new Ellipse(); //overlay used to show city location
        MapOverlay hostOverlay = new MapOverlay(); //MapOverlay, used to overlay the map
        MapLayer hostLayer = new MapLayer(); //used to add a new layer to the map.

        public hosts()
        {
		//constructor
            InitializeComponent(); //method that sets up data on the view
            createhostCircle(); //calls method to create elipses for hosts
        }

        private void createhostCircle()
        {
            //create the circle overlay. These attributes don't change once they are created
            hostCircle.Fill = new SolidColorBrush(Colors.Blue);
            hostCircle.Height = 10;
            hostCircle.Width = hostCircle.Height;
            hostCircle.Opacity = 50; //half transparent
        }

        private void hostStats(string country, string city, string year, string medals)
        {
		//method to display information about the hosts. When method is called string data
		//is passed in and displayed to the user. One method ensures that it always works
            MessageBox.Show("Country: " + country + "\nCity: " + city + "\nYear: " + year + "\nMedals: " + medals, "Game Stats", MessageBoxButton.OK);
        }

        private void clearOverlay()
        {
		//clears what is on the screen
            tempCoord = null; //resets to coordinates
            hostLayer.Remove(hostOverlay); //removes the overlay
            hostMap.Layers.Remove(hostLayer); //removes the layer
        }

        private void setupOverlay(GeoCoordinate coord)
        {
            try
            {
                //set the temporary coordinates to the passed parameters
                tempCoord = coord;
                //centre map to passed coordinated
                this.hostMap.Center = tempCoord;
                this.hostMap.ZoomLevel = 8;
                //create layer and attributes
                hostOverlay.Content = hostCircle;
                hostOverlay.PositionOrigin = new Point(0.5, 0.5);
                hostOverlay.GeoCoordinate = tempCoord;
                //add layer to the map
                hostLayer.Add(hostOverlay);
                hostMap.Layers.Add(hostLayer);
            }
            catch
            {
            }
        }





















#region eventlisteners

        private void appDiary_Click(object sender, EventArgs e)
        {
            //changes view to the "diary" page
            NavigationService.Navigate(new Uri("/Pages/Diary.xaml", UriKind.Relative));
        }

        private void newDelhi_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to New Delhi. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.NewDelhiCoord);
            hostStats("India", "New Delhi", "2010", "26 (9/10/7)");

        }

        private void melbourne_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Melbourne. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.MelbourneCoord);
            hostStats("Australia", "Melbourne", "2006", "29 (11/7/11)");
        }

        private void manchester_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Manchester. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.ManchesterCoord);
            hostStats("England", "Manchester", "2002", "30 (6/8/16)");
        }

        private void kualaLumpur_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Kuala Lumpur. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.KualaLumparCoord);
            hostStats("Malaysia", "Kuala Lumpur", "1998", "12 (3/2/7)");
        }

        private void victoria_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Victoria. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.VictoriaCoord);
            hostStats("Canada", "Victoria", "1994", "20 (6/3/11)");
        }

        private void auckNZ_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Auckland. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.AucklandCoord);
            hostStats("New Zealand", "Auckland", "1990", "22 (5/7/10)");
        }

        private void edinburgh_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Edinburgh. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.EdinburghCoord);
            hostStats("Scotland", "Edinburgh", "1986", "33 (3/12/18)");
        }

        private void brisbane_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Brisbane. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.BrisbaneCoord);
            hostStats("Australia", "Brisbane", "2010", "26 (8/6/12)");
        }

        private void edmontom_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Edmontom. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.EdmontonCoord);
            hostStats("Canada", "Edmontom", "2010", "14 (3/6/5)");
        }

        private void christChurch_Click(object sender, EventArgs e)
        {
		//event listener that changes map view to Christ Church. Displays
		//associated information about the games hosted by them.
            clearOverlay();
            setupOverlay(AppConstants.ChristChurchCoord);
            hostStats("New Zealand", "Christ Church", "2010", "19 (3/5/11)");
        }

#endregion

    }
}
 /*
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