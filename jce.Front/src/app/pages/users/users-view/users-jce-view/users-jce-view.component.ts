import {Component, Input, OnInit} from '@angular/core';
import {UserIdentity} from "../../../../@core/data/models/user/UserIdentity";
import {UserProfile} from "../../../../@core/data/models/user/userProfile";

@Component({
  selector: 'ngx-users-jce-view',
  templateUrl: './users-jce-view.component.html',
  styleUrls: ['./users-jce-view.component.scss']
})
export class UsersJceViewComponent implements OnInit {

  constructor() { }
  @Input() userProfile: UserProfile;
  ngOnInit() {
  }

}
