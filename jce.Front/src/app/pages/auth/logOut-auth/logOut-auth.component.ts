import { Component, OnInit } from '@angular/core';
import {JwksValidationHandler, OAuthService} from 'angular-oauth2-oidc';
import {authConfig} from './authConfig';
import {OidcSecurityService} from 'angular-auth-oidc-client';

@Component({
  selector: 'ngx-login-auth',
  templateUrl: './logOut-auth.component.html',
  styleUrls: ['./logOut-auth.component.scss']
})
export class LogOutAuthComponent implements OnInit {

  constructor(private oauthService: OAuthService,
              public oidcSecurityService: OidcSecurityService) {

    // if (this.oidcSecurityService.moduleSetup) {
    //   this.doCallbackLogicIfRequired();
    // } else {
    //   this.oidcSecurityService.onModuleSetup.subscribe(() => {
    //     this.doCallbackLogicIfRequired();
    //   });
    // }


  }


  private doCallbackLogicIfRequired() {
    if (window.location.hash) {
      this.oidcSecurityService.authorizedCallback();
    }
  }
  // private configureWithNewConfigApi() {
  //   this.oauthService.configure(authConfig);
  //
  //
  // }


  public logoff() {
    this.oauthService.logOut();
  }


  ngOnInit() {
   // this.oidcSecurityService.authorize();
   // this.oauthService.initImplicitFlow();
    console.log('aeaze');
  }

}
