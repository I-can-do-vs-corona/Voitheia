import { Directive, OnInit, Input, OnChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpClient } from '@angular/common/http';

@Directive({
  selector: '[profile-picture]',
  host: {  
    '[src]': 'sanitizedImageDataUrl',
    '[hidden]': 'hideImage'
  } 
})
export class ProfilePictureDirective  implements OnInit, OnChanges  {
  imageDataUrl: any;
  sanitizedImageDataUrl: any;
  hideImage: boolean;

  @Input('profile-picture') pictureData: string;
  @Input('profile-picture-type') pictureType: string;

  constructor(private _sanitizer: DomSanitizer, private _http: HttpClient) { }

  ngOnInit() {
    this.hideImage = true;
    this.calculateImage();
  }

  ngOnChanges() {
    this.calculateImage();
  }

  calculateImage(){
    if(typeof this.pictureData === 'undefined' || this.pictureData === null || this.pictureData === ""){
      this._http.get('../../../assets/images/placeholder.jpg', { responseType: 'blob' })
      .subscribe(res => {
        const reader = new FileReader();
        reader.onloadend = () => {
          this.imageDataUrl = reader.result;
          this.sanitizedImageDataUrl = this._sanitizer.bypassSecurityTrustUrl(this.imageDataUrl);
          this.hideImage = false;
        }
        reader.readAsDataURL(res);
      });
    } else{
      if(typeof this.pictureType === 'undefined' || this.pictureType === null || this.pictureType === ""){
        this.pictureType = "png";
      }
      this.imageDataUrl = "data:image/" + this.pictureType +";base64," + this.pictureData;
      this.sanitizedImageDataUrl = this._sanitizer.bypassSecurityTrustUrl(this.imageDataUrl);
      this.hideImage = false;
    }
  }
}
