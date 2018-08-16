import {Good} from "./Good";

export class GoodSave {
  id: number;
  refPintel: string;
  details: string;
  title: string;
  price: number;
  indexId: string;
  productTypeId: number;
  goodDepartmentId: number;
  // file: string;
  supplierId: number;
  season: string;
  pintelSheetId: number;
  isBasicProduct: boolean;
  isBatch: boolean;
  isDisplayedOnJCE: boolean;
  isDiscountable: boolean;
  originId: number;
  isEnabled: boolean;
  products: Good[];
  createdOn: string;
  createdBy: string;
  updatedOn: string;
  updatedBy: string
}
