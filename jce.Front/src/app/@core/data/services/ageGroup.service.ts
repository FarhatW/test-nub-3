import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {environment} from "../../../../environments/environment";


@Injectable()
export class AgeGroupService {

  constructor(private  http: Http) {
  }

  url = environment.apiUrl;

  private readonly ageGroupEndPoint = this.url + 'agegroups/';

  getAll() {
    return this.http.get(this.ageGroupEndPoint)
      .map(res => res.json());
  }

  getById(id: number) {
    return this.http.get(this.ageGroupEndPoint + id)
      .map(res => res.json());
  }
}
