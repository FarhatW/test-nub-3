export class Product {

  id: number;
  refPintel: string;
  details: string;
  title: string;
  price: number;
  dateMin: string;
  dateMax: string;
  clientAliasProduct: string;
  supplierId: number;
  indexID: string;
}


class FormProductModel {

  constructor(public id: number,
              public refPintel: string,
              public details: string,
              public title: string,
              public  price: number,
              public dateMin: string,
              public dateMax: string,
              public clientAliasProduct: string,
              public supplierId: number,
              public indexID: string) {
  }

}
