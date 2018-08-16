import { Injectable } from '@angular/core';
import {Http} from '@angular/http';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {KeyValuePairEnum} from "../models/Enums/keyValuePair.enum";

@Injectable()
export class CatalogTypeService {

  constructor(private  http: HttpClient ) {
  }

  url = environment.apiUrl;

  private readonly goodDepartmentEndPoint = this.url + 'catalogtypes';

  getAll() {
    return this.http.get<KeyValuePairEnum[]>(this.goodDepartmentEndPoint)
      ;
  }

  getById(id: number) {
    return this.http.get(this.goodDepartmentEndPoint + id)
      ;
  }
}
