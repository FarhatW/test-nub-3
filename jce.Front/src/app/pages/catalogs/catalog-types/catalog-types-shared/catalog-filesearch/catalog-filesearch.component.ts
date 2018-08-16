import {Component, Input, OnInit, Output, EventEmitter, OnChanges, SimpleChanges} from '@angular/core';
import {Catalog} from '../../../../../@core/data/models/catalog/catalog';
import {GoodService} from '../../../../../@core/data/services/good.service';
import {BodyOutputType, Toast, ToasterService} from 'angular2-toaster';
import {ActivatedRoute, Router} from '@angular/router';
import {CatalogService} from '../../../../../@core/data/services/catalog.service';
import {DateConverterService} from '../../../../../@core/utils/dateConverter.service';
import {SearchService} from '../../../../../@core/data/services/search.service';
import {Subject} from 'rxjs/Subject';
import {CatalogGood} from '../../../../../@core/data/models/products/catalogGood';
import {NotificationService} from "../../../../../@core/data/services/notification.service";

@Component({
  selector: 'ngx-catalog-filesearch',
  templateUrl: './catalog-filesearch.component.html',
  styleUrls: ['./catalog-filesearch.component.scss']
})
export class CatalogFilesearchComponent implements OnInit, OnChanges {

  @Input() catalog: Catalog;
  @Output() good = new EventEmitter<CatalogGood>();
  @Input() isMiniCatalog: Boolean;

  products: Object;
  searchTerm$ = new Subject<string>();
  catalogGood: any;
  productPersoArr: CatalogGood[] = [];

  query: any = {
    pageSize: 10,
    page: 1,
  }

  searchQuery: any = {
    searchType: 'goods',
  }

  constructor(private catalogService: CatalogService,
              private goodService: GoodService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              private dateConverterService: DateConverterService,
              private notificationService: NotificationService,
              private searchService: SearchService) {

    this.searchService.search(this.searchTerm$, this.searchQuery)
      .subscribe(results => {
        this.products = results;
      })
  }

  ngOnInit() {
    console.log('isMiniCatalog', this.isMiniCatalog)
  }

  ngOnChanges(changes: SimpleChanges) {

  }

  sendGood(good) {
    console.log('allo from Children', good)
    this.good.emit(good);
    this.productPersoArr = [];
  }

  removeFromProductArray(product) {
    const index = this.productPersoArr.indexOf(product);
    this.productPersoArr.splice(index, 1);
  }

  addToProductPersoArray(product) {
    console.log(this.catalog.catalogGoods.find(x => x.goodId === product.id));
    console.log(this.catalog.catalogGoods);
    if (!this.catalog.catalogGoods.find(x => x.goodId === product.id)) {
      if (this.productPersoArr.length <= 0) {
        this.productPersoArr.push(product);
      } else {
        const title = 'Nombre de produit max';
        const body = 'Veuillez sauvegarder le produits avant de continuer.';
        this.toasterService
          .popAsync(this.notificationService.showWarningToast(title, body));
      }
    } else {
      const title = 'Produit déjà présent dans votre catalogue.';
      const body = 'Ce produit est déjà associé à votre catalogue.';
      this.toasterService
        .popAsync(this.notificationService.showWarningToast(title, body));
    }
  }
}
