import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'ngx-users-elements',
  template: `<toaster-container></toaster-container>` +
  `<router-outlet></router-outlet>
  `,
})
export class UsersComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
