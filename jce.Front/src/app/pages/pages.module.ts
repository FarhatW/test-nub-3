import { NgModule } from '@angular/core';

import { PagesComponent } from './pages.component';
import { DashboardModule } from './dashboard/dashboard.module';
import { PagesRoutingModule } from './pages-routing.module';
import { ThemeModule } from '../@theme/theme.module';
import {DateInputComponent} from '../@core/utils/dateInput.component';
import {ToasterModule} from 'angular2-toaster';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RolesModule} from './roles/roles.module';

const PAGES_COMPONENTS = [
  PagesComponent,
];

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    DashboardModule,
    ToasterModule,
    ReactiveFormsModule,
    FormsModule,
    RolesModule,

    // AgmCoreModule.forRoot({
    //   apiKey: 'AIzaSyAxUDeLuyXGwonPYC42syt8-49ntMUkUB4'
    // }),
  ],
  providers: [

  ],
  declarations: [
    ...PAGES_COMPONENTS,
    DateInputComponent,

  ],
  entryComponents: [DateInputComponent]
})
export class PagesModule {
}
