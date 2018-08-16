import { Component } from '@angular/core';
import {AuthenticationService} from "../../@core/shared/auth/authentication.service";



@Component({
  selector: 'ngx-dashboard',
  styleUrls: ['./dashboard.component.scss'],
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent {

  loggedIn: Boolean;

  constructor(private authService: AuthenticationService) {
    // this.authService.isLoggedInObs()
    //   .subscribe(flag => {
    //     this.loggedIn = flag;
    //     if (!flag) {
    //       this.authService.startSigninMainWindow();
    //     }
    //   });
  }

  login() {
    this.authService.startSigninMainWindow();
  }

  logout() {
    this.authService.startSignoutMainWindow();
  }

}
