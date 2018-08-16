import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {SuppliersComponent} from "./suppliers.component";
import {SuppliersListComponent} from "./suppliers-list/suppliers-list.component";
import {SuppliersListFormComponent} from "./suppliers-list/suppliers-list-form/suppliers-list-form.component";


const routes: Routes = [{
  path: '',
  component: SuppliersComponent,
  children: [
    {
      path: 'list',
      component: SuppliersListComponent,
      data: {title: 'Liste des Fournisseurs'},
      children: [{
        path: ':id',
        component: SuppliersListFormComponent,
        data: {
          title: 'Fiche Fournisseur',
          state: 'supplier'
        }
      },
        {
          path: 'new',
          component: SuppliersListFormComponent,
          data: {
            title: 'Nouveau Fournisseur°',
            state: 'new'
          }

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
export class SuppliersRoutingModule {

}

export const routedComponents = [
  SuppliersComponent,
  SuppliersListComponent
];
