import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';

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