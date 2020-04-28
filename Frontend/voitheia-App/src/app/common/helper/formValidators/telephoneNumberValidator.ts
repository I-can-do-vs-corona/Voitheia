import { Directive} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';
var PhoneNumber = require( 'awesome-phonenumber' );

@Directive({
    selector: '[telephoneNumber]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: telephoneNumberValidator,
            multi: true
        }
    ]
})
export class telephoneNumberValidator implements Validator {

    constructor() {}

    validate(control: AbstractControl): { [key: string]: any} | null {
        if (!control.value) {
            return null;
        }

        let isValid = false;

        let defaultCC = "DE";
		let allowedCC = ["DE"];
		let excludedCC = [];
		let allowedTypes = [];
		let excludedTypes = [];

        let outputFormat = "e164"; //international, national
        
        //var PhoneNumber = require( 'awesome-phonenumber' );
        var parsedPhoneNumber = (defaultCC !== "")? PhoneNumber(control.value, defaultCC) : PhoneNumber(control.value);

        if(parsedPhoneNumber !== undefined && parsedPhoneNumber.isValid()){
            isValid = true;
            
            if((allowedCC != undefined && allowedCC.length > 0 && allowedCC.indexOf(parsedPhoneNumber.getRegionCode().toUpperCase()) === -1) || 
                (excludedCC != undefined && excludedCC.length > 0 && excludedCC.indexOf(parsedPhoneNumber.getRegionCode().toUpperCase()) !== -1)){
                isValid = false;
            }
            
            if(isValid &&
                (allowedTypes != undefined && allowedTypes.length > 0 && allowedTypes.indexOf(parsedPhoneNumber.getType().toLowerCase()) === -1) || 
                (excludedTypes != undefined && excludedTypes.length > 0 && excludedTypes.indexOf(parsedPhoneNumber.getType().toLowerCase()) !== -1)){
                isValid = false;
            }
        }
        if(!isValid){
            return {
                telephoneNumber: true
            };
        } else {
            control.setValue(parsedPhoneNumber.getNumber(outputFormat));
        }
    }
}