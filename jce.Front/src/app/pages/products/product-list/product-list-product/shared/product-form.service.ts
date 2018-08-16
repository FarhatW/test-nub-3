import {Injectable} from '@angular/core';

@Injectable()
export class ProductFormService {

  goodFormValidationMessages: any;

  goodFormErrors = {
    refPintel: '',
    details: '',
    title: '',
    price: '',
    indexId: '',
    goodDepartmentId: '',
    // file: '',
    supplierId: '',
    season: '',
    pintelSheetId: '',
    productTypeId: '',
    isDisplayedOnJCE: '',
    isDiscountable: '',
    originId: '',
  };

  goodFormErrorsBool = {
    refPintel: false,
    details: false,
    title: false,
    price: false,
    indexId: false,
    goodDepartmentId: false,
    // file: false,
    supplierId: false,
    originId: false,
    season: false,
    productTypeId: false,
    pintelSheetId: false,
    isDiscountable: false,
  };

  nameMin: number = 3;
  nameMax: number = 36;
  postalCodeNumber: number = 5;
  streetMin: 1;

  constructor() {

    this.goodFormValidationMessages = {
      refPintel: {
        required: 'Référence obligatoire',
        pattern: 'Doit être composée de 4 chiffres.'
      },
      details: {
        required: 'Ce champs est requis',
      },
      title: {
        required: 'Ce champs est requis',
      },
      price: {
        required: 'Ce champs est requis',
        pattern: 'Chiffre uniquement ex: 99.99',
        minlength: 'Le prix doit être supérieur à 0',
      },
      indexId: {
        required: 'Sélectionnez un index',
        nullValidator: 'Sélectionnez un index',
      },
      goodDepartmentId: {
        required: 'Sélectionnez un univers',
      },
      supplierId: {
        required: 'Sélectionnez un fournisseur.',
        nullValidator: 'Sélectionnez un fournisseur.',
      },
      season: {
        pattern: 'Entrée une année ex: 2018.'
      },
      pintelSheetId: {},
      productTypeId: {
        required: 'Sélectionnez un type de produit.',
      },
      originId: {
        required: 'Provenance requise.',
      }
    };
  }
}
