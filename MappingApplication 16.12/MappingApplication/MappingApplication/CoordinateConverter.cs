using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location; //provodes geocoordinate class
using Windows.Devices.Geolocation;

namespace MappingApplication
{
    public static class CoordinateConverter //Helper class used to convert Geocoordinate expected by the Map Control
    {
        public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {

            //This code was taken from the tutorial on msdn
            //msdn.microsoft.com/en-us/library/windowsphone/develop/jj207045(v=vs.105).aspx
            //No changes have been made as this is a base method
            return new GeoCoordinate
            (
            geocoordinate.Latitude, 
            geocoordinate.Longitude, 
            geocoordinate.Altitude ?? Double.NaN, 
            geocoordinate.Accuracy, 
            geocoordinate.AltitudeAccuracy ?? Double.NaN,
            geocoordinate.Speed ?? Double.NaN,
            geocoordinate.Heading ?? Double.NaN
            ); //returns Geocoordinate. Includes positional data such as Latitude, Longitude etc.


        }
    }
}
