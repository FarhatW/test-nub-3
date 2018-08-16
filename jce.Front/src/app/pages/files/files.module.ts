import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ReactiveFormsModule} from "@angular/forms";
import {ThemeModule} from "../../@theme/theme.module";
import { FilesComponent} from "./files.component";
import {ToasterModule} from "angular2-toaster";
import {NgxDatatableModule} from "@swimlane/ngx-datatable";
import {FilesRoutingModule} from "./files-routing.module";
import { FilesOrganizerComponent } from './files-organizer/files-organizer.component';
import {ButtonModule} from "primeng/primeng";
import {GrowlModule} from "primeng/primeng";
import {DataTableModule, DropdownModule, FileUploadModule, InputTextModule} from "primeng/primeng";
import {TreeModule} from "primeng/primeng";
import {SharedModule} from "primeng/primeng";
import {DialogModule} from "primeng/primeng";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    FilesRoutingModule,
    ButtonModule,
    GrowlModule,
    DropdownModule,
    FileUploadModule,
    DataTableModule,
    TreeModule,
    SharedModule,
    DialogModule,
    InputTextModule
  ],

  declarations: [
    FilesComponent,
    FilesOrganizerComponent
  ]
})
export class FilesModule {

}
