import {
  Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild,
  ViewContainerRef
} from '@angular/core';
import {Good} from '../../../../@core/data/models/products/Good';
import * as XLSX from 'xlsx';
import {LocalDataSource} from 'ng2-smart-table';
import {ToasterService} from 'angular2-toaster';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {GoodService} from '../../../../@core/data/services/good.service';
import {Ng2SmartTableComponent} from 'ng2-smart-table/ng2-smart-table.component';
import {GoodSave} from '../../../../@core/data/models/products/goodSave';
import {animate, state, style, transition, trigger} from '@angular/animations';
import {Router} from '@angular/router';
import {GoodExcel} from '../../../../@core/data/models/products/goodExcel';
import {Key} from '../../../../@core/data/models/shared/key';
import {product_headers} from '../shared/excel-import-headers';
import {ExcelImportService} from "../shared/excel-import.service";

@Component({
  selector: 'ngx-product-list-excel-import',
  templateUrl: './product-list-excel-import.component.html',
  styleUrls: ['./product-list-excel-import.component.scss'],
  animations: [
    trigger('visibilityChanged', [
      state('shown', style({opacity: 1})),
      state('hidden', style({opacity: 0})),
      transition('hidden => shown', animate('.3s'))
    ])
  ]
})
export class ProductListExcelImportComponent implements OnInit, OnChanges {

  visibility: string = 'hidden';

  @ViewChild('xlsTable', {read: Ng2SmartTableComponent}) xlsTable: any;

  errorPageSize: 5;

  constructor(private toasterService: ToasterService,
              private notificationService: NotificationService,
              private excelImportService: ExcelImportService,
              private router: Router,
              private goodService: GoodService) {
  }

  refPintelArray: string[] = [];
  errorsArr: Key[] = [];

  query: any = {
    refPintelArray: this.refPintelArray
  }

  xlsBatches: Good[] = [];
  xlsGoods: Good[];

  source: LocalDataSource = new LocalDataSource();
  validGoods: boolean = false;

  settings = {
    noDataMessage: 'Aucun produit à importer.',
    pager: {
      display: true,
      perPage: 15,
    },

    actions: {
      add: false,
      edit: false,
      delete: false,
    },
    columns: {
      refPintel: {
        title: 'Ref Pintel',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
      },
      indexId: {
        title: 'Lettre',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
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
        title: 'Description',
        type: 'string',
        editable: false,
        valuePrepareFunction: (value) => {
          return (value.length > 10 ? value.substring(0, 10) + '...' : value);
        },
      },
      price: {
        title: 'Prix',
        type: 'string',
        editable: false,
        sort: true,
      },
      pintelSheetId: {
        title: 'Fiche Pintel',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
      },
      originId: {
        title: 'Origine',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
      },
      supplierId: {
        title: 'ID Fournisseur',
        type: 'string',
        sort: true,
        editable: false,
        width: '10px',
      },
    },
  };

  products: any[] = [];

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {

  }

  onFileChange(event) {

    this.errorsArr = [];
    let products: any[] = [];

    const target: DataTransfer = <DataTransfer>(event.target);

    // if (target.files.length !== 1) throw new Error('Cannot use multiple files');
    // if (target.files[0].type !== 'application/vnd.ms-excel') {
    //   const title: string = 'Mauvais Format';
    //   const body: string = 'Le fichier doit être un fichier Excel.';
    //   this.toasterService
    //     .popAsync(this.notificationService.showErrorToast(title, body));
    //   throw  new Error('Mauvais Format');
    // }

    const reader: FileReader = new FileReader();
    reader.onload = (e: any) => {
      /* read workbook */
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, {type: 'binary'});
      /* grab first sheet */
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];
      /* save data */

      products = <any[]>(XLSX.utils.sheet_to_json(ws, {header: 1}));
      const hdr: any[] = this.hdrArrToLower(products[0]);
      const hdrLabels = product_headers;

      if (!this.checkFileValidity(hdr, hdrLabels)) {
        products.splice(0, 1);
        const goodsExcelArr: GoodExcel[] = this.mapExcelProduct(hdr, products, hdrLabels);
        this.errorsArr = this.goodsValidation(goodsExcelArr);
        this.validGoods = this.errorsArr.length <= 0 && goodsExcelArr.length > 0;
        if (this.validGoods) {
          this.source.load(goodsExcelArr);
        } else {
          // this.errorsArr.forEach(item => {
          //   this.toasterService.popAsync(
          //     this.notificationService.showWarningToast(item.goodId, item.errors.join(', '))
          //   )
          // })
        }

      } else {
        this.validGoods = false;
        this.toasterService.popAsync(
          this.notificationService.showErrorToast('Mauvais Fichier', 'Veuillez utiliser le modèle fourni.')
        )
      }
    }
    reader.readAsBinaryString(target.files[0]);
  }


  mapExcelProduct(hdrArr: any[], prods: any[], hdrArrLabels) {
    const goodArr: GoodExcel[] = [];
    prods.forEach(item => {

      const goodExcel: GoodExcel = new GoodExcel();
      goodExcel.refPintel = item[hdrArr.indexOf(hdrArrLabels.refPintel)];
      goodExcel.title = item[hdrArr.indexOf(hdrArrLabels.title)];
      goodExcel.pintelSheet = item[hdrArr.indexOf(hdrArrLabels.pintelSheet)];
      goodExcel.details = item[hdrArr.indexOf(hdrArrLabels.details)];
      goodExcel.price = item[hdrArr.indexOf(hdrArrLabels.price)];
      goodExcel.index = item[hdrArr.indexOf(hdrArrLabels.index)];
      goodExcel.productType = item[hdrArr.indexOf(hdrArrLabels.productType)];
      goodExcel.goodDepartment = item[hdrArr.indexOf(hdrArrLabels.goodDepartment)];
      goodExcel.supplier = item[hdrArr.indexOf(hdrArrLabels.supplier)];
      // goodExcel.season = item[hdrArr.indexOf(hdrArrLabels.season)];
      // goodExcel.isBasicProduct = item[hdrArr.indexOf(hdrArrLabels.isBasicProduct)];
      // goodExcel.isDisplayedOnJCE = item[hdrArr.indexOf(hdrArrLabels.isDisplayedOnJCE)];
      // goodExcel.isDiscountable = item[hdrArr.indexOf(hdrArrLabels.isDiscountable)];
      // goodExcel.isEnabled = item[hdrArr.indexOf(hdrArrLabels.isEnabled)];
      goodExcel.origin = item[hdrArr.indexOf(hdrArrLabels.origin)];
      goodExcel.origin = item[hdrArr.indexOf(hdrArrLabels.origin)];

      if (goodExcel.refPintel !== undefined) {
        goodArr.push(goodExcel);
      }
    });
    return goodArr;
  }

  goodsValidation(goodsExcel: GoodExcel[]) {

    const errorArray: Key[] = [];


    goodsExcel.forEach(item => {
      const errorStringArr: string[] = [];
      !this.excelImportService.check4Digits(item.refPintel) ?
        errorStringArr.push(this.excelImportService.errors.refPintel.format) : null;

      !this.excelImportService.checkNullOrEmpty(item.details) ?
        errorStringArr.push(this.excelImportService.errors.details.required) : null;

      !this.excelImportService.checkNullOrEmpty(item.title) ?
        errorStringArr.push(this.excelImportService.errors.title.required) : null;

      console.log('item.price', item.price);

      !this.excelImportService.checkPrice(item.price) ?
        errorStringArr.push(this.excelImportService.errors.price.required) : null;

      !this.excelImportService.checkNullOrEmpty(item.index) ?
        errorStringArr.push(this.excelImportService.errors.index.required) : null;

      !this.excelImportService.checkNullOrEmpty(item.productType) ?
        errorStringArr.push(this.excelImportService.errors.productType.required) : null

      !this.excelImportService.checkNullOrEmpty(item.goodDepartment) ?
        errorStringArr.push(this.excelImportService.errors.goodDepartment.required) : null;

      !this.excelImportService.checkNullOrEmpty(item.supplier) ?
        errorStringArr.push(this.excelImportService.errors.supplier.required) : null;

      if (errorStringArr.length > 0) {
        const goodKeys = new Key();
        goodKeys.goodId = item.refPintel ? item.refPintel : item.title;
        goodKeys.errors = errorStringArr;
        errorArray.push(goodKeys);
      }

    });

    console.log('errorArray', errorArray);

    return errorArray;
  }

  importGoods() {
    if (this.source.count() >= 0) {
      this.source.getAll()
        .then(result => {
            console.log('result', result);
            this.goodService.multiCreateProduct(result)
              .subscribe(res => {
                  console.log('resres', res);
                  const title = 'Articles Ajoutés';
                  const body = res.addedProductCount + ' articles ont été ajoutés';

                  this.toasterService
                    .popAsync(this.notificationService.showSuccessToast(title, body));

                  if (res.notAddedProductCount <= 0) {
                    const warningTitle = res.notAddedProductCount + ' article(s) non ajouté(s)';
                    const warningBody = 'Les produits suivants éxistent déjà en base de données : '
                      + res.duplicatedRefList.join(', ');

                    this.toasterService
                      .popAsync(this.notificationService.showWarningToast(warningTitle, warningBody));
                  }
                },
                err => {
                  console.log('err', err);
                }
              )
          }
        );
    }
  }

  hdrArrToLower(hdrArr: any[]): any[] {
    hdrArr.forEach(item => {
      const itemtoLower = item.toLowerCase();
      hdrArr.indexOf(item);
      hdrArr.splice(hdrArr.indexOf(item), 1, itemtoLower);
    });

    return hdrArr
  }

  checkFileValidity(hdrArr: any[], headers: any): boolean {
    let i = 0;
    const hdrObj = Object.values(headers);

    hdrArr.forEach(item => {
      const itemExists = hdrObj.indexOf(item);
      if (itemExists === -1) {
        i++
        console.log('i', i, itemExists, item);
      }
    });
    return !!i;
  }

  onClose() {
    this.router.navigate(['/products/product-list/']);
  }

}
