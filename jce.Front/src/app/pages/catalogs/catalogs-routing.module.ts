import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {CatalogsComponent} from './catalogs.component';
import {CatalogMiniComponent} from './catalog-types/catalog-mini/catalog-mini.component';
import {CatalogTypesComponent} from './catalog-types/catalog-types.component';
import {CatalogLettersComponent} from './catalog-types/catalog-letters/catalog-letters.component';
import {CatalogSheetsComponent} from './catalog-types/catalog-sheets/catalog-sheets.component';
import {CatalogWrongTypeComponent} from './catalog-types/catalog-types-shared/catalog-wrong-type/catalog-wrong-type.component';


const routes: Routes = [
  {
    path: '',
    component: CatalogsComponent,
    children: [
      {
        path: 'mini-catalogue/:id',
        component: CatalogMiniComponent,
      },
      {
        path: 'lettres/:id',
        component: CatalogLettersComponent,
      },
      {
        path: 'fiches-de-collectivites/:id',
        component: CatalogSheetsComponent,
      },
      {
        path: '', redirectTo: 'mini-catalogue', pathMatch: 'full',
      },
      {
        path: 'wrong-type',
        component: CatalogWrongTypeComponent,
      },
    ],

  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class CatalogsRoutingModule {

}

export const routedComponents = [
  CatalogsComponent,
  CatalogTypesComponent,
  CatalogMiniComponent,
  CatalogSheetsComponent,
];
