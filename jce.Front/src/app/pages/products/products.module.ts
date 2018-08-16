import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ThemeModule} from '../../@theme/theme.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {ToasterModule} from 'angular2-toaster';
import {PintelSheetService} from '../../@core/data/services/pintelSheet.service';
import {ProductsComponent} from './products.component';
import {GoodService} from '../../@core/data/services/good.service';
import { ProductListComponent } from './product-list/product-list.component';
import {ProductsRoutingModule} from './products-routing.module';
import {NgxDatatableModule} from '@swimlane/ngx-datatable';
import { ProductListProductComponent } from './product-list/product-list-product/product-list-product.component';
import {ProductListSearchComponent} from "./product-list/product-list-product/product-list-search/product-list-search.component";
import { ProductListExcelImportComponent } from './product-list/product-list-excel-import/product-list-excel-import.component';
import { ProductListBatchExcelImportComponent } from './product-list/product-list-batch-excel-import/product-list-batch-excel-import.component';
import {ProductFormService} from "./product-list/product-list-product/shared/product-form.service";
import {NgAutoCompleteModule} from "ng-auto-complete";
import {ExcelImportService} from "./product-list/shared/excel-import.service";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    ProductsRoutingModule,
    NgxDatatableModule,
    NgAutoCompleteModule,
  ],
  declarations: [
    ProductsComponent,
    ProductListComponent,
    ProductListProductComponent,
    ProductListSearchComponent,
    ProductListExcelImportComponent,
    ProductListBatchExcelImportComponent,
  ],
  providers: [
    ProductFormService,
    ExcelImportService
  ],
})
export class ProductsModule { }
