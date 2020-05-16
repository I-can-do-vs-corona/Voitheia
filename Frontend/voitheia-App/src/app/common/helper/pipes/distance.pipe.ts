import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Pipe({
  name: 'distance'
})
export class DistancePipe implements PipeTransform {

  transform(value: any): any {
    var transformedValue =  "";
    if(value <= 450){
      transformedValue = value + " m";
    }else{
      transformedValue = new DecimalPipe("de").transform((value / 1000.0), "1.0-1") + " km";
    }
    return transformedValue;
  }
}
