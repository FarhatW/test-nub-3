import {Injectable} from '@angular/core';

@Injectable()
export class UserAuthFormService {


  userFormValidationMessages: any;
  userAuthFormErrors = {
    userName: '',
    email: '',
    // password: '',
    // confirmPassword: '',
    CreatedBy: '',
    UpdatedBy: '',
    roles: ''

  };

  userFormErrorsBool = {
    userName: false  ,
    email: false  ,
    // password: false  ,
    // confirmPassword: false,
    CreatedBy: false,
    UpdatedBy: false,
    roles: false

  };

  nameMin: number = 3;
  nameMax: number = 100;


  constructor() {

    this.userFormValidationMessages = {

      userName: {
        required: 'Ce champs userName est requis',
        minlength: 'Le nom doit comprendre ' + this.nameMin + ' caractères ou moins.',
        maxlength: 'Le nom doit comprendre ' + this.nameMax + ' caractères ou moins.'

      },
      email: {
        required: 'Ce champs email est requis',
        pattern: 'L\'adresse email doit être valide'

      },
      roles: {
        required: 'Le champs role est requis',
      },
      // password: {
      //   required: 'Le password est incorrect',
      //   pattern: 'Le mot de passe doit comporter 6 caractères minimum, ' +
      //             'combinant des lettres majuscules et minuscules, au moins un chiffre' +
      //             ' et au moins un caractère spécial.  '
      // },
      // confirmPassword: {
      //   required: 'mot de passe n\'est pas conforme',
      //   pattern: 'Le mot de passe n\'est pas conform '
      //
      // },
    };

  }
}
