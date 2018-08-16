import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CesListSearchComponent} from "../../ces/ces-list/ces-list-search/ces-list-search.component";
import {ChildrenSave} from "../../../@core/data/models/user/child/ChildrenSave";
import {ChildrenService} from "../../../@core/data/services/children.service";
import {Page} from "../../../@core/data/models/shared/page";
import {NotificationService} from "../../../@core/data/services/notification.service";
import {Children} from "../../../@core/data/models/user/child/Children";
import {DatatableComponent} from "@swimlane/ngx-datatable";
import {Router} from "@angular/router";
import {ToasterService} from "angular2-toaster";

@Component({
  selector: 'ngx-shared-children-list',
  templateUrl: './shared-children-list.component.html',
  styleUrls: ['./shared-children-list.component.scss']
})
export class SharedChildrenListComponent implements OnInit {

  @ViewChild('childrenTable') userTable: DatatableComponent;
  @ViewChild('searchComp') searchComp: CesListSearchComponent;
  @Input() childrenInput: Children[];

  isSortAscending: boolean = true;
  sortBy: string = 'id';
  rows: Children[] = [];
  childrenSave: ChildrenSave;
  page = new Page();
  search: string;
  children: Children[] = [];
  selectedRow = [];
  selected;

  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
    search: this.search
  };
  constructor(
    private childrenService: ChildrenService,
    private router: Router,
    private notificationService: NotificationService,
    private toasterService: ToasterService) { }



  singleSelectCheck(row: any) {
    this.children = row;
    return this.selected.indexOf(row) === -1;
  }

  onDeselect(event, child: ChildrenSave) {
    child.isEnabled = event.returnValue;


    this.childrenService.update(child)
      .subscribe(res => {
        },
        err => {
          const title = 'Une erreur est survenue';
          this.toasterService.popAsync(this.notificationService.showErrorToast(title, err.body));
        },
        () => {
          const title = 'Modification effectuée';
          const body = 'Le produit ' + child.firstName + ' a bien été ' + (event.returnValue ? 'activé' : 'désactivé');
          this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
        },
      );
  }
  onSelect(event) {
    this.router.navigate(['/suppliers/list/' + event.selected[0].id]);
  }

  ngOnInit() {
    this.setPage({offset: 0, pageSize: 10}, '');
    this.userTable.messages.emptyMessage = 'Aucun Enfants';
    this.userTable.messages.selectedMessage = 'Enfants sélectionné';
    this.userTable.messages.totalMessage = 'Enfants au total';

  }

  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    this.query.search = searchValue;
    if (this.childrenInput) {
      this.rows = this.childrenInput;
      this.page.totalElements = this.childrenInput.length;
      this.userTable.messages.totalMessage = (this.childrenInput.length > 1 ? 'Enfants' : 'Enfant') + ' au total' ;
      this.page.pageNumber = 0;
    } else {
      this.childrenService.getAll(this.query).subscribe(pagedData => {
        this.rows = pagedData.items;
        this.page.totalElements = pagedData.totalItems;
        this.userTable.messages.totalMessage = (pagedData.totalItems > 1 ? 'Enfants' : 'Enfant') + ' au total' ;
        this.page.pageNumber = 0;
      });
    }
  }
  onSort(event) {
    const sort = event.sorts[0];
    this.query.isSortAscending = sort.dir === 'asc' ? true : false;
    this.query.sortBy = sort.prop;
    this.getRows(this.query);
  }

  getRows(query) {
    this.childrenService.getAll(this.query).subscribe(pagedData => {
      // this.rows = [];
      this.rows = pagedData.items;
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
    this.userTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: 10}, '');
  }
  onPerPageChanged(event) {
    this.page.size = event;
    this.query.pageSize = event;
    this.getRows(this.query);
  }


}
