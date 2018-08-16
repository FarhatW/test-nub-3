import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CesListSearchComponent} from '../../ces/ces-list/ces-list-search/ces-list-search.component';
import {UserProfileSave} from '../../../@core/data/models/user/userSave/UserProfileSave';
import {Page} from '../../../@core/data/models/shared/page';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import {UserProfile} from '../../../@core/data/models/user/userProfile';
import {NotificationService} from '../../../@core/data/services/notification.service';
import {UsersService} from '../../../@core/data/services/users.service';
import {Router} from '@angular/router';
import {ToasterService} from 'angular2-toaster';
import {Subscription} from "rxjs/Subscription";
import {SharedService} from "../shared.service";
import {SearchService} from "../../../@core/data/services/search.service";

@Component({
  selector: 'ngx-shared-users-list',
  templateUrl: './shared-users-list.component.html',
  styleUrls: ['./shared-users-list.component.scss']
})
export class SharedUsersListComponent implements OnInit {

  @ViewChild('userTable') userTable: DatatableComponent;
  @ViewChild('searchComp') searchComp: CesListSearchComponent;
  @Input() usersInput: UserProfile[];

  perPageSub: Subscription;
  searchSub: Subscription;
  isSortAscending: boolean = true;
  sortBy: string = 'id';
  rows: UserProfile[] = [];
  userProfile: UserProfileSave;
  page = new Page();
  search: string;
  usersList: UserProfile[] = [];
  selectedRow = [];
  selected;

  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
    search: this.search
  };
  pageSize: number;

  constructor(private userService: UsersService,
              private sharedService: SharedService,
              private searchService: SearchService,
              private router: Router,
              private notificationService: NotificationService,
              private toasterService: ToasterService) {

   this.searchSub =  this.searchService.currentSearchTerm.subscribe(x => {
     console.log(x);
     this.onSearchTerm(x);
   })
    this.perPageSub = this.sharedService.currentPerPage.subscribe(x => {
      console.log(x);
      this.onPerPageChanged(x);
      this.pageSize = x;
    });

  }

  singleSelectCheck(row: any) {
    this.userProfile = row;
    return this.selected.indexOf(row) === -1;
  }

  onDeselect(event, userProfile: UserProfileSave) {
    userProfile.isEnabled = event.returnValue;
    let admin;
    const isAdmin = userProfile.ceId === undefined;

    if (isAdmin) {
      admin =  new UserProfileSave(
        userProfile.id,
        userProfile.firstName,
        userProfile.lastName,
        userProfile.isEnabled,
        userProfile.email,
        userProfile.phone,
        userProfile.createdBy,
        userProfile.updatedBy,
        userProfile.address,
        null
      )

      userProfile = admin;
    }


    this.userService.update(userProfile, isAdmin)
      .subscribe(res => {
        },
        err => {
          const title = 'Une erreur est survenue';
          this.toasterService.popAsync(this.notificationService.showErrorToast(title, err.body));
        },
        () => {
          const title = 'Modification effectuée';
          const body = 'L\'utilisateur ' + userProfile.firstName + ' a bien été ' +
            (event.returnValue ? 'activé' : 'désactivé');
          this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
        },
      );
  }

  onSelect(event) {
  }

  ngOnInit() {
    this.setPage({offset: 0, pageSize: this.pageSize}, '');
    this.userTable.messages.emptyMessage = 'Aucun utilisateurs';
    this.userTable.messages.selectedMessage = 'utilisateur sélectionné';
    this.userTable.messages.totalMessage = 'utilisateurs au total';
  }

  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    this.query.search = searchValue;

    this.userService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.userTable.messages.totalMessage = (pagedData.totalItems > 1 ? 'Users' : 'User') + ' au total' ;
      this.page.pageNumber = 0;
    });
  }

  onSort(event) {
    const sort = event.sorts[0];
    this.query.isSortAscending = sort.dir === 'asc' ? true : false;
    this.query.sortBy = sort.prop;
    this.getRows(this.query);
  }

  getRows(query) {
    this.userService.getAll(this.query).subscribe(pagedData => {
      // this.rows = [];
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = query.page - 1;
    });
  }

  _setQuery(query) {

    this.query = query;
  }
  _setSearch(search) {

    this.query.search = search.length >= 3 ? search : '';
  }
  _setSearchRows(event) {
    this.rows = event;
  }
  _clearSearch(event) {
    this.userTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: this.pageSize}, '');
  }
  onPerPageChanged(event) {
    this.page.size = event;
    this.query.pageSize = event;
    this.getRows(this.query);
  }

  onSearchTerm(event){
    this.query.search = event;
    this.getRows(this.query);
  }


}
