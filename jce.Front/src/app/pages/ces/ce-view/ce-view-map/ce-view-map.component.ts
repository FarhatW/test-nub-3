import {Component, ElementRef, Input, NgZone, OnChanges, OnInit, ViewChild} from '@angular/core';
import {AgmMap, GoogleMapsAPIWrapper, MapsAPILoader} from '@agm/core';
import {FormControl} from '@angular/forms';
import {Observable} from "rxjs/Observable";
import {} from '@types/googlemaps'
import {Address} from "../../../../@core/data/models/shared/address";
import {CE} from "../../../../@core/data/models/ce/ce";
import {GmapsService} from "../../../../@core/data/services/gmaps.service";

declare var google: any;


@Component({
  selector: 'ngx-ce-view-map',
  styleUrls: ['./ce-view-map.component.scss'],
  templateUrl: './ce-view-map.component.html',
})
export class CeViewMapComponent implements OnChanges {

  constructor(private __loader: MapsAPILoader,
              private __zone: NgZone,
              private gmapService: GmapsService) {
  }

  zoom = 15;
  lat = 0;
  lng = 0;

  @Input() address: Address;


  ngOnChanges(changes) {
    console.log('changes', changes);
    console.log('changesaddress', this.address);
    if (this.address) {
      this.updateLatLngFromAddress(this.address)
    }
  }


  updateLatLngFromAddress(ad: Address)  {
    this.gmapService
      .findFromAddress(ad.streetNumber + ' ' + ad.address1 + ' ' + ad.city,
        ad.postalCode
      )
      .subscribe(response => {
        if (response.status === 'OK') {
          this.lat = response.results[0].geometry.location.lat;
          this.lng = response.results[0].geometry.location.lng;
        } else if (response.status === 'ZERO_RESULTS') {
          console.log('geocodingAPIService', 'ZERO_RESULTS', response.status);
        } else {
          console.log('geocodingAPIService', 'Other error', response.status);
        }
      });
  }

  private _setAddress(ad: Address): string {
    console.log('addresd', ad)

    return ad.streetNumber + ' '
      + ad.address1 + ' '
      + ad.postalCode + ' '
      + ad.city
  }
}
