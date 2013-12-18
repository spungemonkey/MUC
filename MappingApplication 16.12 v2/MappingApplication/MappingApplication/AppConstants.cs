using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingApplication
{
    class AppConstants
    {
        //Constant values needed by multiple classes.
        //variables are public and static to allow them to be accessed

        public const int NumVenue = 13;         //total number of venues
        public const int NumHosts = 11;         //number of hosts


        //Coordinates of the hosts
        public static GeoCoordinate GlasgowCoord = new GeoCoordinate(55.8580, -4.2590);                 //Glasgow Coordinates
        public static GeoCoordinate NewDelhiCoord = new GeoCoordinate(28.633349, 77.226226);            //New Delhi Coordinates
        public static GeoCoordinate MelbourneCoord = new GeoCoordinate(-37.814361, 144.963091);         //Melbourne Coordinates
        public static GeoCoordinate ManchesterCoord = new GeoCoordinate(53.479329, -2.248486);          //Manchester Coordinates
        public static GeoCoordinate KualaLumparCoord = new GeoCoordinate(3.138992, 101.686851);         //Kuala Lumpar Coordinates
        public static GeoCoordinate VictoriaCoord = new GeoCoordinate(48.428199, -123.365739);          //Victoria Coordinates
        public static GeoCoordinate AucklandCoord = new GeoCoordinate(-36.848487, 174.763286);          //Auckland Coordinates
        public static GeoCoordinate EdinburghCoord = new GeoCoordinate(55.952372, -3.188764);           //Edinburgh Coordinates
        public static GeoCoordinate BrisbaneCoord = new GeoCoordinate(-27.470925, 153.023519);          //Brisbane Coordinates
        public static GeoCoordinate EdmontonCoord = new GeoCoordinate(53.544362, -113.490897);          //Edmonton Coordinates
        public static GeoCoordinate ChristChurchCoord = new GeoCoordinate(-43.532006, 172.636268);      //Christ Church Coordinates

        //Strings that contain the information for each host city, such as country, year and medals won
        public static string NewDelhiInfo = "Country: India\nCity: New Delhi\nYear: 2010\nMedals: 26 (9/10/7)";                     //New Delhi games information
        public static string MelbourneInfo = "Country: Australia\nCity: Melbourne\nYear: 2006\nMedals: 29 (11/7/11)";               //Melbourme games information
        public static string ManchesterInfo = "Country: England\nCity: Manchester\nYear: 2002\nMedals: 30 (6/8/16)";                //Manchester games information
        public static string KualaLumparInfo = "Country: Malaysia\nCity: Kuala Lumpar\nYear: 1998\nMedals: 12 (3/2/7)";             //Kuala Lumpar games information
        public static string VictoriaInfo = "Country: Canada\nCity: Victoria\nYear: 1994\nMedals: 20 (6/3/11)";                     //Victoria games information
        public static string AucklandInfo = "Country: Auckland\nCity: New Zealand\nYear: 1990\nMedals: 22 (7/7/10)";                //Auckland games information
        public static string EdinburghInfo = "Country: Scotland\nCity: Edinburgh\nYear: 1986\nMedals: 33 (3/12/18)";                //Edinburgh games information
        public static string BrisbaneInfo = "Country: Australia\nCity: Brisbane\nYear: 1982\nMedals: 26 (8/6/12)";                  //Brisbane games information
        public static string EdmontonInfo = "Country: Canada\nCity: Edmonton\nYear: 1978\nMedals: 14 (3/6/5)";                      //Edmonton games information
        public static string ChristChurchInfo = "Country: New Zealand\nCity: Christ Church\nYear: 1974\nMedals: 19 (3/5/11)";       //Christchurch games information



        //Coordinates for the venues in 2014
        public static GeoCoordinate barBud = new GeoCoordinate(56.49314, -2.74544);                         //Barry Buddons Shooting Centre Coordinates
        public static GeoCoordinate cathBrae = new GeoCoordinate(55.794273, -4.219052);                     //Cathkin Braes Mountain Bike Trials Coordinates
        public static GeoCoordinate celtPark = new GeoCoordinate(55.849734, -4.205625);                     //Celtic Park Coordinates
        public static GeoCoordinate chVel = new GeoCoordinate(55.84694, -4.207384);                         //Sir Chris Hoy Velodrome Coordinates
        public static GeoCoordinate hockCen = new GeoCoordinate(55.845831, -4.23696);                       //National Hockey Centre Coordinates
        public static GeoCoordinate hampPark = new GeoCoordinate(55.825515, -4.25197);                      //Hampden Stadium Coordinates
        public static GeoCoordinate ibrox = new GeoCoordinate(55.853372, -4.309069);                        //Ibrox Park Coordinates
        public static GeoCoordinate kelvinBowls = new GeoCoordinate(55.867895, -4.288194);                  //Kelvingrove Lawn Bowls Centre Coordinates
        public static GeoCoordinate royalPool = new GeoCoordinate(55.939923, -3.173174);                    //Royal Commonwealth Pool Coordinates
        public static GeoCoordinate scotCampus = new GeoCoordinate(55.880583, -4.340808);                   //Scotson Sports Campus Coordinates
        public static GeoCoordinate seccPrecinct = new GeoCoordinate(55.860923, -4.287418);                 //SECC Precinct Coordinates
        public static GeoCoordinate strathclydePark = new GeoCoordinate(55.7971971, -4.0342997);            //Strathclyde Country Park Coordinates
        public static GeoCoordinate tollcrossSwim = new GeoCoordinate(55.845591, -4.175798);                //Tollcross International Swimming Centre Coordinates

        //strings that contain venue information, such as name, address and events
        public static string ven0Info = "Venue: Barry Buddons Shooting Centre\nAddress: Angus & Dundee Battalion, DD7 7RY\nEvents: Shooting";                               //Barry Buddons Shooting Centre Coordinates
        public static string ven1Info = "Venue: Cathkin Braes Mountain Bike Trails\nAddress: 201 Ardencraig Road, G45 0HR\nEvents: Mountain Biking";                        //Cathkin Braes Mountain Bike Trials Coordinates
        public static string ven2Info = "Venue: Celtic Park\nAddress: Kerrydale Street, G40 3RE\nEvents: Opening Ceremony";                                                 //Celtic Park Coordinates
        public static string ven3Info = "Venue: Emirates Arena including the Sir Chris Hoy Velodrome\nAddress: 1000 London Road, G40 3HY\nEvents: Cycling, Badminton";      //Sir Chris Hoy Velodrome Coordinates
        public static string ven4Info = "Venue: Glasgow National Hockey Centre\nAddress: 8 Kings Drive, G40 1HB\nEvents: Hockey";                                           //National Hockey Centre Coordinates
        public static string ven5Info = "Venue: Hampden Park\nAddress: Letherby Drive, G42 9BA\nEvents: Athletics";                                                         //Hampden Stadium Coordinates
        public static string ven6Info = "Venue: Ibrox Stadium\nAddress: 150 Edmiston Drive, G51 2XD\nEvents: Rugby Sevens";                                                 //Ibrox Park Coordinates
        public static string ven7Info = "Venue: Kelvingrove Lawn Bowls Centre\nAddress: Kelvin Way, G3 7TA\nEvents: Lawn Bowls";                                            //Kelvingrove Lawn Bowls Centre Coordinates
        public static string ven8Info = "Venue: Royal Commonwealth Pool\nAddress: 21 Dalkeith Road, EH16 5BB\nEvents: Diving";                                              //Royal Commonwealth Pool Coordinates
        public static string ven9Info = "Venue: Scotson Sports Campus\nAddress: 72 Danes Drive, G14 9HD\nEvents: Squash, Table Tennis";                                     //Scotson Sports Campus Coordinates
        public static string ven10Info = "Venue: SECC Precint\nAddress: Exhibition Way, G3 8YW\nEvents: Boxing, Gymnastics, Judo, Netball, Wrestling, Weightlighting";      //SECC Precinct Coordinates
        public static string ven11Info = "Venue: Strathclyde Country Park\nAddress: 366 Hamilton Road, ML1 3ED\nEvents: Triathlon";                                         //Strathclyde Country Park Coordinates
        public static string ven12Info = "Venue: Tollcross International Swimming Centre\nAddress: 350 Wellshot Road, G32 7QR\nEvents: Swimming";                           //Tollcross International Swimming Centre Coordinates


}
}
