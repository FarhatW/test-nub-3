import {Component, EventEmitter, OnChanges, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import {Good} from '../../../@core/data/models/products/Good';
import {Page} from '../../../@core/data/models/shared/page';
import {GoodService} from '../../../@core/data/services/good.service';
import {SearchService} from '../../../@core/data/services/search.service';
import {Subject} from 'rxjs/Subject';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {NotificationService} from '../../../@core/data/services/notification.service';
import {ToasterService} from 'angular2-toaster';
import {ProductListProductComponent} from './product-list-product/product-list-product.component';
import {Subscription} from 'rxjs/Subscription';
import {Supplier} from '../../../@core/data/models/supplier/supplier';
import {SupplierService} from '../../../@core/data/services/supplier.service';
import {LetterService} from '../../../@core/data/services/letter.service';
import {GoodDepartmentService} from '../../../@core/data/services/gooddepartment.service';
import {OriginService} from '../../../@core/data/services/origin.service';
import {ProductTypeService} from '../../../@core/data/services/productType.service';
import {LetterEnum} from '../../../@core/data/models/Enums/letter.enum';
import {KeyValuePairEnum} from '../../../@core/data/models/Enums/keyValuePair.enum';
import {animate, animateChild, group, query, state, style, transition, trigger} from '@angular/animations';
import {Observable} from "rxjs/Observable";

@Component({
  selector: 'ngx-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations: [
    trigger('animRoutes', [
      transition('* => *', [
        query(':enter',
          [
            style({opacity: 0})
          ],
          {optional: true}
        ),
        query(':leave',
          [
            style({opacity: 1}),
            animate('0.2s', style({opacity: 0}))
          ],
          {optional: true}
        ),
        query(':enter',
          [
            style({opacity: 0}),
            animate('0.2s', style({opacity: 1}))
          ],
          {optional: true}
        )
      ])])]
})

export class ProductListComponent implements OnInit, OnDestroy {

  @ViewChild('productTable') productTable: DatatableComponent;
  @ViewChild('productTable') productFormComponent: ProductListProductComponent;

  columns: any[] = [];
  letters: LetterEnum[];
  toyDepartments: KeyValuePairEnum[];
  origins: KeyValuePairEnum[];
  toyTypes: KeyValuePairEnum[];

  visibility: string = 'hidden';
  options = [10, 20, 50];
  optionSelected: any = 10;
  searchType: string = 'goods';
  suppliers: Supplier[];

  selectedRow = [];
  searchTerm$ = new Subject<string>();
  selected;
  isSortAscending: boolean = true;
  sortBy: string = 'refPintel';

  term: string = '';
  page = new Page();
  rows: Good[] = [];
  products: Good[] = [];
  query: any = {
    pageSize: this.page.size,
    page: this.page.pageNumber,
    isSortAscending: this.isSortAscending,
    sortBy: this.sortBy,
    searchType: this.searchType
  };
  isActiveChild: boolean;

  productsSubscription: Subscription;
  timerSubscription: Subscription;
  goodSub: Subscription;
  routerSub: Subscription;

  constructor(private goodService: GoodService,
              private searchService: SearchService,
              private notificationService: NotificationService,
              private toasterService: ToasterService,
              private supplierService: SupplierService,
              private letterService: LetterService,
              private goodDepartmentService: GoodDepartmentService,
              private originService: OriginService,
              private productTypeService: ProductTypeService,
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

    this.supplierService.getAll(this.query)
      .subscribe(suppliers => {
        this.suppliers = suppliers.items;
        this.supplierService.changeSuppliers(this.suppliers);
      });

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

  private refreshData(withTimer: boolean): void {
    this.productsSubscription = this.goodService.getAll(this.query).subscribe(results => {
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


    this.letterService.getAll()
      .subscribe(letters => {
        this.letters = letters;
        console.log('thislettersabove', this.letters);
        this.letterService.changeLetters(this.letters);
      });
    this.goodDepartmentService.getAll()
      .subscribe(departments => {
        this.toyDepartments = departments;
        this.goodDepartmentService.changeDepartments(this.toyDepartments);
      });
    this.originService.getAll()
      .subscribe(origins => {
        this.origins = origins;
        this.originService.changeOrigins(this.origins);
      });
    this.productTypeService.getAll()
      .subscribe(types => {
        this.toyTypes = types;
        this.productTypeService.changeTypes(this.toyTypes);
      });
  }

  setPage(pageInfo, searchValue) {
    this.query.page = pageInfo.offset + 1;
    this.query.pageSize = this.page.size;
    this.query.search = searchValue;
    this.goodService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = 0;
    });
  }

  getRows(queryPage) {
    this.goodService.getAll(this.query).subscribe(pagedData => {
      this.rows = pagedData.items;
      this.page.totalElements = pagedData.totalItems;
      this.page.pageNumber = queryPage.page - 1;
    });
  }

  clearSearch() {
    this.productTable.offset = 0;
    this.page.pageNumber = 0;
    this.query.search = '';
    this.setPage({offset: 0, pageSize: 10}, '');
  }

  newGoodBtn(isBatch: boolean) {
    isBatch ? this.router.navigate(
      ['/products/product-list/new-batch']) : this.router.navigate(['/products/product-list/new-product']);
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

  onDeselect(event, good: Good) {

    good.isEnabled = event.returnValue;
    this.goodService.update(good)
      .subscribe(res => {
        },
        err => {
          const title = 'Une erreur est survenue';
          this.toasterService.popAsync(this.notificationService.showErrorToast(title, err.body));
        },
        () => {
          const title = 'Modification effectuée';
          const body = 'Le produit ' + good.refPintel + ' a bien été ' + (event.returnValue ? 'activé' : 'désactivé');
          this.toasterService.popAsync(this.notificationService.showSuccessToast(title, body));
        },
      );
  }

  onSelect(event) {
    this.router.navigate(['/products/product-list/product/' + event.selected[0].id]);
  }

  openExcelForm(isBatch: boolean) {

    isBatch ? this.router.navigate(
      ['/products/product-list/excel-import/batches']) : this.router.navigate(
      ['/products/product-list/excel-import/products'])
  }

  public ngOnDestroy(): void {
    if (this.productsSubscription) {
      this.productsSubscription.unsubscribe();
    }
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
    this.routerSub.unsubscribe();
  }

  returnSupplierName(supplierId: number): string {
    return this.suppliers.find(x => x.id === supplierId).name;
  }

  getPage(outlet) {
    return outlet.activatedRouteData['state'] || 'product';
  }
}
