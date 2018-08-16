import {ToasterModule} from "angular2-toaster";
import {NgModule} from "@angular/core";
import {Ng2SmartTableModule} from "ng2-smart-table";
import {ThemeModule} from "../../@theme/theme.module";
import {CommonModule} from "@angular/common";
import {RolesService} from "../../@core/data/services/roles.service";
import {RolesComponent} from "./roles.component";


import { RolesListComponent } from './roles-list/roles-list.component';
import {AddRoleModalComponent} from './modal/AddRoleModal.component';
import {NgxDatatableModule} from "@swimlane/ngx-datatable";
import {DeleteRoleModalComponent} from "./modal/DeleteRoleModal.component";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    ToasterModule,
    NgxDatatableModule,

  ],
  declarations: [
    RolesComponent,
    RolesListComponent,
    AddRoleModalComponent,
    DeleteRoleModalComponent
  ],
  providers: [
    RolesService,
  ],
  entryComponents: [
    AddRoleModalComponent,
    DeleteRoleModalComponent
  ],
})
export class RolesModule { }
