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
            {min: 80331, max: 85540},
            {min: 60306, max: 65936},
            {min: 70173, max: 70629}
        ];

        let withinOneRange = false;

        if (!control.value || isNaN(Number(control.value))) {
            return null;
        }

        let value = parseInt(control.value);

        for (let index = 0; index < ranges.length; index++ ) {
            if(value >= ranges[index].min && value <= ranges[index].max){
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