import {ToasterModule} from "angular2-toaster";
import {NgModule} from "@angular/core";
import {Ng2SmartTableModule} from "ng2-smart-table";
import {ThemeModule} from "../../@theme/theme.module";
import {CommonModule} from "@angular/common";
import {UsersComponent} from "./users.component";
import {UsersFormComponent} from "./users-form/users-form.component";
import {UsersListComponent} from "./users-list/users-list.component";
import {UsersRoutingModule} from "./users-routing.module";
import {NgxDatatableModule} from "@swimlane/ngx-datatable";
import {UsersService} from "../../@core/data/services/users.service";
import {UserJceFromService} from "./users-form/user-jce-form/user-jce-from-service";
import { UserIdentityFormComponent } from './users-form/user-identity-form/user-identity-form.component';
import { UserJceFormComponent } from './users-form/user-jce-form/user-jce-form.component';
import {UserAuthFormService} from "./users-form/user-identity-form/UserAuthFormService";
import {UserIdentityService} from "../../@core/data/services/user-Identity.service";
import {DragDropModule} from "primeng/primeng";
import {UsersListPaginationComponent} from "./users-list/users-list-pagination/users-list-pagination.component";
import {UsersListSearchComponent} from "./users-list/users-list-search/users-list-search.component";
import { UsersViewComponent } from './users-view/users-view.component';
import { UsersIdentityViewComponent } from './users-view/users-identity-view/users-identity-view.component';
import { UsersJceViewComponent } from './users-view/users-jce-view/users-jce-view.component';
import { UsersChildViewComponent } from './users-view/users-child-view/users-child-view.component';
import {UsersPasswordFormService} from "./users-form/users-password-form/UsersPasswordFormService";
import {SharedModule} from "../shared/shared.module";
import {UsersPasswordFormComponent} from "./users-form/users-password-form/users-password-form.component";
import {ChildrenListComponent} from "./users-children/children-list/children-list.component";
import {ChildrenService} from "../../@core/data/services/children.service";
import {RouterModule} from "@angular/router";
import {ChildrenFormComponent} from "./users-children/children-form/children-form.component";
import {ChildService} from "./users-children/children-form/ChildService";

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    ThemeModule,
    UsersRoutingModule,
    Ng2SmartTableModule,
    ToasterModule,
    NgxDatatableModule,
    DragDropModule,
    SharedModule
  ],
  declarations: [
    UsersComponent,
    UsersFormComponent,
    UsersListComponent,
    UserIdentityFormComponent,
    UserJceFormComponent,
    UsersListPaginationComponent,
    UsersListSearchComponent,
    UsersViewComponent,
    UsersViewComponent,
    UsersIdentityViewComponent,
    UsersJceViewComponent,
    UsersChildViewComponent,
    UsersPasswordFormComponent,
    ChildrenListComponent,
    ChildrenFormComponent
  ],
  providers: [
    UsersService,
    UserIdentityService,
    UserJceFromService,
    UserAuthFormService,
    UsersPasswordFormService,
    ChildrenService,
    ChildService
  ],
})
export class UsersModule { }
