import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {KeyValuePairEnum} from '../models/Enums/keyValuePair.enum';
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {Supplier} from "../models/supplier/supplier";


@Injectable()
export class OriginService {

  constructor(private  http: HttpClient) {
  }
  url = environment.apiUrl;
  private readonly originEndPoint = this.url + 'origins/';

  origins: KeyValuePairEnum[];
  private originsSource = new BehaviorSubject<KeyValuePairEnum[]>(this.origins);
  currentOrigins = this.originsSource.asObservable();

  // Pass data to siblings components

  changeOrigins(origins: KeyValuePairEnum[]) {
    this.originsSource.next(origins);
  }

  getAll() {
    return this.http.get<KeyValuePairEnum[]>(this.originEndPoint);
  }

  getById(id: number) {
    return this.http.get(this.originEndPoint + id);
  }

  getByName(letter: string) {
    return this.http.get(this.originEndPoint + letter);
  }
}
