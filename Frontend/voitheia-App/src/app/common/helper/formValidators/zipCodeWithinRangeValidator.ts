import { Directive, forwardRef, Attribute} from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl} from '@angular/forms';
import { environment } from 'src/environments/environment';

@Directive({
    selector: '[zipCodeWithinRange]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: ZipCodeWithinRangeValidator,
            multi: true
        }
    ]
})
export class ZipCodeWithinRangeValidator implements Validator {
    validate(control: AbstractControl): { [key: string]: any} | null {
        if(!environment.zipCodeCheckActive){
            return null;
        }

        let ranges = [
            //München
            {min: 80331, max: 85540}
        ];

        let withinOneRange = false;

        if (!control.value || typeof control.value !== "number" || isNaN(Number(control.value))) {
            return null;
        }

        for (let index = 0; index < ranges.length; index++ ) {
            if(control.value >= ranges[index].min && control.value <= ranges[index].max){
                withinOneRange = true;
            }
        }

        if (!withinOneRange) {
            return {
                zipCodeWithinRange: true
            };
        }
        return null;
    }
}