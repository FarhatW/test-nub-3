import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {PintelsheetsComponent} from './pintelsheets.component';
import {PintelsheetsListComponent} from "./pintelsheets-list/pintelsheets-list.component";
import {PintelsheetsListFormComponent} from "./pintelsheets-list/pintelsheets-list-form/pintelsheets-list-form.component";

const routes: Routes = [{
  path: '',
  component: PintelsheetsComponent,
  children: [{
    path: 'list',
    component: PintelsheetsListComponent,
    children: [{
      path: ':id',
      component: PintelsheetsListFormComponent
    },
      {
        path: 'new',
        component: PintelsheetsListFormComponent
      }]
  },
  ]
}];


@NgModule({
  imports: [
    RouterModule.forChild(routes),

  ],
  exports: [
    RouterModule,
  ],
})

export class PintelsheetsRoutingModule {

}

