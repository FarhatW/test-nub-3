import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {CesComponent} from '../ces/ces.component';
import {CesListComponent} from './ces-list/ces-list.component';
import {CesFormComponent} from './ces-form/ces-form.component';
import {CesFormInfosComponent} from './ces-form/ces-form-infos/ces-form-infos.component';
import {CesSetupFormComponent} from './ces-form/ces-setup-form/ces-setup-form.component';
import {CesCatalogFormComponent} from './ces-form/ces-catalog-form/ces-catalog-form.component';
import {CeViewComponent} from './ce-view/ce-view.component';
import {CesMailsFormComponent} from './ces-form/ces-mails-form/ces-mails-form.component';


const routes: Routes = [{
  path: '',
  component: CesComponent,
  children: [
    {
      path: 'ce-list',
      component: CesListComponent,
    },
    {
      path: 'ce-form',
      component: CesFormComponent,
      children: [
        {
        path: '',
        redirectTo: 'informations',
        component: CesFormInfosComponent,
        pathMatch: 'full',
      },
        {
          path: 'informations/:id',
          component: CesFormInfosComponent,
        },
        {
          path: 'configce/:id',
          component: CesSetupFormComponent,
        },
        {
          path: 'catalogue/:id',
          component: CesCatalogFormComponent,
        },
        {
          path: ':id/mails',
          component: CesMailsFormComponent,
        },
        // {
        //   path: '', redirectTo: 'informations', pathMatch: 'full', component: CesFormInfosComponent
        // },
        {
          path: 'new',
          component: CesFormInfosComponent,
        },
        {
          path: 'view/:id',
          component: CeViewComponent,
        },
      ]
    },
    {
      path: 'ce-view/:id',
      component: CeViewComponent,
    }
  ],
}];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class CesRoutingModule {

}

export const routedComponents = [
  CesComponent,
  CesListComponent,
  CesFormComponent,
];
