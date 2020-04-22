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
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/register', user, {withCredentials: false});
  }

  getUserData() {
    return this._httpClient.get(this._utilitiesService.getAPIUrl() + 'user/getuser', {withCredentials: false});
  }

  updateUserData(userData: UpdateUserDTO){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/update', userData, {withCredentials: false});
  }

  confirmEmail(confirmEmailDTO: ConfirmEmailDTO){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/confirmEmail', confirmEmailDTO, {withCredentials: false});
  }

  sendConfirmationMailAgain(email: string){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/sendConfirmationMailAgain', {email: email}, {withCredentials: false});
  }

  deleteAccount() {
    return this._httpClient.delete(this._utilitiesService.getAPIUrl() + 'user/Delete', {withCredentials: false});
  }

  changePassword(changePasswordDTO: SetNewPasswordDTO){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/setNewPassword', changePasswordDTO, {withCredentials: false});
  }

  changeEmail(changeEmailDTO: SetNewEmailDTO){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/setNewEmail', changeEmailDTO, {withCredentials: false});
  }

  sendResetPasswordMail(email: string){
    return this._httpClient.put(this._utilitiesService.getAPIUrl() + 'user/forgotPassword', {email: email}, {withCredentials: false});
  }

  resetPassword(resetPasswordCredentials: ResetPasswordCredentials){
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/resetPassword', resetPasswordCredentials, {withCredentials: false});
  }

  updateProfilePicture(fileToUpload: File){
    let formData = new FormData();
    formData.append("image", fileToUpload);
    return this._httpClient.post(this._utilitiesService.getAPIUrl() + 'user/changeProfilePicture', formData, {withCredentials: false});
  }
}
