/**
 * @license
 * Copyright Akveo. All Rights Reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 */
import {APP_BASE_HREF, DatePipe} from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {APP_INITIALIZER, ErrorHandler, NgModule} from '@angular/core';
import { HttpModule } from '@angular/http';
import { CoreModule } from './@core/core.module';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { ThemeModule } from './@theme/theme.module';
import {NgbModalModule, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {PaginationComponent} from './@core/shared/pagination/pagination.component';
import {ToastyModule} from 'ng2-toasty';
import {ToasterModule} from 'angular2-toaster';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import {OAuthModule} from 'angular-oauth2-oidc';
import {OAuthService} from 'angular-oauth2-oidc';
import {AuthGuardService} from './@core/shared/auth/auth-guard.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {JceInterceptor} from './@core/data/services/jceInterceptor.service';
import { LocalStorageService} from 'angular-localstorage';

import {AuthJceModule} from './pages/auth/auth-jce.module';

import {
  AuthModule,
  OidcSecurityService,
  OpenIDImplicitFlowConfiguration,
  OidcConfigService,
  AuthWellKnownEndpoints
} from 'angular-auth-oidc-client';
import {AuthenticationService} from './@core/shared/auth/authentication.service';
import {NgAutoCompleteModule} from 'ng-auto-complete';
import {CeViewDetailComponent} from "./pages/ces/ce-view/ce-view-detail/ce-view-detail.component";
import {RouterModule} from "@angular/router";

export function loadConfig(oidcConfigService: OidcConfigService) {
  console.log('APP_INITIALIZER STARTING');
  return () => oidcConfigService.load_using_custom_stsServer('http://localhost:5000');
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    RouterModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpModule,
    AppRoutingModule,
    NgbModalModule,
    AuthJceModule,
    AuthModule.forRoot(),
    NgbModule.forRoot(),
    ThemeModule.forRoot(),
    CoreModule.forRoot(),
    ToastyModule.forRoot(),
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['http://localhost/5003'],
        sendAccessToken: true
      }
    }),


  ],
  exports: [ToastyModule],
  bootstrap: [AppComponent],
  providers: [
    OidcConfigService,
    // {
    //   provide: APP_INITIALIZER,
    //   useFactory: loadConfig,
    //   deps: [OidcConfigService],
    //   multi: true
    // },
    {provide: HTTP_INTERCEPTORS, useClass: JceInterceptor, multi: true},
    AuthenticationService,
    AuthGuardService,
    // { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: APP_BASE_HREF, useValue: '/' },
    [DatePipe]
    // LocalStorageService,
  ],
})
export class AppModule {
//   constructor(
//     public oidcSecurityService: OidcSecurityService
//   ) {
//     const openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();
//
//     openIDImplicitFlowConfiguration.stsServer = 'http://localhost:5000/';
//     openIDImplicitFlowConfiguration.redirect_url = window.location.origin + '/#/?';
//     // The Client MUST validate that the aud (audience) Claim contains
//     // its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.
//     // The ID Token MUST be rejected if the ID Token does not list the
//     // Client as a valid audience, or if it contains additional audiences not trusted by the Client.
//     openIDImplicitFlowConfiguration.client_id = 'jceFront';
//     openIDImplicitFlowConfiguration.response_type = 'id_token token';
//     openIDImplicitFlowConfiguration.scope = 'openid profile jce';
//     openIDImplicitFlowConfiguration.post_logout_redirect_uri =  'http://localhost:5002/#/dashbord';
//
//     //openIDImplicitFlowConfiguration.start_checksession = false;
//     //openIDImplicitFlowConfiguration.silent_renew = true;
//    // openIDImplicitFlowConfiguration.silent_renew_url = 'http://localhost:5000/silent-renew.html';
//    // openIDImplicitFlowConfiguration.post_login_route = '/dataeventrecords';
//     // HTTP 403
//    // openIDImplicitFlowConfiguration.forbidden_route = '/Forbidden';
//     // HTTP 401
//    // openIDImplicitFlowConfiguration.unauthorized_route = '/Unauthorized';
//     openIDImplicitFlowConfiguration.log_console_warning_active = true;
//     openIDImplicitFlowConfiguration.log_console_debug_active = true;
//     // id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time,
//     // limiting the amount of time that nonces need to be stored to
//     // prevent attacks.The acceptable range is Client specific.
// //    openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds = 10;
//
//     const authWellKnownEndpoints = new AuthWellKnownEndpoints();
//     authWellKnownEndpoints.issuer = 'http://localhost:5000/';
//
//
//     authWellKnownEndpoints.jwks_uri = 'http://localhost:5000/.well-known/openid-configuration/jwks';
//     authWellKnownEndpoints.authorization_endpoint = 'http://localhost:5000/connect/authorize';
//     authWellKnownEndpoints.token_endpoint = 'http://localhost:5000/connect/token';
//     authWellKnownEndpoints.userinfo_endpoint = 'http://localhost:5000/connect/userinfo';
//     authWellKnownEndpoints.end_session_endpoint = 'http://localhost:5000/connect/endsession';
//     authWellKnownEndpoints.check_session_iframe = 'http://localhost:5000/connect/checksession';
//     authWellKnownEndpoints.revocation_endpoint = 'http://localhost:5000/connect/revocation';
//     authWellKnownEndpoints.introspection_endpoint = 'http://localhost:5000/connect/introspect';
//
//     this.oidcSecurityService.setupModule(openIDImplicitFlowConfiguration, authWellKnownEndpoints);
//   }
}
