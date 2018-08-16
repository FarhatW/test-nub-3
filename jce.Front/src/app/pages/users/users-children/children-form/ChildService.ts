import {Injectable} from '@angular/core';

@Injectable()
export class ChildService {


  userFormValidationMessages: any;
  userFormErrors = {
    firstName: '',
    lastName: '',
    birthDate: '',
    gender: '',
    isEnabled: '',
    amountParticipationCe: '',
    personJceProfileId: '',
    createdBy: '',
    updatedBy: '',
    createdOn: '',
    updatedOn: ''
  };

  userFormErrorsBool = {
    firstName: false,
    lastName: false ,
    birthDate: false ,
    gender: false ,
    isEnabled: false ,
    amountParticipationCe: false ,
    personJceProfileId: false ,
    createdBy: false ,
    updatedBy: false ,
    createdOn: false ,
    updatedOn: false
  };




  nameMin: number = 3;
  nameMax: number = 36;


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
      birthDate: {
        required: 'Ce champs est requis',
        pattern: 'Ce champs doit être valide'

      },
      gender: {
        required: 'Le numéro de téléphone est requis',
        pattern: 'Le numéro de téléphone doit contenir 10 chiffres'
      },
      amountParticipationCe: {
        required: 'Le montant de participation est obligatoire',
        pattern: 'le montant de participation doit être valide'
      }

    };

  }
}
