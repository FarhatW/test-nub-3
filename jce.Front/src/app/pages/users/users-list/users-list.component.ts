import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Subject} from "rxjs/Subject";
import {GoodService} from "../../../@core/data/services/good.service";
import {SearchService} from "../../../@core/data/services/search.service";
import {NotificationService} from "../../../@core/data/services/notification.service";
import {Page} from "../../../@core/data/models/shared/page";
import {Router} from "@angular/router";
import {DatatableComponent} from "@swimlane/ngx-datatable";
import {ToasterService} from "angular2-toaster";
import {UserProfileSave} from "../../../@core/data/models/user/userSave/UserProfileSave";
import {UsersService} from "../../../@core/data/services/users.service";
import {UserProfile} from "../../../@core/data/models/user/UserProfile";
import {CesListSearchComponent} from "../../ces/ces-list/ces-list-search/ces-list-search.component";
import {CE} from "../../../@core/data/models/ce/ce";
import {Supplier} from "../../../@core/data/models/supplier/supplier";
import {el} from "@angular/platform-browser/testing/src/browser_util";
import {Children} from "../../../@core/data/models/user/child/Children";
import {Routing} from "../../../@core/data/models/shared/Routing";

@Component({
  selector: 'ngx-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {


  routes: Routing[] = [];
  data: any[] = [];


  constructor() {

  }

  ngOnInit(): void {
    this.data.push('fas fa-user-plus');
    this.routes.push(new Routing("Ajouter un utilisateur", "/users/users-form/new", true, this.data))



  }


}
