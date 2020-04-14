import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

@Directive({
    selector: '[greaterThan]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => GreaterThanValidator),
            multi: true
        }
    ]
})
export class GreaterThanValidator implements Validator {
    constructor( @Attribute('greaterThan') public equal: string,
                 @Attribute('greaterThanReverse') public reverse: string) {}

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

        // value not greater than
        if (otherControl && control.value <= otherControl.value  && !this.isReverse) {
            return {
                greaterThan: true
            };
        }

        // value less than and reverse
        if (otherControl && control.value < otherControl.value && this.isReverse) {
            delete otherControl.errors['greaterThan'];
            if (!Object.keys(otherControl.errors).length) {
                otherControl.setErrors(null);
            }
        }

        // value not less and reverse
        if (otherControl && control.value < otherControl.value && this.isReverse) {
            otherControl.setErrors({ greaterThan: true });
        }

        return null;
    }
}