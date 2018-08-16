import {Component, OnChanges, OnInit, SimpleChanges, ViewChild} from '@angular/core';
import {CeService} from '../../../@core/data/services/ce.service';
import {CE} from '../../../@core/data/models/ce/ce';
import {Router} from '@angular/router';
import {Page} from '../../../@core/data/models/shared/page';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import {CesListSearchComponent} from "./ces-list-search/ces-list-search.component";
import {UsersService} from "../../../@core/data/services/users.service";
import {UserProfile} from "../../../@core/data/models/user/UserProfile";

type CES = CE[][];

@Component({
  selector: 'ngx-ces-list',
  templateUrl: './ces-list.component.html',
  styleUrls: ['./ces-list.component.scss'],
})
export class CesListComponent implements OnInit {


  @ViewChild('productTable') productTable: DatatableComponent;
  @ViewChild('searchComp') searchComp: CesListSearchComponent;

  idAdmin: number = null;
  isSortAscending: boolean = true;
  sortBy: string = 'id';
  rows: CE[] = [];
  page = new Page();
  search: string;
  users: UserProfile[] = [];
  private readonly UserType: number = 1;



  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
    idAdmin: this.idAdmin,
    search: this.search,
    userType: this.UserType
  };

  constructor(private ceService: CeService,
              private router: Router,
              private userService: UsersService) {
  }

  ngOnInit() {
    this.setPage({offset: 0, pageSize: 10}, '');
    this.productTable.messages.emptyMessage = 'Aucun CE';
    this.productTable.messages.selectedMessage = 'ce sélectionné';

    this.ceService.currentCes.subscribe(ces => this.rows = ces);

    this.userService.getAll(this.query).subscribe(
      users => {
        this.users = users.items;
        console.log('users', users);
      },
      err => {
        console.log(err);
      }
    )
  }


  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = pageInfo.pageSize;
    this.query.search = searchValue;
    this.ceService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.productTable.messages.totalMessage = (pagedData.totalItems > 1 ? 'CEs' : 'CE') + ' au total' ;
      this.page.pageNumber = 0;
    });
  }

  onSort(event) {
    const sort = event.sorts[0];
    this.query.isSortAscending = sort.dir === 'asc' ? true : false;
    console.log('sort', sort);
    this.query.sortBy = sort.prop;
    this.getRows(this.query);
  }

  onPageChange(page) {
    this.query.page = page;
  }

  getRows(query) {
    this.ceService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      console.log('pageddata', pagedData);
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = query.page - 1;
    });
  }

  _setSearch(search) {

    this.query.search = search.length >= 3 ? search : '';
  }

  _setQuery(query) {

    this.query = query;
  }

  _setSearchRows(event) {
    console.log('setsearchrow', event);
    this.rows = event;
  }

  _clearSearch(event) {
    this.productTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: 10}, '');
  }

  onPerPageChanged(event) {
    this.page.size = event;
    this.query.pageSize = event;
    this.getRows(this.query);
  }

  onPerUserChanged(event) {
    this.idAdmin = event;
    this.query.search = '';
    this.query.idAdmin = event;
    this.searchComp.searchInputTs = '';
    this.setPage({offset: 0, pageSize: this.query.pageSize}, '' );
  }

  _setUserForList(id: number): string {
    return id ? this.userService.setNameFirstName(this.users.find(x => x.id === id)) :
      '';
  }
}
