import {
  AbstractControl,
  AsyncValidatorFn,
  Form, FormArray,
  FormControl, FormGroup,
  ValidationErrors
} from '@angular/forms';

export class CustomValidators {

  static minMaxProducts(c: FormControl): ValidationErrors {
    const productNumber = Number(c.value);
    const min = 2;
    const max = 5;
    const isValid = !isNaN(productNumber) && productNumber >= min && productNumber <= max;
    const message = {
      maxProd: {
        message: 'Le nombre de produit doit être compris entre ' + min + ' et ' + max + ' .'
      }
    };
    return isValid ? null : message;
  }

  static expirationDate(c: FormControl) {
    const expDate = new Date(c.value);
    console.log('cvalue', expDate);
    const currentYear = new Date();
    console.log('currentYear', currentYear);
    const isValid = expDate > currentYear;

    const message = {
      expirationDate: {
        message: 'La date d expiration doit être supérieur à la date du jour'
      }
    };

    return isValid ? null : message;
  }
}
