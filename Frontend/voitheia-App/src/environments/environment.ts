// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiBaseUrl: "https://activecruzerdev.azurewebsites.net/",
  requestDistance: "10000",
  requestAmount: "1000",
  defaultSessionLifetimeMinutes: 360,
  dialogWidth: "450px",
  registerOpenDate: new Date(2020, 3, 1, 8, 0, 0),
  goLiveDate: new Date(2020, 3, 4, 8, 0, 0),
  reCaptchaKey: "",//"6Lc1GOoUAAAAAAxQau6hK3a7rsXjNatarq4pAk6o",
  zipCodeCheckActive: true,
  zipCodeLength: 5,
  //In bytes
  profilePictureMaxSize: 1048576, //= 1MB
  //in Pixel
  profilePictureMaxWidth: 500,
  profilePictureMaxHeight: 500,
  profilePictureAllowedTypes: ['image/png']
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
