import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-countdown',
  templateUrl: './countdown.component.html',
  styleUrls: ['./countdown.component.scss']
})
export class CountdownComponent implements OnInit {

  text:any = {
    Year: 'Jahre',
    Month: 'Monate',
    Weeks: "Wochen",
    Days: "Tage",
    Hours: "Stunden",
    Minutes: "Minuten",
    Seconds: "Sekunden",
    MilliSeconds: "Millisekunden"
  };

  constructor() { }

  ngOnInit(): void {
  }

}
