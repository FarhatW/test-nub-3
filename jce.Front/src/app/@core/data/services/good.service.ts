import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Good} from '../models/products/Good';
import {HttpClient} from "@angular/common/http";
import {JceData} from "../models/JceData";
import {BatchList} from "../models/products/batchList";
import {ProductList} from "../models/products/productList";
import {environment} from "../../../../environments/environment";
import {GoodSave} from "../models/products/goodSave";
import {Supplier} from "../models/supplier/supplier";
import {BehaviorSubject} from "rxjs/BehaviorSubject";

@Injectable()
export class GoodService {

  constructor(private  http: HttpClient,
  ) {
  }

  url = environment.apiUrl;
  private readonly goodEndPoint = this.url + 'goods/';
  private readonly batchEndPoint = this.url + 'batches/';
  private readonly productEndPoint = this.url + 'products/';



  // API Calls
  getAll(filter) {
    console.log(filter);
    console.log(this.toQueryString(filter));
    return this.http.get<JceData<Good>>(this.goodEndPoint + '?' + this.toQueryString(filter))
      .map(res => res);
  }

  getByRefPintel(refPintel: string) {
    console.log(refPintel);
    return this.http.get<Good>(this.goodEndPoint + 'refPintel/' + refPintel)
      .map(res => res);
  }

  getById(id: number) {
    console.log(id);
    return this.http.get<Good>(this.goodEndPoint + id)
      .map(res => res);
  }

  create(good: GoodSave) {

    return this.http.post<Good>(
      (good.isBatch ? this.batchEndPoint : this.productEndPoint), good)
      .map(res => res);
  }

  update(good: GoodSave) {
    return this.http.put<Good>(
      (good.isBatch ? this.batchEndPoint : this.productEndPoint) + good.id, good)
      .map(res => res);
  }

  delete(id: number) {
    return this.http.delete(this.goodEndPoint + id);
  }

  multiCreateBatch(goodArray: Good[]) {
    return this.http.post<BatchList>(this.batchEndPoint + 'multiAdd', goodArray)
  }

  multiCreateProduct(goodArray: Good[]) {
    return this.http.post<ProductList>(this.productEndPoint + 'multiAdd', goodArray)
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
