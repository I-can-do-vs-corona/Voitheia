import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

@Directive({
    selector: '[inputNumber]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: inputNumberValidator,
            multi: true
        }
    ]
})
export class inputNumberValidator implements Validator {
    constructor() {}

    validate(control: AbstractControl): { [key: string]: any} | null {
        if (!control.value || !isNaN(Number(control.value))) {
            return null;
        }
        
        return {
            inputNumber: true
        };
    }
}