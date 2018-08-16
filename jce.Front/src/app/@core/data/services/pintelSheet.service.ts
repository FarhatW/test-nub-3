import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {FormPintelSheet, PintelSheet} from '../models/pintelSheet';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {JceData} from "../models/JceData";
import {PintelSheetSave} from "../models/pintelSheet/pintelSheetSave";

@Injectable()
export class PintelSheetService {

  constructor(private  http: HttpClient) {
  }

  url = environment.apiUrl;

  private readonly pintelSheetEndPoint = this.url + 'pintelSheets/';

  getAll(filter) {
    console.log(filter);
    console.log(this.toQueryString(filter));
    return this.http.get<JceData<PintelSheet>>(this.pintelSheetEndPoint + '?' + this.toQueryString(filter));
  }

  getById(id: number) {
    console.log(id);
    return this.http.get<PintelSheet>(this.pintelSheetEndPoint + id);
  }


  create(pintelSheet: PintelSheetSave) {
    return this.http.post<PintelSheetSave>(this.pintelSheetEndPoint, pintelSheet);
  }

  update(pintelSheet: PintelSheetSave) {
    return this.http.put<PintelSheetSave>(this.pintelSheetEndPoint + pintelSheet.id, pintelSheet);
  }

  delete(id: number) {
    return this.http.delete<PintelSheet>(this.pintelSheetEndPoint + id);
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
