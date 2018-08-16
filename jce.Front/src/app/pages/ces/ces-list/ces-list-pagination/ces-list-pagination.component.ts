import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'ngx-ces-list-pagination',
  templateUrl: './ces-list-pagination.component.html',
  styleUrls: ['./ces-list-pagination.component.scss']
})
export class CesListPaginationComponent implements OnInit {

@Output() perPage = new EventEmitter<number>();

  options = [10, 20, 50];
  optionSelected: any = 10;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  onOptionsSelected(event) {
    console.log('event', event)
    this.perPage.emit(event);
  }

  _newCeButtonClick() {
    this.router.navigate(['/ces/ce-form/new']);
  }

}
