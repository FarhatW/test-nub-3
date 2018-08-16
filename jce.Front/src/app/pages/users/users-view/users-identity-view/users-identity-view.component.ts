import {Component, Input, OnInit} from '@angular/core';
import {CE} from "../../../../@core/data/models/ce/ce";
import {UserIdentity} from "../../../../@core/data/models/user/UserIdentity";

@Component({
  selector: 'ngx-users-identity-view',
  templateUrl: './users-identity-view.component.html',
  styleUrls: ['./users-identity-view.component.scss']
})
export class UsersIdentityViewComponent implements OnInit {

  constructor() { }
  @Input() userIdentity: UserIdentity;

  ngOnInit() {
  }

}
