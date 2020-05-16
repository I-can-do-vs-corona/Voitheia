namespace ActiveCruzer.Helper
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }

        public static string ConvertUmlauts(this string self)
        {
            if (self.IsNullOrWhiteSpace())
            {
                return self;
            }
            self = self.Replace("ü", "ue");
            self = self.Replace("Ü", "Ue");
            self = self.Replace("ä", "ae");
            self = self.Replace("Ä", "Ae");
            self = self.Replace("ö", "oe");
            self = self.Replace("Ö", "Oe");
            return self.Replace("ß", "ss");
        }
    }
}