import {Injectable} from '@angular/core';
import {CE} from "../../../../../@core/data/models/ce/ce";
import {BehaviorSubject} from "rxjs/BehaviorSubject";

@Injectable()
export class SupplierFormService {

  supplierFormValidationMessages: any;

  supplierFormErrors = {
    name: '',
    description: '',
    supplierRef: '',

  };

  supplierFormErrorsBool = {
    name: false,
    description: false,
    supplierRef: false,
  };

  nameMin: number = 3;
  nameMax: number = 36;
  postalCodeNumber: number = 5;
  streetMin: 1;

  constructor() {

    this.supplierFormValidationMessages = {
      name: {
        required: 'Ce champs est requis',
        minlength: 'Min 3 caractères.'
      },
      description: {
        required: 'Ce champs est requis',
        minlength: 'Min 3 caractères.'
      },
      supplierRef: {
        required: 'Ce champs est requis',
        minlength: 'Min 3 caractères.'


      },
    };
  }
}
