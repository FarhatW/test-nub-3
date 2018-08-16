import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Supplier} from '../models/supplier/supplier';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {JceData} from "../models/JceData";
import {BehaviorSubject} from "rxjs/BehaviorSubject";

@Injectable()
export class SupplierService {

  constructor(private  http: HttpClient) {
  }

  suppliers: Supplier[];
  private suppliersSource = new BehaviorSubject<Supplier[]>(this.suppliers);
  currentSuppliers = this.suppliersSource.asObservable();
  // Pass data to siblings components

  changeSuppliers(suppliers: Supplier[]) {
    this.suppliersSource.next(suppliers);
  }

  getCurrentSuppliers() {
    return this.suppliersSource.getValue();
  }

  url = environment.apiUrl;

  private readonly supplierEndPoint = this.url + 'suppliers/';

  getAll(filter) {
    return this.http.get<JceData<Supplier>>(this.supplierEndPoint + '?' + this.toQueryString(filter));
  }

  getById(id: number) {
    console.log(id);
    return this.http.get<Supplier>(this.supplierEndPoint + id);
  }


  create(supplier: Supplier) {
    return this.http.post<Supplier>(this.supplierEndPoint, supplier);
  }

  update(supplier: Supplier) {
    return this.http.put<Supplier>(this.supplierEndPoint + supplier.id, supplier);
  }

  delete(id: number) {
    return this.http.delete(this.supplierEndPoint + id);
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
