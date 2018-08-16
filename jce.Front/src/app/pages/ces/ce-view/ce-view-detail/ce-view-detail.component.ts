import {Component, Input, OnInit} from '@angular/core';
import {CE} from "../../../../@core/data/models/ce/ce";
import {UsersService} from "../../../../@core/data/services/users.service";
import {UserProfileSave} from "../../../../@core/data/models/user/userSave/UserProfileSave";
import {Router} from "@angular/router";

@Component({
  selector: 'ngx-ce-view-detail',
  templateUrl: './ce-view-detail.component.html',
  styleUrls: ['./ce-view-detail.component.scss']
})
export class CeViewDetailComponent implements OnInit {

  @Input() ce: CE;
  user: UserProfileSave;
  constructor(private userService: UsersService,
              private router: Router) { }

  ngOnInit() {
    // this.userService.getById(this.ce.adminJceProfileId,)
  }

  toCeEdit() {
    this.router.navigate(['/ces/ce-form/informations/', this.ce.id])
  }

}
