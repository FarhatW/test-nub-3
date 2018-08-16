import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {ProductsComponent} from './products.component';
import {ProductListComponent} from './product-list/product-list.component';
import {ProductListProductComponent} from "./product-list/product-list-product/product-list-product.component";
import {ProductListExcelImportComponent} from "./product-list/product-list-excel-import/product-list-excel-import.component";
import {ProductListBatchExcelImportComponent} from "./product-list/product-list-batch-excel-import/product-list-batch-excel-import.component";


const routes: Routes = [
  {
    path: '',
    component: ProductsComponent,
    children: [
      {
        path: 'product-list',
        component: ProductListComponent,
        children: [
          {
            path: 'new-product',
            component: ProductListProductComponent,
            data: {isBatch: false,
            state: 'newProduct'
            }
          },
          {
            path: 'new-batch',
            component: ProductListProductComponent,
            data: {isBatch: true,
              state: 'newBatch'
            }
          },
          {
            path: 'product/:id',
            component: ProductListProductComponent,
            data: {
              state: 'product'
            }
          },
          {
            path: 'excel-import',
            children: [{
              path: 'products',
              component: ProductListExcelImportComponent,
              data: {
                state: 'productXL'
              }
            },
              {
                path: 'batches',
                component: ProductListBatchExcelImportComponent,
                data: {
                  state: 'batchXL'
                }
              },
            ]
          },
        ]
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
export class ProductsRoutingModule {

}

export const routedComponents = [
  ProductsComponent,
];
