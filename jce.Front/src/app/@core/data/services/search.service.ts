import {Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {JceData} from "../models/JceData";
import {Good} from "../models/products/Good";
import {CE} from "../models/ce/ce";
import {Supplier} from "../models/supplier/supplier";
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {KeyValuePairEnum} from "../models/Enums/keyValuePair.enum";

@Injectable()
export class SearchService {
  url = environment.apiUrl;
  queryUrl: string = '?&search=';
  pageSize: string = '&pageSize=10';
  page: string = '&page=1';
  isBatch: string = '&isbatch='
  goods: Good[] = [];
  request: string = '';

  constructor(private http: HttpClient) {
  }


  search(terms: Observable<string>, query) {
    return terms.debounceTime(100)
      .distinctUntilChanged()
      .switchMap(term =>
        term.length >= 3 ?
          this.searchEntries(term, query) : Observable.of(this.goods));
  }
  searchPerPage(terms: Observable<string>) {
    return terms.debounceTime(100)
      .distinctUntilChanged()
      .switchMap(term =>
        term.length >= 3 ?
          // this.searchEntries(term, query)
          this.setSearchTerm(term)
          :
          Observable.of(this.goods));
  }

  searchTerm: string;
  private searchTermSource = new BehaviorSubject<string>(this.searchTerm);

  currentSearchTerm =  this.searchTermSource.asObservable();

  setSearchTerm(searchTerm: string) {
    this.searchTermSource.next(searchTerm);
    return Observable.of([]);
  }

  getSearchTerm(): string {
    return this.searchTermSource.getValue();
  }


  searchEntries(term, query) {

    switch (query.searchType) {

      case 'goods':
        return this.http
          .get<JceData<Good>>(this.url + query.searchType +
            this.queryUrl + term + this.pageSize
            + this.page
            + this.isBatch + query);

      case 'suppliers':
        return this.http
          .get<JceData<Supplier>>(this.url + query.searchType
            + this.queryUrl + term + this.pageSize
            + this.page
            + query)

      case 'ces':
        return this.http
          .get<JceData<CE>>(this.url + query.searchType +
            this.queryUrl + term + this.pageSize
            + this.page
            + query);

      case 'goods2Batch':
        console.log('url', this.url + query.searchType +
          this.queryUrl + term)
        return this.http
          .get<JceData<Good>>(this.url + query.searchType +
            this.queryUrl + term );

      default:
        return [];

    }






  }
}
