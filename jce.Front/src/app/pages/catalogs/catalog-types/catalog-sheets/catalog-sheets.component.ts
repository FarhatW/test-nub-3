import {Component, OnInit} from '@angular/core';
import {AgeGroupEnum} from '../../../../@core/data/models/Enums/ageGroup.enum';
import {AgeGroupService} from '../../../../@core/data/services/ageGroup.service';
import {PintelSheet} from '../../../../@core/data/models/pintelSheet';
import {PintelSheetService} from '../../../../@core/data/services/pintelSheet.service';
import {SearchService} from '../../../../@core/data/services/search.service';
import {DatePipe} from '@angular/common';
import {DateConverterService} from '../../../../@core/utils/dateConverter.service';
import {CatalogService} from '../../../../@core/data/services/catalog.service';
import {ActivatedRoute, Router} from '@angular/router';
import {BodyOutputType, Toast, ToasterService} from 'angular2-toaster';
import {GoodService} from '../../../../@core/data/services/good.service';
import {Subject} from 'rxjs/Subject';
import {CatalogSave} from '../../../../@core/data/models/catalog/catalogSave';
import {Catalog} from '../../../../@core/data/models/catalog/catalog';
import {CatalogGoodSave} from '../../../../@core/data/models/products/catalogGoodSave';
import {NotificationService} from "../../../../@core/data/services/notification.service";

@Component({
  selector: 'ngx-catalog-sheets',
  templateUrl: './catalog-sheets.component.html',
  styleUrls: ['./catalog-sheets.component.scss']
})
export class CatalogSheetsComponent implements OnInit {

  ageGroups: AgeGroupEnum[];
  pintelSheets: PintelSheet[] = [];
  id: number;
  pageSize = 40;
  pintelSheetIdArray: number[] = [];
  pintelSheetQuery: any = {
    pageSize: this.pageSize,
    returnProducts: false,
    pintelSheetsArray: this.pintelSheetIdArray
  };
  products: Object;
  searchTerm$ = new Subject<string>();
  catalogQuery: any  = {
    ceId: true,
}
  catalogSave: CatalogSave = new CatalogSave();
  catalog: Catalog;

  constructor(private ageGroupService: AgeGroupService,
              private pintelSheetService: PintelSheetService,
              private catalogService: CatalogService,
              private goodService: GoodService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              public datepipe: DatePipe,
              private dateConverterService: DateConverterService,
              private searchService: SearchService,
              private notificationService: NotificationService,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
    this.searchService.search(this.searchTerm$, true)
      .subscribe(results => {
        this.products = results;
      })
  }

  ngOnInit() {
    this.ageGroupService.getAll()
      .subscribe(results => {
        this.ageGroups = results;
      })
    this.pintelSheetService.getAll(this.pintelSheetQuery)
      .subscribe(response => {
        this.pintelSheets = response.items;
      })
    this.catalogService.getByCEId(this.id, this.catalogQuery)
      .subscribe(catalog => {
        this.catalog = catalog;
        this.setCatalog(this.catalog);
        this.catalog.catalogGoods.forEach(item => {
          if (!this.pintelSheetIdArray.find(x => x === item.pintelSheetId)) {
            this.pintelSheetIdArray.push(item.pintelSheetId);
          }
        })
      })
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
    catalog.catalogGoods.filter(x => x.isAddedManually === true).forEach(item => {
      const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
      catalogGoodSave.isAddedManually = true;
      catalogGoodSave.dateMax = this.dateConverterService.dateToYear(item.dateMax);
      catalogGoodSave.dateMin = this.dateConverterService.dateToYear(item.dateMin);
      catalogGoodSave.goodId = item.goodId;
      catalogGoodSave.catalogId = item.catalogId;
      catalogGoodSave.isAddedManually = true;
      this.catalogSave.catalogGoods.push(catalogGoodSave);
    })

    console.log('this.catalogSave', this.catalogSave);
  }

  getFilteredPintelSheets(id: number) {
    return this.pintelSheets.filter(x => x.ageGroup.id === id);
  }

  checked(id: number): Boolean {
    if ((this.catalog.catalogGoods.filter(x => x.pintelSheetId === id
        && x.isAddedManually === false).length > 0)) {
      return true;
    }
    return false
  }

  onChange(event, id: number) {
    if (event.srcElement.checked) {
      if (!this.pintelSheetIdArray.find(x => x === id)) {
        this.pintelSheetIdArray.push(id);
      }
    } else {
      const lele = this.pintelSheetIdArray.find(x => x === id);
      const index = this.pintelSheetIdArray.indexOf(lele);
      this.pintelSheetIdArray.splice(index, 1)
    }
  }

  saveCatalog() {

    if (this.dateConverterService.dateValidity(this.catalogSave.catalogGoods) === 0 ) {

      console.log('thissavecat', this.catalogSave);

      this.pintelSheetQuery.pintelSheetsArray = this.pintelSheetIdArray;
      const $result = this.catalogService.updatePintelSheets(this.catalogSave, this.pintelSheetQuery)
      $result.subscribe(catalog => {
          this.catalog = catalog;
          this.setCatalog(this.catalog);
        },
        err => {
          console.log(err);
          const title = 'une erreur est survenue';
          const body = err.body;
          this.toasterService
            .popAsync(this.notificationService.showErrorToast(title, body));
        },
        () => {

          const title = 'Catalogue Modifié';
          const body = 'Le catalogue du ce n° ' + this.catalog.ceId + ' a bien été mis à jour.';
          this.toasterService
            .popAsync(this.notificationService.showSuccessToast(title, body));
        }
      )
    } else {
      const title = 'Vérifiez les dates !';
      const body = 'Assurez vous que les dates min ne soient pas supérieures au dates max';

      this.toasterService
        .popAsync(this.notificationService.showWarningToast(title, body));
    }
  }

  receiveCatalogGood(product) {

    console.log('thiscatalog', this.catalog);
    if (!this.catalog.catalogGoods.find(pr => pr.goodId === product.id) &&
      !this.catalogSave.catalogGoods.find(pr => pr.goodId === product.id)) {

      const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
      catalogGoodSave.catalogId = this.catalogSave.id;
      catalogGoodSave.goodId = product.id;
      catalogGoodSave.dateMin = product.dateMin;
      catalogGoodSave.dateMax = product.dateMax;
      catalogGoodSave.isAddedManually = true;
      catalogGoodSave.clientProductAlias = product.clientProductAlias;
      catalogGoodSave.employeeParticipationMessage = product.employeeParticipationMessage;
      this.catalogSave.catalogGoods.push(catalogGoodSave);
      this.saveCatalog();
    }
  }

  receiveDelete(event) {
    console.log('deleteEvent', event.data);
    if (this.catalogSave.catalogGoods.find(x => x.goodId === event.data.id)) {
      const index = this.catalogSave.catalogGoods.indexOf(event.data.goodId);
      this.catalogSave.catalogGoods.splice(index, 1);
      this.saveCatalog();
    }
  }

  receiveEdit(event): void {
    console.log('catalogLetterSave', this.catalog.catalogGoods)
    console.log('evennewddata', event);
    const editedProduct: CatalogGoodSave = new CatalogGoodSave();
    editedProduct.goodId = event.id;
    editedProduct.clientProductAlias = event.clientProductAlias;
    editedProduct.catalogId = this.catalogSave.id;
    editedProduct.dateMin = event.dateMin;
    editedProduct.dateMax = event.dateMax;
    editedProduct.employeeParticipationMessage = event.employeeParticipationMessage;
    editedProduct.isAddedManually = true;
    const index: number = this.catalogSave.catalogGoods.indexOf(
      this.catalogSave.catalogGoods.find(x => x.goodId === event.id));
    console.log('index', index);
    if (index !== -1) {
      this.catalogSave.catalogGoods.splice(index, 1, editedProduct);
    }
    this.saveCatalog();
  }
}
