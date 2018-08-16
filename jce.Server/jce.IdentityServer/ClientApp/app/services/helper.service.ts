import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
@Injectable()
export class HelperService {

  constructor() { }

    public jwt() {
      // create authorization header with jwt token

      // var currentUser = JSON.parse(this.localStorageService.get('currentUser') || '{}');
      // if (currentUser && currentUser.token) {
      //     var headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
      //     return new RequestOptions({ headers: headers });
      }

}
