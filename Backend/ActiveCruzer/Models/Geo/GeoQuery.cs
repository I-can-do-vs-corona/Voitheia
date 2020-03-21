namespace ActiveCruzer.Models.Geo
{
    /// <summary>
    /// Query to retrieve geoCordinates
    /// </summary>
    public class GeoQuery
    {
        /// <summary>
        /// The street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The zip
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// The City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The Country
        /// </summary>
        public string Country { get; set; } = "Germany";

        public override string ToString()
        {
            return Street + "," + Zip + "," + City + "," + Country;
        }
    }
}