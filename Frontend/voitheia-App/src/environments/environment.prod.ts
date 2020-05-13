export const environment = {
  production: true,
  apiBaseUrl: "https://activecruzer.azurewebsites.net/",
  requestDistance: "10000",
  requestAmount: "1000",
  defaultSessionLifetimeMinutes: 360,
  dialogWidth: "450px",
  registerOpenDate: new Date(2020, 4, 3, 18, 0, 0),
  goLiveDate: new Date(2020, 4, 18, 18, 0, 0),
  reCaptchaKey: "", //6LeJGOoUAAAAAFb1h7wSDXNl64oVQ0-QC_2RITYM
  zipCodeCheckActive: true,
  zipCodeLength: 5,
  zipCodeRanges: [
    //München
    {min: 80331, max: 81929},
    //Frankfurt
    {min: 60310, max: 60329},
    //Stuttgart
    {min: 70173, max: 70629}
  ],
  //In bytes
  profilePictureMaxSize: 1048576, //= 1MB
  //in Pixel
  profilePictureMaxWidth: 500,
  profilePictureMaxHeight: 500,
  profilePictureAllowedTypes: ['image/png']
};
