import {Injectable} from '@angular/core';

@Injectable()
export class UsersPasswordFormService {

  usersPasswordFormValidationMessages: any;
  usersPasswordFormErrors = {

    password: '',
    confirmPassword: '',


  };
  usersPasswordFormErrorsBool = {
    password: false  ,
    confirmPassword: false,
  };
  nameMin: number = 3;
  nameMax: number = 100;


  constructor() {

    this.usersPasswordFormValidationMessages = {

      password: {
        required: 'Le password est incorrect',
        pattern: 'Le mot de passe doit comporter 6 caractères minimum, ' +
        'combinant des lettres majuscules et minuscules, au moins un chiffre' +
        ' et au moins un caractère spécial.  '
      },
      confirmPassword: {
        required: 'mot de passe n\'est pas conforme',
        pattern: 'Le mot de passe n\'est pas conform '

      },
    };

  }
}
