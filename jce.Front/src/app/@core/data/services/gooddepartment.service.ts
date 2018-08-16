import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {HttpClient} from "@angular/common/http";
import {LetterEnum} from "../models/Enums/letter.enum";
import {KeyValuePairEnum} from "../models/Enums/keyValuePair.enum";
import {environment} from "../../../../environments/environment";
import {BehaviorSubject} from "rxjs/BehaviorSubject";


@Injectable()
export class GoodDepartmentService {


  constructor(private  http: HttpClient) {
  }

  departments: KeyValuePairEnum[];
  private departmentsSource = new BehaviorSubject<KeyValuePairEnum[]>(this.departments);
  currentDepartments = this.departmentsSource.asObservable();

  // Pass data to siblings components

  changeDepartments(departments: KeyValuePairEnum[]) {
    this.departmentsSource.next(departments);
  }

  getCurrentDepartments() {
    return this.departmentsSource.getValue();
  }

  url = environment.apiUrl;

  private readonly goodDepartmentEndPoint = this.url + 'gooddepartments/';

  getAll() {
    return this.http.get<KeyValuePairEnum[]>(this.goodDepartmentEndPoint)      ;
  }

  getById(id: number) {
    return this.http.get(this.goodDepartmentEndPoint + id)
      .map(res => res);
  }
}
