namespace ActiveCruzer.Models
{
    public static class ErrorCodes
    {
        public const int EmailNotConfirmed = 1;
        public const int OwnRequest = 2;
        public const int RequestDoesNotExist =3;
        public const int InvalidRequestState = 4;
        public const int InvalidAddress = 5;
        public const int MissingCoordinates = 6;
        public const int ContactSupport = 7;
        public const int AccountLocked = 8;
        public const int InvalidCredentials = 9;
        public const int NotAuthorizedToPerformAction = 10;
        public const int TryAgain = 10;
        public const int InvalidModel = 11;
    }
}