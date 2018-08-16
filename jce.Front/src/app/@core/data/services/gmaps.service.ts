import {Injectable, NgZone} from '@angular/core';
import {GoogleMapsAPIWrapper, LatLng, MapsAPILoader} from '@agm/core';
import {Observable } from "rxjs/Observable";
import {} from '@types/googlemaps';
import {Observer} from "rxjs/Observer";
import {Http} from "@angular/http";

declare var google: any;

@Injectable()
export class GmapsService  {
  API_KEY: string;
  API_URL: string;

  constructor(private http: Http) {
    this.API_KEY = 'AIzaSyAxUDeLuyXGwonPYC42syt8-49ntMUkUB4'
    this.API_URL = `https://maps.googleapis.com/maps/api/geocode/json?key=${this.API_KEY}&address=`;
  }

   findFromAddress(address: string, postalCode?:
    string, place?: string, province?: string, region?:
    string, country?: string): Observable<any> {
    let compositeAddress = [address];

    if (postalCode) compositeAddress.push(postalCode);
    if (place) compositeAddress.push(place);
    if (province) compositeAddress.push(province);
    if (region) compositeAddress.push(region);
    if (country) compositeAddress.push(country);

    let url = `${this.API_URL}${compositeAddress.join(',')}`;

      return  this.http.get(url).map(response => <any> response.json())

  }
}
