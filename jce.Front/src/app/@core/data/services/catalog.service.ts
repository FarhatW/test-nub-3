import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Catalog} from '../models/catalog/catalog';
import {CatalogSave} from '../models/catalog/catalogSave';
import {Observable} from 'rxjs/Observable';
import {Subject} from "rxjs/Subject";
import {CatalogLettersSave} from "../models/catalog/catalogLettersSave";
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class CatalogService {

  constructor(private  http: HttpClient) { }
  url = environment.apiUrl;

  private readonly catalogEndPoint = this.url + 'catalogs/';

  getAll(filter) {
    return this.http.get(this.catalogEndPoint + '?' + this.toQueryString(filter));
  }

  getById(id: number, filter) {
    console.log(id);
    return this.http.get(this.catalogEndPoint + id);

  }

  getByCEId(id: number, filter) {
    console.log('url', this.catalogEndPoint + id + '?' + this.toQueryString(filter));

    return this.http.get<Catalog>(
      this.catalogEndPoint + id + '?' + this.toQueryString(filter));



  }

  create(catalog: CatalogSave) {
    return this.http.post<Catalog>(this.catalogEndPoint, catalog);
  }

  update(catalog: CatalogSave) {
    return this.http.put<Catalog>(this.catalogEndPoint + catalog.id, catalog);
  }

  updatePintelSheets(catalog: CatalogSave, filter) {
    console.log('filter', filter);
    return this.http.put<Catalog>(this.catalogEndPoint + 'updatePintelSheets/' + catalog.id +
      '?' + this.toQueryString(filter), catalog);
  }

  updateLetters(catalog: CatalogLettersSave, filter) {
    console.log('filter', filter);
    return this.http.put<Catalog>(this.catalogEndPoint + 'updateLetters/' + catalog.id +
      '?' + this.toQueryString(filter), catalog);
  }

  delete(id: number) {
    return this.http.delete(this.catalogEndPoint + id);
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
