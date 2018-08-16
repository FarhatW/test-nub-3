import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CeViewDetailComponent} from "../ces/ce-view/ce-view-detail/ce-view-detail.component";
import {ThemeModule} from "../../@theme/theme.module";
import {CesRoutingModule} from "../ces/ces-routing.module";
import {Ng2SmartTableModule} from "ng2-smart-table";
import {NgxDatatableModule} from "@swimlane/ngx-datatable";
import {ReactiveFormsModule} from "@angular/forms";
import {CesModule} from "../ces/ces.module";
import {ChildrenListComponent} from "../users/users-children/children-list/children-list.component";
import { SharedNavbarComponent } from './shared-navbar/shared-navbar.component';
import {RouterModule} from "@angular/router";
import { SharedUsersListComponent } from './shared-users-list/shared-users-list.component';
import {ToasterModule} from "angular2-toaster";
import { SharedChildrenListComponent } from './shared-children-list/shared-children-list.component';
import { SharedItemPerPageComponent } from './shared-item-per-page/shared-item-per-page.component';
import {SharedService} from "./shared.service";
import { SharedSearchComponent } from './shared-search/shared-search.component';

@NgModule({
  imports: [
    RouterModule,
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    ToasterModule,
    NgxDatatableModule,
  ],
  declarations: [
    CeViewDetailComponent,
    SharedNavbarComponent,
    SharedUsersListComponent,
    SharedChildrenListComponent,
    SharedItemPerPageComponent,
    SharedSearchComponent,

  ],
  exports: [
    CeViewDetailComponent,
    SharedNavbarComponent,
    SharedUsersListComponent,
    SharedChildrenListComponent,
    SharedItemPerPageComponent,
    SharedSearchComponent
  ], providers: [
    SharedService

  ]
})
export class SharedModule { }
