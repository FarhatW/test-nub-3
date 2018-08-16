import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Product} from '../models/products/product';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class ProductService {

  constructor(private  http: HttpClient) {}

  url = environment.apiUrl;


  private readonly productEndPoint = this.url + 'catalogGoods/';

  getAll(filter) {
    console.log(filter);
    console.log( this.toQueryString(filter));
    return this.http.get(this.productEndPoint + '?' + this.toQueryString(filter));
  }


  getByRefPintel(refPintel: string) {
    console.log(refPintel);
    return this.http.get(this.productEndPoint + 'refPintel/' + refPintel);
  }

  getById(id: number) {
    console.log(id);
    return this.http.get(this.productEndPoint + id);
  }

  create(product: Product) {
    return this.http.post(this.productEndPoint, product);
  }

  update(product: Product) {
    return this.http.put(this.productEndPoint + product.id, product);
  }

  delete(id: number) {
    return this.http.delete(this.productEndPoint + id);
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
