import {OAuthService} from 'angular-oauth2-oidc';
import * as jwt_decode from 'jwt-decode';
import {Router} from "@angular/router";
import {Injectable} from "@angular/core";

@Injectable()
export class HelpersService {

  constructor(private oauthService: OAuthService, private router: Router) {

  }
  getUserData() {
    if (this.oauthService.hasValidAccessToken())
          return  this.getDecodedAccessToken(this.oauthService.getAccessToken());
    //else
     //this.router.navigate(['/authenticate/logOut']);
  }

  private getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    }
    catch (Error) {
      return null;
    }
  }

  verifyAutorData(data: any, create: boolean){
    if(data.CreatedBy === null)
        data.CreatedBy = this.getUserData().sub

    if(!create)
      data.UpdatedBy = this.getUserData().sub
  }
  public toQueryString(obj) {
    const parts = [''];
    for (const property in obj){
      const value =  obj[property];
      if (value != null && value !== undefined) {
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }
    return parts.join('&');

  }
}
