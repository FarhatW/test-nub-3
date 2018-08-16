import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {SharedService} from "../shared.service";

@Component({
  selector: 'ngx-shared-item-per-page',
  templateUrl: './shared-item-per-page.component.html',
  styleUrls: ['./shared-item-per-page.component.scss']
})
export class SharedItemPerPageComponent implements OnInit {
  @Input() item: string;
  @Output() perPage = new EventEmitter<number>();

  options = [10, 20, 50];
  optionSelected: number = this.options[0];

  constructor(public sharedService: SharedService) { }

  ngOnInit() {
    this.sharedService.setPerpage(this.optionSelected);
  }

  onOptionsSelected(event) {
    this.sharedService.setPerpage(event);

  }


}
