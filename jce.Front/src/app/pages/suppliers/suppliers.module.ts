import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ThemeModule} from '../../@theme/theme.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {NgxDatatableModule} from '@swimlane/ngx-datatable';
import {ReactiveFormsModule} from '@angular/forms';
import {SuppliersComponent} from './suppliers.component';
import {SuppliersListComponent} from './suppliers-list/suppliers-list.component';
import {SuppliersRoutingModule} from './suppliers-routing.module';
import {SuppliersListFormComponent} from './suppliers-list/suppliers-list-form/suppliers-list-form.component';
import {SupplierFormService} from './suppliers-list/suppliers-list-form/shared/supplier-form.service';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    SuppliersRoutingModule
  ],
  declarations: [
    SuppliersComponent,
    SuppliersListComponent,
    SuppliersListFormComponent
  ],
  providers: [
    SupplierFormService
  ],
})
export class SuppliersModule {
}
