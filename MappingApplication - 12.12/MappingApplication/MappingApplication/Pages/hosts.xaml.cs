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
        GeoCoordinate tempCoord = new GeoCoordinate();
        Ellipse hostCircle = new Ellipse();
        MapOverlay hostOverlay = new MapOverlay();
        MapLayer hostLayer = new MapLayer();

        public hosts()
        {
            InitializeComponent();
            createhostCircle();
        }

        private void createhostCircle()
        {
            //create the circle overlay. These attributes don't change once they are created
            hostCircle.Fill = new SolidColorBrush(Colors.Blue);
            hostCircle.Height = 20;
            hostCircle.Width = 20;
            hostCircle.Opacity = 50;
        }

        private void hostStats(string country, string city, string year, string medals)
        {
            MessageBox.Show("Country: " + country + "\nCity: " + city + "\nYear: " + year + "\nMedals: " + medals, "Game Stats", MessageBoxButton.OK);
        }

        private void clearOverlay()
        {
            tempCoord = null;
            hostLayer.Remove(hostOverlay);
            hostMap.Layers.Remove(hostLayer);
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

        private void appDiary_Click(object sender, EventArgs e)
        {
            //changes view to the "diary" page
            NavigationService.Navigate(new Uri("/Pages/Diary.xaml", UriKind.Relative));
        }

        private void newDelhi_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(28.633349, 77.226226));
            hostStats("India", "New Delhi", "2010", "26 (9/10/7)");

        }

        private void melbourne_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(-37.814361, 144.963091));
            hostStats("Australia", "Melbourne", "2006", "29 (11/7/11)");
        }

        private void manchester_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(53.479329, -2.248486));
            hostStats("England", "Manchester", "2002", "30 (6/8/16)");
        }

        private void kualaLumpur_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(3.138992, 101.686851));
            hostStats("Malaysia", "Kuala Lumpur", "1998", "12 (3/2/7)");
        }

        private void victoria_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(48.428199, -123.365739));
            hostStats("Canada", "Victoria", "1994", "20 (6/3/11)");
        }

        private void auckNZ_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(-36.848487, 174.763286));
            hostStats("New Zealand", "Auckland", "1990", "22 (5/7/10)");
        }

        private void edinburgh_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(55.952372, -3.188764));
            hostStats("Scotland", "Edinburgh", "1986", "33 (3/12/18)");
        }

        private void brisbane_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(-27.470925, 153.023519));
            hostStats("Australia", "Brisbane", "2010", "26 (8/6/12)");
        }

        private void edmontom_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(53.544362, -113.490897));
            hostStats("Canada", "Edmontom", "2010", "14 (3/6/5)");
        }

        private void christChurch_Click(object sender, EventArgs e)
        {
            clearOverlay();
            setupOverlay(new GeoCoordinate(-43.532006, 172.636268));
            hostStats("New Zealand", "Christ Church", "2010", "19 (3/5/11)");
        }

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