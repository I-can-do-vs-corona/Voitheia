import { Directive, forwardRef, Attribute } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

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
