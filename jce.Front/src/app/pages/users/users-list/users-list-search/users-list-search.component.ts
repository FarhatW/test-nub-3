import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {CeService} from "../../../../@core/data/services/ce.service";
import {CE} from "../../../../@core/data/models/ce/ce";
import {Subject} from "rxjs/Subject";
import {SearchService} from "../../../../@core/data/services/search.service";
import {UsersService} from "../../../../@core/data/services/users.service";

@Component({
  selector: 'ngx-users-list-search',
  templateUrl: './users-list-search.component.html',
  styleUrls: ['./users-list-search.component.scss']
})
export class UsersListSearchComponent implements OnInit, OnChanges {

  @Output() clearSearchArr = new EventEmitter<any>();
  @Output() setSearch = new EventEmitter<string>();
  @Output() searchResults = new EventEmitter<CE[]>()
  @Output() setQuery = new EventEmitter<any>();
  @Output() totalItems = new EventEmitter<number>();
  @Input() getQuery: any;

  searchInputTs: string;

  searchType: string = 'usersList';
  searchTerm$ = new Subject<string>();

  query: any = {
    pageSize: null,
    page: null,
    isSortAscending: false,
    sortBy: null,
    searchType: 'usersList'
  }

  constructor(private searchService: SearchService,
              private userService: UsersService) {

    this.searchService.search(this.searchTerm$, this.query)
      .subscribe(results => {
        if (!(results instanceof Array)) {
          this.totalItems.emit(results.totalItems)
          this.userService.changeUsers(results.totalItems);
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
