import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';

import { NbMenuService, NbSidebarService } from '@nebular/theme';
import { UserService } from '../../../@core/data/users.service';
import { AnalyticsService } from '../../../@core/utils/analytics.service';
import {OAuthService} from "angular-oauth2-oidc";
import {NbUserMenuItem} from "@nebular/theme/components/user/user.component";
import {Router} from "@angular/router";
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {el} from "@angular/platform-browser/testing/src/browser_util";
import {AuthenticationService} from "../../../@core/shared/auth/authentication.service";

@Component({
  selector: 'ngx-header',
  styleUrls: ['./header.component.scss'],
  templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit, OnDestroy {
  loggedIn: Boolean;

  @Input() position = 'normal';

  user: any;
  @Output()
  a = new EventEmitter<NbUserMenuItem>();
  userMenu = [{ title: 'Profile' }, { title: 'Log out'}];

  constructor(private sidebarService: NbSidebarService,
              private menuService: NbMenuService,
              private userService: UserService,
              private analyticsService: AnalyticsService,
              private oauthService: OAuthService,
              public authService:  AuthenticationService,
              public oidcSecurityService: OidcSecurityService,
  private router : Router) {

    // this.authService.isLoggedInObs()
    //   .subscribe(flag => {
    //     this.loggedIn = flag;
    //     if (!flag) {
    //       this.authService.startSigninMainWindow();
    //     }
    //   });
  }
  ngOnDestroy(): void {
    this.oidcSecurityService.onModuleSetup.unsubscribe();
  }
  ngOnInit() {
    this.userService.getUsers()
      .subscribe((users: any) => this.user = users.nick);
  }

  public logoff(a) {
    console.log(a);
    if (a.title === 'Profile')
      this.authService.startSigninMainWindow();
    else
      this.authService.startSignoutMainWindow();

  }

  toggleSidebar(): boolean {

    this.sidebarService.toggle(true, 'menu-sidebar');
    return false;
  }

  toggleSettings(): boolean {
    this.sidebarService.toggle(false, 'settings-sidebar');
    return false;
  }

  goToHome() {
    this.menuService.navigateHome();
  }

  startSearch() {
    this.analyticsService.trackEvent('startSearch');
  }
}
