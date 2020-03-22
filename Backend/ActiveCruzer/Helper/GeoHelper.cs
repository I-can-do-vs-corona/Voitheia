using System.Runtime.InteropServices.ComTypes;

namespace ActiveCruzer.Helper
{
    public class GeoHelper
    {
        public static double MetersToDegree(int meters)
        {
           return (double)meters / 40000000 * 360;

        }
    }
}