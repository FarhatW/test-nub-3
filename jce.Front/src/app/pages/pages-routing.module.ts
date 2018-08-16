import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {PagesComponent} from './pages.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {AuthGuardService} from "../@core/shared/auth/auth-guard.service";
import {RolesComponent} from "./roles/roles.component";
import {SuppliersComponent} from "./suppliers/suppliers.component";

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [
    {
      path: 'dashboard',
      component: DashboardComponent,
    },
    {
      path: 'roles',
      component: RolesComponent,
    },
    {
      path: 'suppliers',
      loadChildren: './suppliers/suppliers.module#SuppliersModule',
    },
    {
      path: 'pintelsheets',
      loadChildren: './pintelsheets/pintelsheets.module#PintelsheetsModule',
    },
    {
      path: 'ui-features',
      loadChildren: './ui-features/ui-features.module#UiFeaturesModule',
    }, {

      path: 'components',
      loadChildren: './components/components.module#ComponentsModule',

    },
    {
      path: 'maps',
      loadChildren: './maps/maps.module#MapsModule',

    }, {
      path: 'charts',
      loadChildren: './charts/charts.module#ChartsModule',

    }, {
      path: 'editors',
      loadChildren: './editors/editors.module#EditorsModule',

    }, {
      path: 'forms',
      loadChildren: './forms/forms.module#FormsModule',

    }, {
      path: 'tables',
      loadChildren: './tables/tables.module#TablesModule',

    }, {
      path: 'ces',
      loadChildren: './ces/ces.module#CesModule',

    },

    {
      path: 'users',
      loadChildren: './users/users.module#UsersModule',

    },
    {
      path: 'catalogues',
      loadChildren: './catalogs/catalogs.module#CatalogsModule',

    },
    {
      path: 'products',
      loadChildren: './products/products.module#ProductsModule',

    },
    {
      path: 'files',
      loadChildren: './files/files.module#FilesModule',
    },

    {
      path: '',
      redirectTo: 'dashboard',

      pathMatch: 'full',
    }],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
