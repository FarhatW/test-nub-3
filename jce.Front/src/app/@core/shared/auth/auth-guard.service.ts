import {Injectable, OnInit} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot} from '@angular/router';
import {OAuthService} from 'angular-oauth2-oidc';
import * as jwt_decode from 'jwt-decode';
import {HelpersService} from '../../utils/helpers.service';
import {Observable} from 'rxjs/Observable';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {map} from 'rxjs/operators';

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private oauthService: OAuthService, private router: Router,
              private oidcSecurityService: OidcSecurityService, private helperService: HelpersService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    console.log(route + '' + state);
    console.log('AuthorizationGuard, canActivate');

    return this.oidcSecurityService.getIsAuthorized().pipe(
      map((isAuthorized: boolean) => {
        console.log('AuthorizationGuard, canActivate isAuthorized: ' + isAuthorized);

        if (isAuthorized) {
          return true;
        }

       // this.router.navigate(['/unauthorized']);
        return false;
      })
    );
  }
    // if (this.oauthService.hasValidAccessToken()) {
    //   const user = this.helperService.getUserData();
    //   if ( route.data.roles !== undefined) {
    //     console.log('route.data.roles', route.data);
    //     for (let index = 0; index < user.role.length; index++) {
    //
    //       if (route.data.roles.includes(user.role[index])) {
    //         return true;
    //       }
    //     }
    //     return false;
    //   }
    //   return true ;
    // } else {
    //   this.router.navigate(['/logOut']);
    //   //console.log('auth guard');
    //  // this.oauthService.logOut();
    //
    //   return false;
    // }



}
