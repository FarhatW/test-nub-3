import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Subject} from 'rxjs/Subject';
import {Good} from '../../../../../@core/data/models/products/Good';
import {SearchService} from '../../../../../@core/data/services/search.service';
import {Supplier} from '../../../../../@core/data/models/supplier/supplier';

@Component({
  selector: 'ngx-product-list-search',
  templateUrl: './product-list-search.component.html',
  styleUrls: ['./product-list-search.component.scss']
})
export class ProductListSearchComponent implements OnInit {

  searchTerm$ = new Subject<string>();
  searchType: string = 'goods';
  products: Good[] = [];
  totalItems: number;
  query: any = {
    searchType: this.searchType,
  };

  @Output() good2send = new EventEmitter<Good>();
  @Input() suppliers: Supplier[];

  constructor(private searchService: SearchService) {
    this.searchService.search(this.searchTerm$, this.query)
      .subscribe(results => {
        if (!(results instanceof Array)) {
          this.products = results.items;
          this.totalItems = results.totalItems;
        }
      })
  }

  ngOnInit() {
  }

  sendGood(good) {
    console.log('sendgood', good);
    this.good2send.emit(good);
  }

  clearSearch() {
    this.products = [];
    this.totalItems = -1;
  }

  returnString(totalItems: number): String {

    if (totalItems === 0) {
      return 'Aucun produit trouvé';
    }

    if (totalItems === 1) {
      return '1 Produit trouvé';
    }
    return (totalItems + ' produits trouvés');
  }

  returnSupplier(id: number) {
    if (this.suppliers) {
      return this.suppliers.find(x => x.id === id).name;
    }
  }
}
