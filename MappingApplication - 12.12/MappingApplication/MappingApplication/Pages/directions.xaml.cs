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

namespace MappingApplication.Pages
{
    public partial class directions : PhoneApplicationPage
    {

      /*  RouteQuery routeQuery = null;
        GeocodeQuery gcQuery = null;
        List<GeoCoordinate> destCoords = new List<GeoCoordinate>();


        public directions()
        {
            InitializeComponent();
        }

        
                             gcQuery = new GeocodeQuery();
                    //gcQuery.SearchTerm = "55.849734, -4.205625";
                    gcQuery.GeoCoordinate = new GeoCoordinate(55.849734, -4.205625);
                    gcQuery.QueryCompleted += gcQuery_QueryCompleted;
                    gcQuery.QueryAsync();
       

        void gcQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                routeQuery = new RouteQuery();
                destCoords.Add(e.Result[0].GeoCoordinate);
                routeQuery.Waypoints = destCoords;
                routeQuery.QueryCompleted += routeQuery_QueryCompleted;
                routeQuery.QueryAsync();
                gcQuery.Dispose();
            }
        }

        void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            if (e.Error == null)
            {
                Route route = e.Result;
                MapRoute mapRoute = new MapRoute(mapRoute);
                mainMap.AddRoute(mapRoute);
                gcQuery.Dispose();
            }
        }  */
    }
}