import {Injectable} from '@angular/core';

@Injectable()
export class UserJceFromService {


  userFormValidationMessages: any;
  userFormErrors = {
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    idCe: '',
    CreatedBy: '',
    UpdatedBy: '',
    agency: '',
    service: '',
    company: '',
    streetNumber: '',
    address1: '',
    address2: '',
    postalCode: '',
    city: '',
    addressExtra: '',
    ceId:''
  };

  userFormErrorsBool = {
    firstName: false,
    lastName: false ,
    email: false  ,
    phone: false  ,
    idCe: false,
    CreatedBy: false,
    UpdatedBy: false,
    agency: false,
    service: false,
    company: false,
    streetNumber: false,
    address1: false,
    address2: false,
    postalCode: false,
    city: false,
    addressExtra: false,
    ceId:false
  };




  nameMin: number = 3;
  nameMax: number = 36;
  postalCodeNumber: number = 5;
  streetMin: 1;

  _compare(item1, item2): boolean {
    return item1 === item2;
  }

  constructor() {

    this.userFormValidationMessages = {

      firstName: {
        required: 'Ce champs est requis',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou plus.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      lastName: {
        required: 'Ce champs est requis',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou plus.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'
      },
      email: {
        required: 'Ce champs est requis',
        pattern: 'L\'adresse email doit être valide'

      },
      phone: {
        required: 'Le numéro de téléphone est requis',
        pattern: 'Le numéro de téléphone doit contenir 10 chiffres'
      },
      company: {
        required: 'Le nom de la société est obligatoire',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou moins.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'
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
      ceId: {
        required: 'Le CE  est requis',
      },
    };

  }
}
