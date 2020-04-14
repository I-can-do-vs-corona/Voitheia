import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';


@Directive({
    selector: '[equal][formControlName],[equal][formControl],[equal][ngModel]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => EqualValidator),
            multi: true
        }
    ]
})
export class EqualValidator implements Validator {
    constructor( @Attribute('equal') public equal: string,
                 @Attribute('equalReverse') public reverse: string) {}

    private get isReverse() {
        if (!this.reverse) {
            return false;
        }

        return this.reverse === 'true' ? true : false;
    }

    validate(control: AbstractControl): { [key: string]: any } {

        if (!control.value || control.value === '') {
            return null;
        }

        if (!control.parent) {
            return null;
        }

        // control value (e.g. password)
        const otherControl = control.root.get(this.equal);

        // value not equal
        if (otherControl && control.value !== otherControl.value  && !this.isReverse) {
            return {
                equal: true
            };
        }

        // value equal and reverse
        if (otherControl && control.value === otherControl.value && this.isReverse) {
            delete otherControl.errors['equal'];
            if (!Object.keys(otherControl.errors).length) {
                otherControl.setErrors(null);
            }
        }

        // value not equal and reverse
        if (otherControl && control.value !== otherControl.value && this.isReverse) {
            otherControl.setErrors({ equal: true });
        }

        return null;
    }
}