import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {KeyValuePairEnum} from '../models/Enums/keyValuePair.enum';
import {BehaviorSubject} from "rxjs/BehaviorSubject";


@Injectable()
export class ProductTypeService {

  constructor(private  http: HttpClient) {
  }

  types: KeyValuePairEnum[];
  private typesSource = new BehaviorSubject<KeyValuePairEnum[]>(this.types);
  currentTypes = this.typesSource.asObservable();

  // Pass data to siblings components

  changeTypes(types: KeyValuePairEnum[]) {
    this.typesSource.next(types);
  }

  getCurrentTypes() {
   return  this.typesSource.getValue();
  }

  url = environment.apiUrl;

  private readonly productTypeEndPoint = this.url + 'producttypes/';

  getAll() {
    return this.http.get<KeyValuePairEnum[]>(this.productTypeEndPoint)
      ;
  }

  getById(id: number) {
    return this.http.get(this.productTypeEndPoint + id)
      ;
  }
}
