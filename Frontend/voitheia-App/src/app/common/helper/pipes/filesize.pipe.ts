import { Pipe, PipeTransform } from '@angular/core';

import { DecimalPipe } from '@angular/common';

@Pipe({
  name: 'filesize'
})
export class FilesizePipe implements PipeTransform {

  transform(value: any): any {
    var transformedValue =  "";
    if(value < 1024){
      transformedValue = value + " b";
    }else if(value <= (1024 * 1024) -1){
      transformedValue = new DecimalPipe("de").transform((value / 1024.0), "1.0-1") + " kb";
    }else{
      transformedValue = new DecimalPipe("de").transform((value / (1024.0 * 1024.0)), "1.0-1") + " mb";
    }
    return transformedValue;
  }

}
