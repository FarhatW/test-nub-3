import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {CE} from "../../../@core/data/models/ce/ce";

@Injectable()
export class CeFormService {
  // Passage des données entre components

  ce: CE;
  formSteps: number;

  private formStepsSource = new BehaviorSubject<number>(this.formSteps);
  currentStep = this.formStepsSource.asObservable();

  changeFormStep(step: number){
    console.log('step', step);
    this.formStepsSource.next(step);
  }


  private ceSource = new BehaviorSubject<CE>(this.ce);
  currentCe = this.ceSource.asObservable();

  changeCE(ce: CE) {
    this.ceSource.next(ce);
  }

  ceFormValidationMessages: any;
  catalogFormValidationMessages: any;

  ceFormErrors = {
    name: '',
    fax: '',
    telephone: '',
    logo: '',
    actif: '',
    isDeleted: '',
    createdBy: '',
    updatedBy: '',
    agency: '',
    service: '',
    company: '',
    streetNumber: '',
    address1: '',
    address2: '',
    postalCode: '',
    city: '',
    addressExtra: '',
    adminJceProfileId: ''
  };

  ceFormErrorsBool = {
    name: false,
    fax: false,
    telephone: false,
    logo: false,
    actif: false,
    isDeleted: false,
    createdBy: false,
    updatedBy: false,
    agency: false,
    service: false,
    company: false,
    streetNumber: false,
    address1: false,
    address2: false,
    postalCode: false,
    city: false,
    addressExtra: false,
    adminJceProfileId: false

  };

  catalogFormErrors = {
    expirationDate: '',
    catalogType: '',
    productChoiceQuantity: '',
  };

  catalogFormErrorsIsDirty = {
    expirationDate: false,
    catalogType: false,
    productChoiceQuantity: false,
  }


  nameMin: number = 3;
  nameMax: number = 36;
  postalCodeNumber: number = 5;
  streetMin: 1;

  _compare(item1, item2): boolean {
    return item1 === item2;
  }

  constructor() {

    this.ceFormValidationMessages = {
      company: {
        required: 'Le nom de la société est obligatoire',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou moins.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      name: {
        required: 'Ce champs est requis',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou plus.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      fax: {
        pattern: 'Le numéro doit contenir 10 chiffres'
      },
      telephone: {
        required: 'Le numéro de téléphone est requis',
        pattern: 'Le numéro de téléphone doit contenir 10 chiffres'
      },
      address1: {
        required: 'L addresse est requise.',
        minlength: 'L addresse doit comprendre ' + this.nameMin + ' caractères ou plus.',
        maxlength: 'L addresse doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      postalCode: {
        required: 'Le CP est obligatoire.',
        pattern: 'CP invalide'
      },
      city: {
        required: 'La ville  est requise',
        minlength: 'La ville doit comprendre ' + this.nameMin + ' caractères ou plus.',
        maxlength: 'La ville doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      streetNumber: {
        min: 'Supérieur à 0'
      },
      adminJceProfileId: {
        required: 'Veuillez choisir un commercial.'
      }
    };

    this.catalogFormValidationMessages = {
      catalogType: {
        required: 'Type obligatoire?'
      },
      expirationDate: {
        // required: 'Date obligatoire.',
        // expirationDate: 'Date inférieur à la date du jour.'
      }
    }
  }
}
