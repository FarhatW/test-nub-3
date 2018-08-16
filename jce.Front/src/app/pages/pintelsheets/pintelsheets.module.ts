import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ThemeModule} from '../../@theme/theme.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {ToasterModule} from 'angular2-toaster';
import {NgxDatatableModule} from "@swimlane/ngx-datatable";
import {ReactiveFormsModule} from "@angular/forms";
import {PintelsheetsListComponent} from "./pintelsheets-list/pintelsheets-list.component";
import {PintelsheetsComponent} from "./pintelsheets.component";
import {PintelsheetsRoutingModule} from "./pintelsheets-routing.module";
import {PintelsheetsListFormComponent} from "./pintelsheets-list/pintelsheets-list-form/pintelsheets-list-form.component";
import {PintelsheetFormService} from "./pintelsheets-list/pintelsheets-list-form/shared/pintelsheet-form.service";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    PintelsheetsRoutingModule,
  ],
  declarations: [
    PintelsheetsComponent,
    PintelsheetsListComponent,
    PintelsheetsListFormComponent,
  ],
  providers: [
    PintelsheetFormService
  ],
})
export class PintelsheetsModule { }
