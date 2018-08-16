import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Provider} from '../models/provider/provider';

@Injectable()
export class ProviderService {

  constructor(private  http: Http) {
  }

  private readonly providerEndPoint = 'http://localhost:60383/api/providers/';

  getAll(filter) {
    console.log(filter);
    console.log(this.toQueryString(filter));
    return this.http.get(this.providerEndPoint + '?' + this.toQueryString(filter)).map(res => res.json());
  }

  getById(id: number) {
    console.log(id);
    return this.http.get(this.providerEndPoint + id).map(res => res.json());
  }


  create(provider: Provider) {
    return this.http.post(this.providerEndPoint, provider).map(res => res.json());
  }

  update(provider: Provider) {
    return this.http.put(this.providerEndPoint + provider.id, provider).map(res => res.json());
  }

  delete(id: number) {
    return this.http.delete(this.providerEndPoint + id);
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
