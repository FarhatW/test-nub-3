import { Injectable } from '@angular/core';
import {Http} from "@angular/http";
import {HttpClient} from "@angular/common/http";
import {KeyValuePairEnum} from "../models/Enums/keyValuePair.enum";
import {environment} from "../../../../environments/environment";

@Injectable()
export class CatalogChoiceTypeService {

  constructor(private  http: HttpClient) {
  }
  url = environment.apiUrl;


  private readonly goodDepartmentEndPoint = this.url + 'catalogchoicetypes';

  getAll() {
    return this.http.get<KeyValuePairEnum[]>(this.goodDepartmentEndPoint)
     ;
  }

  getById(id: number) {
    return this.http.get(this.goodDepartmentEndPoint + id)
     ;
  }

}
