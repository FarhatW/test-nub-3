import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UsersService} from "../../../../@core/data/services/users.service";
import {UserProfile} from "../../../../@core/data/models/user/UserProfile";

@Component({
  selector: 'ngx-ces-list-per-user',
  templateUrl: './ces-list-per-user.component.html',
  styleUrls: ['./ces-list-per-user.component.scss']
})
export class CesListPerUserComponent implements OnInit {

  @Output() userId = new EventEmitter<number>()
  @Input() users: UserProfile[];

  private readonly UserType: number = 1;
  private readonly NoPageSize: boolean = true;
  query: any = {
    userType: this.UserType,
    noPageSize: this.NoPageSize
  };

  selectedUser = null;

  constructor(private userService: UsersService) { }

  ngOnInit() {
    // this.query.userType = 1;
    // this.userService.getAll(this.query).subscribe(
    //   usersList => {
    //     this.usersList = usersList.items;
    //     console.log('usersList', usersList);
    //   },
    //   err => {
    //     console.log(err);
    //   }
    // )
  }

  onUserSelected(event) {
    console.log('onUserSelected', event);
    this.userId.emit(event);
  }

}
