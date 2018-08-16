import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'ngx-users-list-pagination',
  templateUrl: './users-list-pagination.component.html',
  styleUrls: ['./users-list-pagination.component.scss']
})
export class UsersListPaginationComponent implements OnInit {

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


}
