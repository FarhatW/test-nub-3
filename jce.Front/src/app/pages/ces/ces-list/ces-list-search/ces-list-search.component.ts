import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {SearchService} from "../../../../@core/data/services/search.service";
import {Subject} from "rxjs/Subject";
import {Good} from "../../../../@core/data/models/products/Good";
import {CE} from "../../../../@core/data/models/ce/ce";
import {CeService} from "../../../../@core/data/services/ce.service";

@Component({
  selector: 'ngx-ces-list-search',
  templateUrl: './ces-list-search.component.html',
  styleUrls: ['./ces-list-search.component.scss']
})
export class CesListSearchComponent implements OnInit, OnChanges {


  @Output() clearSearchArr = new EventEmitter<any>();
  @Output() setSearch = new EventEmitter<string>();
  @Output() searchResults = new EventEmitter<CE[]>()
  @Output() setQuery = new EventEmitter<any>();
  @Output() totalItems = new EventEmitter<number>();
  @Input() getQuery: any;
  searchInputTs: string;

  searchType: string = 'ces';
  searchTerm$ = new Subject<string>();

  query: any = {
    pageSize: null,
    page: null,
    isSortAscending: false,
    sortBy: null,
    searchType: 'ces'
  }

  constructor(private searchService: SearchService,
              private ceService: CeService) {

    this.searchService.search(this.searchTerm$, this.query)
      .subscribe(results => {
        if (!(results instanceof Array)) {
          this.totalItems.emit(results.totalItems)
          this.ceService.changeCes(results.totalItems);
          this.searchResults.emit(results.items);
          if (this.query.pageSize) {
            this.setQuery.emit(this.query);
          }
        }
      });
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {

    this.query = {
      pageSize: this.getQuery.pageSize,
      page: this.getQuery.page,
      isSortAscending: this.getQuery.isSortAscending,
      sortBy: this.getQuery.sortBy,
      searchType: this.getQuery.searchType
    };
  }

  clearSearch(search) {

      const clearArr = {
        offset: 0,
        pageNumber: 1,
        search: '',
        setPageOffset: 0,
        setPagePageSize: 10
      };
       this.clearSearchArr.emit(clearArr);
  }

  _setSearch(search) {
    if (search.length >= 3) {
      console.log('search', search)
       this.setSearch.emit(search);
    }
  }
}
