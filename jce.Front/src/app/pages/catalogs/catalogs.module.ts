import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ThemeModule} from '../../@theme/theme.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {ToasterModule} from 'angular2-toaster';
import {CatalogMiniComponent} from './catalog-types/catalog-mini/catalog-mini.component';
import {CatalogTypesComponent} from './catalog-types/catalog-types.component';
import {CatalogsRoutingModule} from './catalogs-routing.module';
import {CatalogsComponent} from './catalogs.component';
import {CatalogService} from '../../@core/data/services/catalog.service';
import {CatalogLettersComponent} from './catalog-types/catalog-letters/catalog-letters.component';
import {CatalogFilesearchComponent} from './catalog-types/catalog-types-shared/catalog-filesearch/catalog-filesearch.component';
import {CatalogHistoryComponent} from './catalog-types/catalog-types-shared/catalog-history/catalog-history.component';
import {CatalogSheetsComponent} from './catalog-types/catalog-sheets/catalog-sheets.component';
import {PintelSheetService} from '../../@core/data/services/pintelSheet.service';
import {CatalogWrongTypeComponent} from './catalog-types/catalog-types-shared/catalog-wrong-type/catalog-wrong-type.component';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    Ng2SmartTableModule,
    CatalogsRoutingModule,
  ],
  declarations: [
    CatalogsComponent,
    CatalogMiniComponent,
    CatalogTypesComponent,
    CatalogLettersComponent,
    CatalogFilesearchComponent,
    CatalogHistoryComponent,
    CatalogSheetsComponent,
    CatalogWrongTypeComponent],
  providers: [
    CatalogService,
    PintelSheetService
  ],
})
export class CatalogsModule {
}
