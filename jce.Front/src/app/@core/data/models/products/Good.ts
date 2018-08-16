import {Product} from './product';
import {CatalogSave} from '../catalog/catalogSave';

class Good {
  constructor(public id: number,
              public refPintel: string,
              public details: string,
              public title: string,
              public  price: number,
              public indexId: string,
              public  productTypeId: number,
              public  goodDepartmentId: number,
              public  supplierId: number,
              public  season: string,
              public  pintelSheetId: number,
              public  isBasicProduct: boolean,
              public  isBatch: boolean,
              public  isDisplayedOnJCE: boolean,
              public  isDiscountable: boolean,
              public  isEnabled: boolean,
              public  originId: number,
              public  products: Good[],
              public  createdOn: string,
              public  createdBy: string,
              public  updatedOn: string,
              public  updatedBy: string,
  ) {
  }
}

class FormGoodModel {
  constructor(public id: number,
              public refPintel: string,
              public details: string,
              public title: string,
              public  price: number,
              public indexId: string,
              public  productTypeId: number,
              public  goodDepartmentId: number,
              public  supplierId: number,
              public  season: string,
              public  pintelSheetId: number,
              public  isBasicProduct: boolean,
              public  isBatch: boolean,
              public  isDisplayedOnJCE: boolean,
              public  isDiscountable: boolean,
              public  isEnabled: boolean,
              public  originId: number,
              public  products: Good[],
              public  createdOn: string,
              public  createdBy: string,
              public  updatedOn: string,
              public  updatedBy: string) {
  }
}

export {Good, FormGoodModel}
