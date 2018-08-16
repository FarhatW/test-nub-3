import {CatalogProduct} from '../products/catalogProduct';
import {CatalogGood} from '../products/catalogGood';

class Catalog {

  id: number;
  ceId: number;
  indexId: string;
  catalogType: number;
  isActif: boolean;
  expirationDate: Date;
  displayPrice: boolean;
  productChoiceQuantity: number;
  catalogChoiceTypeId: number;
  booksQuantity: number;
  toysQuantity: number;
  subscriptionQuantity: number;

  catalogGoods: CatalogGood[];

  createdBy: string;
  updatedBy: string;
  // constructor(id: number, ceId: number, indexId: string, catalogType: number, isActif: boolean,
  //             expirationDate: string, displayPrice: boolean, productChoiceQty: number, expiration: Date,
  //             isUniqueChoice: boolean, isMultipleChoice: boolean, isCumulatedProductChoice: boolean,
  //             booksQty: number, toysQty: number, subQty: number, goods: CatalogGood[],
  //             createdBy: string, updatedBy: string) {}

}

class FormCatalogModel {
  constructor(public id: number,
              public ceId: number,
              public catalogType: number,
              public isActif: boolean,
              public expirationDate: Date,
              public displayPrice: boolean,
              public productChoiceQuantity: number,
              public catalogChoiceTypeId: number,
              public booksQuantity: number,
              public  toysQuantity: number,
              public  subscriptionQuantity: number,
              public  catalogGoods: CatalogGood[]) {
  }
}

export {Catalog, FormCatalogModel};
