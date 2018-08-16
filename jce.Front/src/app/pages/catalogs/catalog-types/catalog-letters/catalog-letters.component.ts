import {Component, forwardRef, OnInit} from '@angular/core';
import {Catalog} from '../../../../@core/data/models/catalog/catalog';
import {ActivatedRoute, Router} from '@angular/router';
import {BodyOutputType, Toast, ToasterConfig, ToasterService} from 'angular2-toaster';
import {CatalogService} from '../../../../@core/data/services/catalog.service';
import {DatePipe} from '@angular/common';
import {CatalogGoodSave} from '../../../../@core/data/models/products/catalogGoodSave';
import {GoodService} from '../../../../@core/data/services/good.service';
import {DateConverterService} from '../../../../@core/utils/dateConverter.service';
import {CatalogGood} from '../../../../@core/data/models/products/catalogGood';
import {Subject} from 'rxjs/Subject';
import {SearchService} from '../../../../@core/data/services/search.service';
import {CatalogLettersSave} from '../../../../@core/data/models/catalog/catalogLettersSave';
import {NotificationService} from "../../../../@core/data/services/notification.service";

@Component({
  selector: 'ngx-catalog-letters',
  templateUrl: './catalog-letters.component.html',
  styleUrls: ['./catalog-letters.component.scss'],
})
export class CatalogLettersComponent implements OnInit {

  constructor(private catalogService: CatalogService,
              private goodService: GoodService,
              private toasterService: ToasterService,
              private route: ActivatedRoute,
              private dateConverterService: DateConverterService,
              private searchService: SearchService,
              private notificationService: NotificationService,
              private router: Router) {
    route.params.subscribe(p => this.id = +p['id'] || 0);
    this.searchService.search(this.searchTerm$, true)
      .subscribe(results => {
        this.products = results;
        console.log('results', this.products);
      })
  }

  products: Object;
  searchTerm$ = new Subject<string>();

  dateArr: any[] = [
    {
      letter: 'A',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'B',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'C',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'D',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'E',
      dateMin: 0,
      dateMax: 0,
      checked: false,

    },
    {
      letter: 'F',
      dateMin: 0,
      dateMax: 0,
      checked: false,

    },
    {
      letter: 'G',
      dateMin: 0,
      dateMax: 0,
      checked: false,

    },
    {
      letter: 'H',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'I',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    },
    {
      letter: 'J',
      dateMin: 0,
      dateMax: 0,
      checked: false,
    }];

  ceId: boolean = true;


  catalogQuery: any = {
    ceId: true,
  };

  query: any = {
    search: this.searchTerm$,
    lettersArray: [],
  };

  id: number;

  catalogLetterSave: CatalogLettersSave = new CatalogLettersSave();

  catalog: Catalog;

  // catalog: Catalog = {
  //   id: 0,
  //   ceId: 0,
  //   indexId: '',
  //   catalogType: 0,
  //   isActif: false,
  //   expirationDate: new Date,
  //   displayPrice: false,
  //   productChoiceQuantity: 0,
  //   catalogChoiceTypeId: 0,
  //   booksQuantity: 0,
  //   toysQuantity: 0,
  //   subscriptionQuantity: 0,
  //   createdBy: '',
  //   updatedBy: '',
  //   catalogGoods: [{
  //     goodId: 0,
  //     catalogId: 0,
  //     title: '',
  //     details: '',
  //     indexId: '',
  //     isBasicProduct: false,
  //     isDisplayedOnJce: false,
  //     pintelSheetId: 0,
  //     price: 0,
  //     productType: 0,
  //     refPintel: '',
  //     universID: 0,
  //     createdBy: '',
  //     updatedBy: '',
  //     dateMin: '',
  //     dateMax: '',
  //     clientProductAlias: '',
  //     isAddedManually: false,
  //   }]
  // };


  ngOnInit() {

    this.catalogService.getByCEId(this.id, this.catalogQuery)
      .subscribe(catalog => {
          this.catalog = catalog;
          console.log('thiscatalog', catalog);
          if (this.catalog.catalogType !== 2) {
            this.router.navigate(['catalogues/wrong-type']);
          }
          this.setCatalog(this.catalog);
          this.checked(this.catalog.catalogGoods);
          this.dateConverterService.dateDisplay(this.dateArr, this.catalog.catalogGoods);
        },
        err => {
          console.log(err)
        },
      );
  }

  setCatalog(catalog: Catalog) {
    this.catalogLetterSave.id = catalog.id;
    this.catalogLetterSave.ceId = catalog.ceId;
    this.catalogLetterSave.catalogType = catalog.catalogType;
    this.catalogLetterSave.indexId = catalog.indexId;
    this.catalogLetterSave.isActif = catalog.isActif;
    this.catalogLetterSave.expirationDate = catalog.expirationDate;
    this.catalogLetterSave.displayPrice = catalog.displayPrice;
    this.catalogLetterSave.productChoiceQuantity = catalog.productChoiceQuantity;
    this.catalogLetterSave.booksQuantity = catalog.booksQuantity;
    this.catalogLetterSave.catalogChoiceTypeId = catalog.catalogChoiceTypeId;
    this.catalogLetterSave.toysQuantity = catalog.toysQuantity;
    this.catalogLetterSave.subscriptionQuantity = catalog.subscriptionQuantity;
    this.catalogLetterSave.createdBy = catalog.createdBy;
    this.catalogLetterSave.updatedBy = catalog.updatedBy;
    this.catalogLetterSave.catalogGoods = [];
    catalog.catalogGoods.filter(x => x.isAddedManually === true).forEach(item => {
      const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
      catalogGoodSave.isAddedManually = true;
      catalogGoodSave.dateMax = this.dateConverterService.dateToYear(item.dateMax);
      catalogGoodSave.dateMin = this.dateConverterService.dateToYear(item.dateMin);
      catalogGoodSave.employeeParticipationMessage = item.employeeParticipationMessage;
      catalogGoodSave.goodId = item.goodId;
      catalogGoodSave.catalogId = item.catalogId;
      this.catalogLetterSave.catalogGoods.push(catalogGoodSave);
    })
    console.log('thiscatalog', this.catalogLetterSave);
  }


  receiveEdit(event): void {
    console.log('catalogLetterSave', this.catalog.catalogGoods)
      console.log('evennewddata', event);
      const editedProduct: CatalogGoodSave = new CatalogGoodSave();
      editedProduct.goodId = event.id;
      editedProduct.clientProductAlias = event.clientProductAlias;
      editedProduct.catalogId = this.catalogLetterSave.id;
      editedProduct.dateMin = event.dateMin;
      editedProduct.dateMax = event.dateMax;
      editedProduct.employeeParticipationMessage = event.employeeParticipationMessage;
      editedProduct.isAddedManually = true;
      const index: number = this.catalogLetterSave.catalogGoods.indexOf(
        this.catalogLetterSave.catalogGoods.find(x => x.goodId === event.id));
      console.log('index', index);
      if (index !== -1) {
        this.catalogLetterSave.catalogGoods.splice(index, 1, editedProduct);
      }
      console.log('index', index);

      this.saveCatalog();

  }


  receiveCatalogGood(product) {

    if (!this.catalog.catalogGoods.find(pr => pr.goodId === product.id)
      && !this.catalogLetterSave.catalogGoods.find(pr => pr.goodId === product.id)) {
      const catalogGoodSave: CatalogGoodSave = new CatalogGoodSave();
      catalogGoodSave.catalogId = this.catalogLetterSave.id;
      catalogGoodSave.goodId = product.id;
      catalogGoodSave.dateMin = product.dateMin;
      catalogGoodSave.dateMax = product.dateMax;
      catalogGoodSave.clientProductAlias = product.clientProductAlias;
      catalogGoodSave.employeeParticipationMessage = product.employeeParticipationMessage;
      catalogGoodSave.isAddedManually = true;
      this.catalogLetterSave.catalogGoods.push(catalogGoodSave);
      this.saveCatalog();
    }
  }

  saveCatalog() {
    this.dateArr.filter(x => x.checked).forEach(item => {
      this.query.lettersArray.push(item.letter)
    });

    if (this.dateConverterService.dateValidity(this.catalogLetterSave.catalogGoods) > 0 &&
      this.dateConverterService.dateValidityForDateArray(this.dateArr.filter(x => x.checked)) > 0) {
      const title = 'une erreur est survenue';
      const body = 'Vérifiez que les dates minimum sont bien inférieures aux dates maximum pour chaque lettre.';
      this.toasterService
        .popAsync(this.notificationService.showWarningToast(title, body));
    } else {
      this.catalogLetterSave.ProductLetters = this.dateArr.filter(x => x.checked);
      const $result = this.catalogService.updateLetters(this.catalogLetterSave, this.query);
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
    }
  }

  checked(catalogGoods: CatalogGood[]) {
    this.dateArr.forEach(item => {
        item.checked = (catalogGoods.filter(x => x.indexId === item.letter && x.isAddedManually === false).length > 0);
      }
    )
  }

  receiveDelete(event) {
    console.log('deleteEvent', event.data);
    console.log('this.catalogLetterSave', this.catalogLetterSave.catalogGoods);
    if (this.catalogLetterSave.catalogGoods.find(x => x.goodId === event.data.id)) {
      const index = this.catalogLetterSave.catalogGoods.indexOf(event.data.goodId);
      this.catalogLetterSave.catalogGoods.splice(index, 1);
      this.saveCatalog()
    }
  }
}
