import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

@Directive({
    selector: '[numberLength]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: NumberLengthValidator,
            multi: true
        }
    ]
})
export class NumberLengthValidator implements Validator {
    constructor( @Attribute('numberLength') public numberLength: string) {}

    validate(control: AbstractControl): { [key: string]: any} | null {
        if(isNaN(Number(this.numberLength))){
            return null;
        }

        if (!control.value || isNaN(Number(control.value))) {
            return null;
        }

        let numberOfDigits = parseInt(this.numberLength);
        let maxNumber = 0;
        let minNumber = 0;
        let numberString = "";
        for(let index = 1; index <= numberOfDigits; index++){
            numberString += "9";

            if(index === numberOfDigits - 1){
                minNumber = parseInt(numberString);
                minNumber++;
            }
        }

        maxNumber = parseInt(numberString);
        
        let value = parseInt(control.value);

        if (value < minNumber || value > maxNumber) {
            return {
                numberLength: true
            };
        }
        return null;
    }
}