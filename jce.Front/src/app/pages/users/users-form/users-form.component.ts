import {
  AfterContentChecked,
  AfterViewChecked,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {UserJceFormComponent} from "./user-jce-form/user-jce-form.component";
import {UserIdentityFormComponent} from "./user-identity-form/user-identity-form.component";


@Component({
  selector: 'ngx-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.scss']
})
export class UsersFormComponent implements OnInit, AfterContentChecked {

  addProfile: boolean = true;
  isECE: boolean = false;
  constructor(  private route: ActivatedRoute) { }

  ngOnInit(): void {

  }

  ngAfterContentChecked() {
    switch (this.route.firstChild.component) {
      case  UserJceFormComponent :
        this.addProfile = true;
        break;
      case UserIdentityFormComponent :
        this.addProfile = false;
        break;
    }
  }
}
