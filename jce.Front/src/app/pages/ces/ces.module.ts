import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ThemeModule} from '../../@theme/theme.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {CesRoutingModule, routedComponents} from '../ces/ces-routing.module';
import {CeService} from '../../@core/data/services/ce.service';
import { CesFormComponent } from './ces-form/ces-form.component';
import { CesSetupFormComponent } from './ces-form/ces-setup-form/ces-setup-form.component';
import { CesFormNavbarComponent } from './ces-form/ces-form-navbar/ces-form-navbar.component';
import { CesFormInfosComponent } from './ces-form/ces-form-infos/ces-form-infos.component';
import {ToasterModule} from 'angular2-toaster';
import { CesCatalogFormComponent } from './ces-form/ces-catalog-form/ces-catalog-form.component';
import {CesFormSummaryComponent} from './ces-form/ces-form-summary/ces-form-summary.component';
import { CeViewComponent } from './ce-view/ce-view.component';
import { CeViewDetailComponent } from './ce-view/ce-view-detail/ce-view-detail.component';
import { CeViewCatalogComponent } from './ce-view/ce-view-catalog/ce-view-catalog.component';
import { CeViewCesetupComponent } from './ce-view/ce-view-cesetup/ce-view-cesetup.component';
import {NgxDatatableModule} from '@swimlane/ngx-datatable';
import {CeFormService} from './ces-form/ce-form.service';
import {ReactiveFormsModule} from '@angular/forms';
import {CeViewMapComponent} from './ce-view/ce-view-map/ce-view-map.component';
import {AgmCoreModule} from '@agm/core';
import {CesListPaginationComponent} from './ces-list/ces-list-pagination/ces-list-pagination.component';
import { CesListSearchComponent } from './ces-list/ces-list-search/ces-list-search.component';
import { CesListPerUserComponent } from './ces-list/ces-list-per-user/ces-list-per-user.component';
import {CesMailsFormComponent} from './ces-form/ces-mails-form/ces-mails-form.component';
import { CesListeNavbarComponent } from './ces-list/ces-list-navbar/ces-list-navbar.component';
import {CesComponent} from './ces.component';
import {CesListComponent} from './ces-list/ces-list.component';
import {SharedModule} from "../shared/shared.module";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    CesRoutingModule,
    Ng2SmartTableModule,
    NgxDatatableModule,
    ReactiveFormsModule,
    SharedModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAxUDeLuyXGwonPYC42syt8-49ntMUkUB4'
    }),
  ],
  declarations: [
    // ...routedComponents,
    CesComponent,
    CesListComponent,
    CesFormComponent,
    CesSetupFormComponent,
    CesFormNavbarComponent,
    CesFormInfosComponent,
    CesCatalogFormComponent,
    CesFormSummaryComponent,
    CeViewComponent,
    // CeViewDetailComponent,
    CeViewCatalogComponent,
    CeViewCesetupComponent,
    CeViewMapComponent,
    CesListPaginationComponent,
    CesListSearchComponent,
    CesListPerUserComponent,
    CesMailsFormComponent,
    CesListeNavbarComponent,
  ],
  providers: [
    CeFormService
  ],
})
export class CesModule { }
