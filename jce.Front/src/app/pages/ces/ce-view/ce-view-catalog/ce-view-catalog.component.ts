import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Catalog} from '../../../../@core/data/models/catalog/catalog';
import {CatalogTypeService} from '../../../../@core/data/services/catalog-type.service';
import {KeyValuePairEnum} from '../../../../@core/data/models/Enums/keyValuePair.enum';
import {Router} from '@angular/router';

@Component({
  selector: 'ngx-ce-view-catalog',
  templateUrl: './ce-view-catalog.component.html',
  styleUrls: ['./ce-view-catalog.component.scss']
})
export class CeViewCatalogComponent implements OnInit {

  constructor(private catalogTypeService: CatalogTypeService,
              private router: Router) {
  }

  @Input() catalog: Catalog;
  @Input() productNumber: number;
  @Input() ceId: number;

  catalogTypes: KeyValuePairEnum[] = [];

  ngOnInit() {
    this.catalogTypeService.getAll()
      .subscribe(res => {
          this.catalogTypes = res;
          console.log('catatyprs', this.catalogTypes)
        }
      );
  }

  cataType(id: number): string {
    if (this.catalog) {
      const catalogTypeName = this.catalogTypes.find(x => x.id === id).name
      return catalogTypeName;
    }
  }

  routerToCatalog(catalogType: number, ceId: number) {

    this.router.navigateByUrl(
      '/catalogues/' + this.replaceValue(this.catalogTypes.find(
      x => x.id === catalogType).name)
      + '/' + ceId);
  }

  replaceValue(catalogType: string): string {
    console.log('catalog', catalogType);
   return catalogType.replace(' ', '-')
     .replace(/[ ]/g, '-')
     .replace(/[é]/g, 'e')
      .toLowerCase();
  }
}
