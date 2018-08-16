import {Scopes} from './Scopes';

export class Consent {
  clientName: string;
  clientUrl: string;
  clientLogoUrl: string;
  allowRememberConsent: boolean;
  identityScopes: Array<Scopes>;
  resourceScopes: Array<Scopes>;

}
