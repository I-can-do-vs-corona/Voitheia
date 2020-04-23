import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from 'src/app/common/shared/services/utilities.service';
import { RegisterUserDTO } from 'src/app/common/models/registerUserDTO';
import { UpdateUserDTO } from 'src/app/common/models/updateUserDTO';
import { ConfirmEmailDTO } from 'src/app/common/models/confirmEmailDTO';
import { SetNewPasswordDTO } from 'src/app/common/models/setNewPasswordDTO';
import { ResetPasswordCredentials } from 'src/app/common/models/resetPasswordCredentialsDTO';
import { SetNewEmailDTO } from 'src/app/common/models/setNewEmailDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _httpClient: HttpClient, private _utilitiesService: UtilitiesService) { }

  registerUser(user: RegisterUserDTO) {
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/register', user);
  }

  getUserData() {
    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'user/getuser');
  }

  updateUserData(userData: UpdateUserDTO){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/update', userData);
  }

  confirmEmail(confirmEmailDTO: ConfirmEmailDTO){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/confirmEmail', confirmEmailDTO);
  }

  sendConfirmationMailAgain(email: string){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/sendConfirmationMailAgain', {email: email});
  }

  deleteAccount() {
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'user/delete');
  }

  deleteProfilePicture(){
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'user/deleteProfilePicture');
  }

  changePassword(changePasswordDTO: SetNewPasswordDTO){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/setNewPassword', changePasswordDTO);
  }

  changeEmail(changeEmailDTO: SetNewEmailDTO){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/setNewEmail', changeEmailDTO);
  }

  sendResetPasswordMail(email: string){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/forgotPassword', {email: email});
  }

  resetPassword(resetPasswordCredentials: ResetPasswordCredentials){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/resetPassword', resetPasswordCredentials);
  }

  updateProfilePicture(fileToUpload: File){
    let formData = new FormData();
    formData.append("image", fileToUpload);
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/changeProfilePicture', formData);
  }
}
