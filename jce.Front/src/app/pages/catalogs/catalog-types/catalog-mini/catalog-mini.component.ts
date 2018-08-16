import {Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {CatalogService} from '../../../../@core/data/services/catalog.service';
import {Catalog} from '../../../../@core/data/models/catalog/catalog';
import {CatalogSave} from '../../../../@core/data/models/catalog/catalogSave';
import {CatalogProduct} from '../../../../@core/data/models/products/catalogProduct';
import {CatalogProductSave} from '../../../../@core/data/models/products/catalogProductSave';
import {Product} from '../../../../@core/data/models/products/product';
import {LocalDataSource, ServerDataSource} from 'ng2-smart-table';
import * as XLSX from 'xlsx';
import {DateInputComponent} from '../../../../@core/utils/dateInput.component';
import {ExlProduct} from '../../../../@core/data/models/products/exlProduct';

import {ToasterService, ToasterConfig, Toast, BodyOutputType} from 'angular2-toaster';
import {CatalogGoodSave} from '../../../../@core/data/models/products/catalogGoodSave';
import {DateConverterService} from '../../../../@core/utils/dateConverter.service';
import {GoodService} from '../../../../@core/data/services/good.service';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {Good} from '../../../../@core/data/models/products/Good';

@Component({
  selector: 'ngx-catalog-mini',
  templateUrl: './catalog-mini.component.html',
  styleUrls: ['./catalog-mini.component.scss'],
})
export class CatalogMiniComponent implements OnInit {

  constructor(private catalogService: CatalogService,
              private goodService: GoodService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              private dateConverterService: DateConverterService,
              private notificationService: NotificationService,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
  }

  @ViewChild('fileInput') fileInput: any;
  dateArray: any[] = [];

  isMiniCatalog: Boolean = true;

  id: number;
  pageSize: number = 10;
  totalProdutcs: number;
  pintelRefArray: string[] = [];
  PAGE_SIZE = this.pintelRefArray.length;
  pageCount: number;
  query: any = {
    refPintelArray: this.pintelRefArray,
    pageSize: this.PAGE_SIZE,
  };

  catalogQuery: any = {
    ceId: true,
  };
  catalogProdutcs: CatalogGoodSave[] = [];
  addedProduct: CatalogGoodSave;
  catalogproddd: any[] = [];
  catalogSave: CatalogSave = new CatalogSave();
  catalog: Catalog = {
    id: 0,
    ceId: 0,
    indexId: '',
    catalogType: 0,
    isActif: false,
    expirationDate: new Date,
    displayPrice: false,
    productChoiceQuantity: 0,
    catalogChoiceTypeId: 0,
    booksQuantity: 0,
    toysQuantity: 0,
    subscriptionQuantity: 0,
    createdBy: '',
    updatedBy: '',
    catalogGoods: this.catalogproddd,
  };

  settings = {

    noDataMessage: 'Ce Catalogue ne contient aucun produit.',

    actions: {
      add: false,
    },

    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
      confirmSave: true,
    },
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    columns: {
      refPintel: {
        title: 'Ref Pintel',
        type: 'string',
        sort: true,
        editable: false,
      },
      title: {
        title: 'Nom du Produit',
        type: 'string',
        editable: false,
        valuePrepareFunction: (value) => {
          return (value.length > 10 ? value.substring(0, 10) + '...' : value);
        },

      },
      details: {
        title: 'Détails',
        editable: false,
        valuePrepareFunction: (value) => {
          return (value.length > 10 ? value.substring(0, 10) + '...' : value);
        },
      },
      price: {
        title: 'Prix',
        type: 'number',
        editable: false,
        sort: true,
      },
      dateMin: {
        title: 'Date Min',
        type: 'number',
        sort: true,
        editor: {
          type: 'list',
          config: {
            list: this.dateConverterService.getLastDate(),
          },
        },
      },
      dateMax: {
        title: 'Date Max',
        type: 'number',
        sort: true,
        editor: {
          type: 'list',
          config: {
            list: this.dateConverterService.getLastDate(),
          },
        },
      },
      clientProductAlias: {
        title: 'Alias',
        type: 'string',
      },
      employeeParticipationMessage: {
        title: 'Participation',
        type: 'string'
      }
    },
  };

  source: LocalDataSource = new LocalDataSource();

  ngOnInit() {
    this.pageSize = 10;
    this.catalogService.getByCEId(this.id, this.catalogQuery)
      .subscribe(catalog => {
          this.refreshCatalog(catalog);
          console.log('catalog', catalog);
          this.dateArray = this.dateConverterService.getLastDate();
        },
        err => {
          console.log('err', err);
        },
      );
  }

  dateValidity(dateMin, dateMax): Boolean {
    if (dateMin <= dateMax) {
      return true;
    }
    return false;
  }

  refreshCatalog(catalog) {
    this.catalog = catalog;
    this.catalog.catalogGoods.forEach(item => {
      item.dateMax = this.dateConverterService.dateToYear(item.dateMax);
      item.dateMin = this.dateConverterService.dateToYear(item.dateMin);
    })
    this.setCatalog(this.catalog);
    this.refreshTable();
  }

  setCatalog(catalog: Catalog) {
    this.catalogSave.id = catalog.id;
    this.catalogSave.ceId = catalog.ceId;
    this.catalogSave.createdBy = catalog.createdBy;
    this.catalogSave.updatedBy = catalog.updatedBy;
    this.catalogSave.subscriptionQuantity = catalog.subscriptionQuantity;
    this.catalogSave.toysQuantity = catalog.toysQuantity;
    this.catalogSave.booksQuantity = catalog.booksQuantity;
    this.catalogSave.catalogChoiceTypeId = catalog.catalogChoiceTypeId;
    this.catalogSave.displayPrice = catalog.displayPrice;
    this.catalogSave.productChoiceQuantity = catalog.productChoiceQuantity;
    this.catalogSave.expirationDate = catalog.expirationDate;
    this.catalogSave.catalogType = catalog.catalogType;
    this.catalogSave.isActif = catalog.isActif;
    this.catalogSave.indexId = catalog.indexId;
    this.catalogSave.catalogGoods = [];
    catalog.catalogGoods.forEach(item => {
      const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
      catalogGoodSave.isAddedManually = true;
      catalogGoodSave.dateMax = item.dateMax
      catalogGoodSave.dateMin = item.dateMin;
      catalogGoodSave.goodId = item.goodId;
      catalogGoodSave.catalogId = item.catalogId;
      catalogGoodSave.isAddedManually = true;
      catalogGoodSave.clientProductAlias = item.clientProductAlias;
      catalogGoodSave.employeeParticipationMessage = item.employeeParticipationMessage;
      this.catalogSave.catalogGoods.push(catalogGoodSave);
    })

    console.log(this.catalogSave, 'fffffffffffffffffff');
  }

  setPaging(pageSize) {
    console.log(pageSize.target.value);
    this.pageSize = pageSize.target.value;
    this.source.setPaging(1, this.pageSize, true);
    this.pageCount = Math.ceil(this.totalProdutcs / this.pageSize);
  }

  refreshTable() {
    this.source.load(this.catalog.catalogGoods);
    this.source.setPaging(1, 10, true);
    this.totalProdutcs = this.catalog.catalogGoods.length;
    this.pageCount = Math.ceil(this.totalProdutcs / this.pageSize);
  }

  onFileChange(event: any) {

    const target: DataTransfer = <DataTransfer>(event.target);
    if (target.files.length !== 1) throw new Error('Cannot use multiple files');
    if (target.files[0].type !== 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet') {
      const title: string = 'Mauvais Format';
      const body: string = 'Le fichier doit être un fichier Excel.';
      this.toasterService
        .popAsync(this.notificationService.showErrorToast(title, body));
      throw  new Error('Mauvais Format');
    }

    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      /* read workbook */
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, {type: 'binary'});

      /* grab first sheet */
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];

      /* save data */
      this.catalogProdutcs = <CatalogGoodSave[]>(XLSX.utils.sheet_to_json(ws, {header: 1}));
      this.catalogProdutcs.splice(0, 1);
      this.productMap(this.catalogProdutcs);
    };
    reader.readAsBinaryString(target.files[0]);
  }

  productMap(catalogGoods: any[]) {

    let title: string;
    let body: string;
    const notAdded: string[] = [];
    const added: string[] = [];

    for (let i = 0; i < catalogGoods.length; i++) {
      if (catalogGoods[i][0] != null) {
        this.pintelRefArray.push(catalogGoods[i][0]);
      }
    }
    this.query.pageSize = this.pintelRefArray.length;
    this.goodService.getAll(this.query)
      .subscribe(products => {
          const produtctt: Good[] = products.items;
          for (let i = 0; i < produtctt.length; i++) {
            if (!this.catalogSave.catalogGoods.find(pr => pr.goodId === produtctt[i].id)) {
              const addedProduct = new CatalogGoodSave();
              addedProduct.goodId = produtctt[i].id;
              addedProduct.catalogId = this.catalog.id;
              addedProduct.dateMin = catalogGoods.find(x => x[0] === produtctt[i].refPintel)[1];
              addedProduct.dateMax = catalogGoods.find(x => x[0] === produtctt[i].refPintel)[2];
              addedProduct.clientProductAlias = catalogGoods.find(x => x[0] === produtctt[i].refPintel)[3];
              this.addedProduct.isAddedManually = true;
              this.catalogSave.catalogGoods.push(addedProduct);
              added.push(produtctt[i].refPintel, ', ');
            } else {
              notAdded.push(' ' + produtctt[i].refPintel);
            }
          }
        },
        err => {
          console.log(err);

        },
        () => {
          const $result = this.catalogService.update(this.catalogSave);
          $result.subscribe(
            catalog => {
              this.refreshCatalog(catalog);
            },
            err => console.log(err),
            () => {
              if (added.length > 0) {
                title = 'Produits ajoutés au catalogue';
                body = added.toString();
                this.toasterService
                  .popAsync(this.notificationService.showWarningToast(title, body));
              }
              if (notAdded.length > 0) {
                title = 'Produits non ajoutés au catalogue';
                body = notAdded.toString();
                this.toasterService
                  .popAsync(this.notificationService.showWarningToast(title, body));
              }
              this.refreshTable();
            },
          );
        },
      );
  }

  saveCatalog(catalogSave: CatalogSave, prodTitle, prodBody) {
    const $result = this.catalogService.update(this.catalogSave);
    $result.subscribe(catalog => {
        this.refreshCatalog(catalog);
      },
      err => {
        const title = err.status + '  ' + err.statusText;
        const body = err._body;
        this.toasterService
          .popAsync(this.notificationService.showWarningToast(title, body));
      },
      () => {
        this.toasterService
          .popAsync(this.notificationService.showSuccessToast(prodTitle, prodBody));
        this.refreshTable();
      });
  }

  onEditConfirm(event): void {
    if (this.dateValidity(event.newData.dateMin, event.newData.dateMax)) {
      const editedProduct: CatalogGoodSave = new CatalogGoodSave();
      editedProduct.goodId = event.newData.id;
      editedProduct.clientProductAlias = event.newData.clientProductAlias;
      editedProduct.catalogId = this.catalogSave.id;
      editedProduct.dateMin = event.newData.dateMin;
      editedProduct.dateMax = event.newData.dateMax;
      editedProduct.employeeParticipationMessage = event.newData.employeeParticipationMessage;
      editedProduct.isAddedManually = true;
      const index: number = this.catalogSave.catalogGoods.indexOf(
        this.catalogSave.catalogGoods.find(x => x.goodId === event.newData.id));
      console.log('index', index);
      if (index !== -1) {
        this.catalogSave.catalogGoods.splice(index, 1, editedProduct);
      }
      console.log('index', index);

      const title = 'Produit édité !';
      const body = 'Produit ' + event.newData.refPintel + 'a bien été modifié.';
      this.saveCatalog(this.catalogSave, title, body);

    } else {
      const title = 'Erreur sur les dates';
      const body = 'Vérifiez les dates Min et Max sur le produit modifié.';
      this.toasterService
        .popAsync(this.notificationService.showErrorToast(title, body));
    }
  }

  receiveCatalogGood(product) {

    const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
    catalogGoodSave.catalogId = this.catalogSave.id;
    catalogGoodSave.goodId = product.id;
    catalogGoodSave.dateMin = product.dateMin;
    catalogGoodSave.dateMax = product.dateMax;
    catalogGoodSave.clientProductAlias = product.clientProductAlias;
    catalogGoodSave.employeeParticipationMessage = product.employeeParticipationMessage;
    catalogGoodSave.isAddedManually = true;
    this.catalogSave.catalogGoods.push(catalogGoodSave);
    const title = 'Produit Ajouté !';
    const body = 'Produit ' + product.refPintel + ' ajouté au catalog.';

    this.saveCatalog(this.catalogSave, title, body);
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Êtes vous sûres de vouloir supprimer ce produit de ce catalogue ?')) {
      const deleteProd: CatalogGoodSave = this.catalogSave.catalogGoods.find(x => x.goodId === event.data.id);
      const index: number = this.catalogSave.catalogGoods.indexOf(deleteProd);
      if (index !== -1) {
        this.catalogSave.catalogGoods.splice(index, 1);
        const $result = this.catalogService.update(this.catalogSave);
        $result.subscribe(catalog => {
            this.refreshCatalog(catalog);
          },
          err => {
            const title = 'Une erreur est survenue';
            const body = err.body;
            this.toasterService
              .popAsync(this.notificationService.showErrorToast(title, body));
          },
          () => {
            const title = 'Succès';
            const body = 'Produit ' + event.data.refPintel + ' supprimé du catalogue';
            this.toasterService
              .popAsync(this.notificationService.showSuccessToast(title, body));
          });
        this.refreshTable();
      }
    } else {
      (console.log('lelele'));
      event.confirm.reject();
    }
  }
}
