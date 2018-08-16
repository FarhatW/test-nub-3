import {Component, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild} from '@angular/core';
import {SearchService} from '../../../@core/data/services/search.service';
import {Subject} from 'rxjs/Subject';
import {Subscription} from "rxjs/Subscription";
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {ToasterService} from "angular2-toaster";
import {Observable} from "rxjs/Rx";
import {DatatableComponent} from "@swimlane/ngx-datatable";
import {Page} from "../../../@core/data/models/shared/page";
import {NotificationService} from "../../../@core/data/services/notification.service";
import {Good} from "../../../@core/data/models/products/Good";
import {PintelSheetService} from "../../../@core/data/services/pintelSheet.service";
import {FormPintelSheet, PintelSheet} from "../../../@core/data/models/pintelSheet";
import {PintelSheetSave} from "../../../@core/data/models/pintelSheet/pintelSheetSave";

@Component({
  selector: 'ngx-pintelsheets-list',
  templateUrl: './pintelsheets-list.component.html',
  styleUrls: ['./pintelsheets-list.component.scss']
})
export class PintelsheetsListComponent implements OnInit, OnDestroy, OnChanges {

  @ViewChild('productTable') productTable: DatatableComponent;

  columns: any[] = [];

  options = [10, 20, 50];
  optionSelected: any = 10;
  isPintelSheetEdit: boolean;
  pintelsheet2Send: PintelSheet;
  sendpintelsheetId: number = 0;
  searchType: string = 'suppliers';

  selectedRow = [];
  sendPintelsheet: PintelSheet;
  searchTerm$ = new Subject<string>();
  selected;
  isSortAscending: boolean = true;
  sortBy: string = 'refPintel';
  isVisible: boolean = false;

  term: string = '';
  page = new Page();
  rows: PintelSheet[] = [];
  products: Good[] = [];
  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
  };

  routerSub: Subscription;

  pintelsheetSubscription: Subscription;
  timerSubscription: Subscription;
  isActiveChild: boolean;

  constructor(private pintelsheetsService: PintelSheetService,
              private searchService: SearchService,
              private notificationService: NotificationService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              private router: Router) {
    this.page.size = 10;
    this.query.pageSize = 10;
    this.query.page = 0;
    this.page.totalElements = 0;
    this.page.pageNumber = 0;
    this.page.totalPages = 0;

    this.routerSub = router.events.filter(x => x instanceof NavigationEnd).subscribe(() => {
        this.refreshData(false);
        const activeChild = this.route.children.length;
        this.isActiveChild = activeChild > 0 ? true : false;
      },
      err => {
        console.log('err', err)
      },
      () => {
      }
    )
    this.refreshData(true);
  }

  private refreshData(withTimer: boolean): void {
    this.pintelsheetSubscription = this.pintelsheetsService.getAll(this.query).subscribe(results => {
      this.rows = results.items;
      this.page.totalElements = results.totalItems;
      if (this.selectedRow[0]) {
        this.selected = this.selectedRow[0];
      }
      if (withTimer) {
        this.subscribeToData();
      }
    });
  }

  _setSearch(search) {
    this.query.search = search.length >= 3 ? search : '';
  }

  private subscribeToData(): void {
    // this.timerSubscription = Observable.timer(5000).first().subscribe(() => this.refreshData());
  }

  ngOnInit() {
    this.setPage({offset: 0, pageSize: 10});
    this.productTable.messages.emptyMessage = 'Aucun Produit';
    this.productTable.messages.selectedMessage = 'article sélectionné';
    this.productTable.messages.totalMessage = 'articles au total';

  }

  setPage(pageInfo) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    // this.query.search = searchValue;
    this.pintelsheetsService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = 0;
    });
  }

  getRows(query) {
    this.pintelsheetsService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = query.page - 1;
    });
  }

  clearSearch() {
    this.productTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: 10});
  }

  singleSelectCheck(row: any) {

    console.log('row', row);
    this.sendpintelsheetId = row;
    return this.selected.indexOf(row) === -1;
  }

  afterPintelSheetSave(event) {
    this.selectedRow = [];
    this.selectedRow.push(event);
    this.query.page = 1;
    this.query.pageSize = this.page.size;
    this.query.search = '';
    this.getRows(this.query);
    this.productTable.offset = 0;
    this.sendpintelsheetId = this.selectedRow[0].id;
    this.isPintelSheetEdit = true;
  }

  newPintelSheetBtn() {
    this.router.navigate(['/pintelsheets/list/new']);
  }

  onSort(event) {
    const sort = event.sorts[0];
    this.query.isSortAscending = sort.dir === 'asc' ? true : false;
    this.query.sortBy = sort.prop;
    this.getRows(this.query);
  }

  onOptionsSelected(event) {
    this.page.size = event;
    this.query.pageSize = event;
    this.getRows(this.query);
  }

  onSelect(event) {
      this.router.navigate(['/pintelsheets/list/' + event.selected[0].id]);
  }

  public ngOnDestroy(): void {
    if (this.pintelsheetSubscription) {
      this.pintelsheetSubscription.unsubscribe();
    }
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('changes', changes);
  }

}
