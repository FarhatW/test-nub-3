import {Consent} from './Consent';

export class ProcessConsentResult {

 isRedirect: boolean;
 redirectUri: string;
  hasValidationError: boolean;
 validationError: string;

 showView: boolean;
 viewModel: Consent;

}
