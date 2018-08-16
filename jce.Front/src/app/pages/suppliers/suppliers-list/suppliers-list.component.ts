import {Component, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild} from '@angular/core';
import {SearchService} from '../../../@core/data/services/search.service';
import {Subject} from 'rxjs/Subject';
import {Subscription} from 'rxjs/Subscription';
import {ActivatedRoute, NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {ToasterService} from 'angular2-toaster';
import {Observable} from 'rxjs/Rx';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import {Page} from '../../../@core/data/models/shared/page';
import {NotificationService} from '../../../@core/data/services/notification.service';
import {Good} from '../../../@core/data/models/products/Good';
import {SupplierService} from '../../../@core/data/services/supplier.service';
import {Supplier} from '../../../@core/data/models/supplier/supplier';
import {Title} from '@angular/platform-browser';
import {Navigation} from "selenium-webdriver";
import {animate, group, query, style, transition, trigger} from "@angular/animations";
import {SupplierFormService} from "./suppliers-list-form/shared/supplier-form.service";

@Component({
  selector: 'ngx-suppliers-list',
  templateUrl: './suppliers-list.component.html',
  styleUrls: ['./suppliers-list.component.scss'],
  animations: [
    trigger('routerTransition', [
      transition('* <=> *', [
        group([
          query(':enter, :leave', style({ position: 'fixed', width:'100%' })
            , { optional: true }),
          query(':enter', [
            style({ transform: 'translateX(100%)' }),
            animate('0.5s ease-in-out', style({ transform: 'translateX(0%)' }))
          ], { optional: true }),
          query(':leave', [
            style({ transform: 'translateX(0%)' }),
            animate('0.5s ease-in-out', style({ transform: 'translateX(-100%)' }))
          ], { optional: true }),
        ])
      ])
    ])
  ]
})


export class SuppliersListComponent implements OnInit, OnDestroy, OnChanges {

  @ViewChild('productTable') productTable: DatatableComponent;

  columns: any[] = [];
  options = [10, 20, 50];
  optionSelected: any = 10;
  isSupplierEdit: boolean;
  supplier2Send: Supplier;
  sendSupplierId: number = 0;
  searchType: string = 'suppliers';
  isActivatedChild: boolean = false;

  selectedRow = [];
  sendSupplier: Supplier;
  searchTerm$ = new Subject<string>();
  selected;
  isSortAscending: boolean = true;
  sortBy: string = 'refPintel';
  isVisible: boolean = false;

  term: string = '';
  page = new Page();
  rows: Supplier[] = [];
  products: Good[] = [];
  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
    searchType: this.searchType
  };
  supplierSubscription: Subscription;
  timerSubscription: Subscription;
  routerSub: Subscription;
  isActiveChild: boolean = false;
  i: number = 0;

  constructor(private supplierService: SupplierService,
              private searchService: SearchService,
              private notificationService: NotificationService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              private titleService: Title,
              private supplierFormService: SupplierFormService,
              private router: Router) {
    this.page.size = 10;
    this.query.pageSize = 10;
    this.query.page = 0;
    this.page.totalElements = 0;
    this.page.pageNumber = 0;
    this.page.totalPages = 0;
    this.route.data.subscribe(t => this.titleService.setTitle(t.title));
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

    this.searchService.search(this.searchTerm$, this.query)
      .subscribe(results => {
        if (!(results instanceof Array)) {
          this.productTable.offset = 0;
          this.rows = results.items;
          this.page.totalElements = results.totalItems;
        }
      });
  }

  getOutlet(outlet) {
    return outlet.activatedRouteData.state;
  }

  private refreshData(withTimer: boolean): void {
    this.supplierSubscription = this.supplierService.getAll(this.query).subscribe(results => {
      console.log('lele', new Date().toString() );
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
    this.timerSubscription = Observable.timer(5000).first().subscribe(() => this.refreshData(true));
  }


  ngOnInit() {
    this.setPage({offset: 0, pageSize: 10}, '');
    this.productTable.messages.emptyMessage = 'Aucun Produit';
    this.productTable.messages.selectedMessage = 'article sélectionné';
    this.productTable.messages.totalMessage = 'articles au total';
    // let activeChild = this.route.Children.length;
    // if (activeChild !== 0) {
    //   this.isActivatedChild = true;
    // }
  }

  ngOnChanges(changes: SimpleChanges) {
    console.log('changes', changes);
  }

  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    this.query.search = searchValue;
    this.supplierService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = 0;
    });
  }

  getRows(query) {
    this.supplierService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = query.page - 1;
    });
  }

  clearSearch() {
    this.productTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: 10}, '');
  }

  singleSelectCheck(row: any) {
    this.sendSupplier = row;
    return this.selected.indexOf(row) === -1;
  }

  afterSupplierSave(event) {
    this.selectedRow = [];
    this.selectedRow.push(event);
    this.query.page = 1;
    this.query.pageSize = this.page.size;
    this.query.search = '';
    this.getRows(this.query);
    this.productTable.offset = 0;
    this.supplier2Send = this.selectedRow[0];
    this.isSupplierEdit = true;
  }

  newSupplierBtn() {
    this.selectedRow = [];
    this.isSupplierEdit = false;
    this.isVisible = true;
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

  onDeselect(event, supplier: Supplier) {
    supplier.isEnabled = event.returnValue;
    this.supplierService.update(supplier)
      .subscribe(res => {
        },
        err => {
          const title = 'Une erreur est survenue';
          this.toasterService.popAsync(this.notificationService.showErrorToast(title, err.body));
        },
        () => {
          const title = 'Modification effectuée';
          const body = 'Le produit ' + supplier.name + ' a bien été ' + (event.returnValue ? 'activé' : 'désactivé');
          this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
        },
      );
  }

  onSelect(event) {
    this.router.navigate(['/suppliers/list/' + event.selected[0].id]);
  }

  public ngOnDestroy(): void {
    if (this.supplierSubscription) {
      this.supplierSubscription.unsubscribe();
    }
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }

    this.routerSub.unsubscribe();
  }

}
