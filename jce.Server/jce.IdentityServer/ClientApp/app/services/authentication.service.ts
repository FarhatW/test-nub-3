import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import {Router} from '@angular/router';
import {Http} from '@angular/http';
import {HttpClient} from "@angular/common/http";
import {User} from "../Models/user";
import {ProcessLoginResult} from "../Models/ProcessLoginResult";
import {LoggedOutResource} from "../Models/LoggedOutResource";

@Injectable()
export class AuthenticationService {
    constructor(
      private router: Router,
      private http: HttpClient,
    ) { }


  login(email: string, password: string, returnUrl: string, rememberMe: boolean) {

    return this.http.post<ProcessLoginResult>('http://localhost:5000/api/users/authenticate',
      {
        email: email,
        password: password,
        RememberMe: rememberMe,
        returnUrl: returnUrl

      });
  }

    logOut(logoutId: string){

        return this.http.get<LoggedOutResource>('http://localhost:5000/api/users/logout/?logoutId='+ logoutId);
    }

}
