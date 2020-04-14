import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

@Directive({
    selector: '[notEqual][formControlName],[notEqual][formControl],[notEqual][ngModel]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => NotEqualValidator),
            multi: true
        }
    ]
})
export class NotEqualValidator implements Validator {
    constructor( @Attribute('notEqual') public notEqual: string,
                 @Attribute('notEqualReverse') public reverse: string) {}

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
        const otherControl = control.root.get(this.notEqual);

        // value not equal
        if (otherControl && control.value === otherControl.value  && !this.isReverse) {
            return {
                notEqual: true
            };
        }

        // value not equal and reverse
        if (otherControl && control.value !== otherControl.value && this.isReverse) {
            delete otherControl.errors['notEqual'];
            if (!Object.keys(otherControl.errors).length) {
                otherControl.setErrors(null);
            }
        }

        // value equal and reverse
        if (otherControl && control.value === otherControl.value && this.isReverse) {
            otherControl.setErrors({ notEqual: true });
        }

        return null;
    }
}