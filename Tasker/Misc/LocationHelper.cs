using System.Collections.Generic;
using System.Device.Location;
using Tasker.Models;

namespace Tasker.Misc 
{
    public static class LocationHelper
    {
        public static bool IsNearBy(this Location location, GeoCoordinate pointCoordinate, double distanceThreshold = 1.0)
        {
            double distance = GeoCodeDistanceCalc.CalcDistance(location.Latitude, location.Longitude,
                              pointCoordinate.Latitude, pointCoordinate.Longitude);
            return (distance <= distanceThreshold);  
        }

        public static Location FindNearestMeetingThreshold(IEnumerable<Location> locations,
                    GeoCoordinate pointCoordinate, double threshold = 1.0)
        {
            double distance = 2000000;
            Location foundLocation = null; 
            foreach (Location location in locations)
            {
                double newdistance = GeoCodeDistanceCalc.CalcDistance(location.Latitude, location.Longitude,
                                                     pointCoordinate.Latitude, pointCoordinate.Longitude);

                if (newdistance < threshold && newdistance < distance)
                {
                    foundLocation = location;
                    distance = newdistance; 
                } 
            }
            return foundLocation; 

        } 
        
    }
}
