import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Good} from '../../../../@core/data/models/products/Good';
import {NotificationService} from '../../../../@core/data/services/notification.service';
import {ToasterService} from 'angular2-toaster';
import {LocalDataSource} from 'ng2-smart-table';
import {GoodService} from '../../../../@core/data/services/good.service';
import * as XLSX from 'xlsx';
import {Product} from '../../../../@core/data/models/products/product';
import {GoodSave} from "../../../../@core/data/models/products/goodSave";
import {Router} from "@angular/router";

@Component({
  selector: 'ngx-product-list-batch-excel-import',
  templateUrl: './product-list-batch-excel-import.component.html',
  styleUrls: ['./product-list-batch-excel-import.component.scss']
})
export class ProductListBatchExcelImportComponent implements OnInit {


  constructor(private toasterService: ToasterService,
              private notificationService: NotificationService,
              private router: Router,
              private goodService: GoodService) {
  }
  refPintelArray: string[] = [];
  query: any = {
    refPintelArray: this.refPintelArray
  }
  xlsBatches: Good[] = [];
  xlsGoods: Good[];
  source: LocalDataSource = new LocalDataSource();
  products: any[] = [];

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
      products: {
        title: 'Produits',
        type: 'string',
        valuePrepareFunction: (value) => {
          return this.returnRefPintelz(value);
        },
        edit: false,
        sort: false
      }

    },
  };

  ngOnInit() {
  }

  returnRefPintelz(products: Product[]): String {
    let newStringArr: string[] = [];
    products.forEach(item => {
      newStringArr.push(item.refPintel);
    })

    const refPintelString: string = newStringArr.join(', ')
    return refPintelString;
  }

  onFileChange(event) {

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

      console.log('laallaa');
      this.products = <any[]>(XLSX.utils.sheet_to_json(ws, {header: 1}));
      const hdr: any[] = this.products[0];
      this.products.splice(0, 1);
      this.getBatchProducts(this.products, hdr);
    }
    reader.readAsBinaryString(target.files[0]);
  }

  getBatchProducts(batches: Good[], hdrArr: any[]) {

    hdrArr.forEach(item => {
      const itemtoLower = item.toLowerCase();
      hdrArr.indexOf(item);
      hdrArr.splice(hdrArr.indexOf(item), 1, itemtoLower);
    });

    const indexBatchProductRef = hdrArr.indexOf('lot');
    const indexRefPintel = hdrArr.indexOf('reference pintel');
    const indexDetails = hdrArr.indexOf('description');
    const indexPrice = hdrArr.indexOf('prix');
    const indexLetter = hdrArr.indexOf('lettre');
    const indexNameProd = hdrArr.indexOf('nom du lot');
    const indexDepartment = hdrArr.indexOf('univers');
    const indexType = hdrArr.indexOf('type de produit');

    const refArray: string[] = [];

    batches.forEach(item => {
      refArray.push(item[indexBatchProductRef]);
      console.log('item[indexBatchProductRef]', item[indexBatchProductRef]);
    })

    this.query.refPintelArray = refArray;
    let prods: Good[] = [];
    this.goodService.getAll(this.query)
      .subscribe(returnProds => {
          prods = returnProds.items;

          batches.forEach(item => {
            const good: Good = new GoodSave();
            good.refPintel = item[indexRefPintel];
            good.title = item[indexNameProd];
            good.indexId = item[indexLetter];
            good.details = item[indexDetails];
            good.goodDepartmentId = item[indexDepartment];
            const string: string = item[indexBatchProductRef]
            if (item[indexPrice] !== undefined) {
              good.price = this.priceConvert(item[indexPrice]);
            }

            console.log('string', string);
            if (string !== undefined) {
              good.products = prods.filter(x => string.includes(x.refPintel));
            }
            good.createdBy = 'mathieu';
            good.updatedBy = 'mathieu';
            good.isBatch = false;
            good.isEnabled = true;
            console.log('ggggg', good);

            if (good.refPintel !== undefined) {
              this.xlsBatches.push(good);
            }
          })
        },
        err => {
          console.log(err);
          this.toasterService.popAsync(this.notificationService.showErrorToast(err.type, err.body))
        },
        () => {
          this.source.load(this.xlsBatches);
        },
      );
  }

  importGoods() {
    if (this.source.count() >= 0) {
      this.source.getAll()
        .then(result => {
            this.goodService.multiCreateBatch(result).subscribe(res => {
                  console.log('resres', res);
                  const title = 'Articles Ajoutés';
                  const body = res.addedBatchCount + ' articles ont été ajoutés';

                  this.toasterService
                    .popAsync(this.notificationService.showSuccessToast(title, body));

                  if (res.notAddedBatchCount <= 0) {
                    const warningTitle = res.notAddedBatchCount + ' article(s) non ajouté(s)';
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

  priceConvert(price: string): number {

    const newPrice: string = price.replace(',', '.');
    const newNumber: number = +newPrice;
    return newNumber;
  }

  onClose() {
    this.router.navigate(['/products/product-list/']);
  }

}
