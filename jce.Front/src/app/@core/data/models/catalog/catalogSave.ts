import {CatalogProductSave} from '../products/catalogProductSave';
import {CatalogGoodSave} from "../products/catalogGoodSave";

export class CatalogSave {

  id: number;
  ceId: number;
  indexId: string;
  catalogType: number;
  isActif: boolean;
  expirationDate:  Date;
  displayPrice: boolean;
  productChoiceQuantity: number;
  catalogChoiceTypeId: number;
  booksQuantity: number;
  toysQuantity: number;
  subscriptionQuantity: number;

  catalogGoods: CatalogGoodSave[];

  createdBy: string;
  updatedBy: string;
}
