namespace ActiveCruzer.Models.DTO.Geo
{
    /// <summary>
    /// Model for gps coordinates. Contains the longitude and latitude in degrees
    /// </summary>
    public class CoordinatesDto
    {
        /// <summary>
        /// The longitude in degrees
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// The latitude in degrees
        /// </summary>
        public double Latitude { get; set; }
    }
}