import { Component, OnInit } from '@angular/core';
import {User} from "../../Models/user";
import {ActivatedRoute, Router} from "@angular/router";
import {ToastyService} from "ng2-toasty";
import {AuthenticationService} from "../../services/authentication.service";
import { ProcessLoginResult }  from "../../Models/ProcessLoginResult";
import '../../../assets/login-animation.js';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    returnUrl: any;
    model: any = {};
    user = User;
    processLoginResult: ProcessLoginResult;
  constructor(
      private router: Router,
      private authenticationService: AuthenticationService,
      private toastyService: ToastyService,
      private activatedRoute: ActivatedRoute,
  ) { }
  
    ngAfterViewInit() {
        (window as any).initialize();
    }
    
  ngOnInit() {
      // reset login status

      // get return url from route parameters or default to '/'
      this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
  }

    login() {
      
        this.authenticationService.login(this.model.email, this.model.password, this.returnUrl, false)
            .subscribe(
            result => {
                this.processLoginResult = result;

                console.log("USER", this.processLoginResult);
                window.location.href = this.processLoginResult.redirectUri;
            },

            error => {
                console.log(error);
                this.toastyService.error({
                    title: 'Error : ' + error.status + ' ' + error.statusText,
                    msg: error._body,
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                });
            }
        );
    }


}
