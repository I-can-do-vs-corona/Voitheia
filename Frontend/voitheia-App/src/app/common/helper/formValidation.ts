import { Directive, forwardRef, Attribute, Injector, SecurityContext } from '@angular/core';
import { NG_VALIDATORS, Validator,
         Validators, AbstractControl, ValidatorFn, NG_ASYNC_VALIDATORS, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from '../shared/services/utilities.service';
import { DomSanitizer } from '@angular/platform-browser';

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

@Directive({
    selector: '[requireField]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => RequireFieldValidator),
            multi: true
        }
    ]
})
export class RequireFieldValidator implements Validator {
    constructor( @Attribute('requireField') public controleValueField: string) {}

    validate(c: AbstractControl): { [key: string]: any } {
        // control value (e.g. password)
        const controleValue = c.root.get(this.controleValueField);

        if (controleValue && (controleValue.value === undefined || controleValue.invalid )) {
            return {
                requireField: true
            };
        }
        return null;
    }
}

@Directive({
    selector: '[requireFieldIfSet]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => RequireFieldIfSetValidator),
            multi: true
        }
    ]
})
export class RequireFieldIfSetValidator implements Validator {
    constructor( @Attribute('requireFieldIfSet') public controleValueField: string) {}

    validate(c: AbstractControl): { [key: string]: any } {
        // control value (e.g. password)
        const controleValue = c.root.get(this.controleValueField);

        if (c.value !== undefined && c.valid && controleValue && controleValue.value === undefined) {
            return {
                requireFieldIfSet: true
            };
        }
        return null;
    }
}
