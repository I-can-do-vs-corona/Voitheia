import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'keys'
})
export class KeysPipe implements PipeTransform {

  transform(value) : any {
    let keys = [];
    for (let key in value) {
      if(!isNaN(parseInt(key))){
        keys.push(parseInt(key));
      }
    }
    return keys;
  }

}
