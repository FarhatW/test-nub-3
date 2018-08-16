
import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';


@Component({
  template: `
    <input class="form-control" type="number" style="border: none; background: transparent;" disabled
           [(value)]="renderValue">
  `,
})
export class DateInputComponent implements OnInit {

  public renderValue: number;

  @Input() value: string;

  constructor() {  }

  ngOnInit() {


    this.renderValue = +this.value.substring(0, 4);

    console.log('value', this.value);
  }



}
