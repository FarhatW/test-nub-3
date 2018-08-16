import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {CeSetup} from '../models/ce/ceSetup';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Injectable()
export class CeSetupService {

  constructor(private  http: HttpClient) {}

  url = environment.apiUrl;
  private readonly ceSetupEndPoint = this.url + 'cesetups/';

  getAll(filter) {
    console.log(filter);
    console.log( this.toQueryString(filter));
    return this.http.get(this.ceSetupEndPoint + '?' + this.toQueryString(filter)).map(res => res);
  }

  getById(id: number) {
    console.log(id);
    return this.http.get(this.ceSetupEndPoint + id).map(res => res);
  }

  getByCEId(ceId: number) {
    return this.http.get<CeSetup>(this.ceSetupEndPoint + 'byce/' + ceId ).map(res => res);
  }

  create(ceSetup: CeSetup) {
    return this.http.post<CeSetup>(this.ceSetupEndPoint, ceSetup).map(res => res);
  }

  update(ceSetup: CeSetup) {
    return this.http.put<CeSetup>(this.ceSetupEndPoint + ceSetup.id, ceSetup).map(res => res);
  }

  delete(id: number) {
    return this.http.delete(this.ceSetupEndPoint + id);
  }

  toQueryString(obj) {
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
