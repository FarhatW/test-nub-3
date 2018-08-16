import {Injectable} from '@angular/core';
import {CE} from '../models/ce/ce';
import {CeSave} from '../models/ce/ceSave';
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {HttpClient} from "@angular/common/http";
import {JceData} from "../models/JceData";
import {environment} from "../../../../environments/environment";

@Injectable()
export class CeService {
  ce: CE;

  constructor(private  http: HttpClient,
              ) {
  }

  url = environment.apiUrl;

  private readonly ceEndPoint = this.url + 'ces/';

  ces: CE[];
  private cesSource = new BehaviorSubject<CE[]>(this.ces);
  currentCes = this.cesSource.asObservable();

  // Pass data to siblings components

  changeCes(ces: CE[]) {
    this.cesSource.next(ces);
  }


  getAll(filter) {
    console.log('filter', filter)
    return this.http.get<JceData<CE>>(this.ceEndPoint + '?' + this.toQueryString(filter));
  }


  getById(id: number, filter) {
    return this.http.get<CE>(this.ceEndPoint + id + '?' + this.toQueryString(filter));
  }

  create(ce: CeSave) {
    ce.CreatedBy = 'tamere';
    ce.UpdatedBy = 'tamere';
    return this.http.post<CE>(this.ceEndPoint, ce);
  }

  update(id: number, ce: CeSave) {
    return this.http.put<CE>(this.ceEndPoint + ce.id, ce);
  }

  delete(id: number) {
    return this.http.delete(this.ceEndPoint + id);
  }

  toQueryString(obj) {
    const parts = [''];
    for (const property in obj) {
      const value = obj[property];
      if (value != null && value !== undefined) {
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }
    return parts.join('&');
  }
}
