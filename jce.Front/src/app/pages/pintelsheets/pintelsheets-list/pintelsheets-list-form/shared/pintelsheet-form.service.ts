import {Injectable} from '@angular/core';

@Injectable()
export class PintelsheetFormService {

  pintelsheetFormValidationMessages: any;

  pintelsheetFormErrors = {
    sheetId: '',
    filePath: '',
    season: '',
    ageGroupId: ''
  };

  pintelsheetFormErrorsBool = {
    sheetId: false,
    filePath: false,
    season: false,
    ageGroupId: false
  };

  constructor() {

    this.pintelsheetFormValidationMessages = {
      sheetId: {
        required: 'Ce champs est requis',
      },
      filePath: {
        required: 'Ce champs est requis',
        minlength: 'Min 3 caractères.'
      },
      season: {
        required: 'Ce champs est requis',
      },
      ageGroupId: {
        required: 'Le goupe dage est requis'
      }
    };
  }
}
